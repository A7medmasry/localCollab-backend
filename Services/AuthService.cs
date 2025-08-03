using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Framework.Enums;
using Framework.Exceptions;
using Framework.Models;
using Microsoft.IdentityModel.Tokens;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.User;
using TiktokLocalAPI.Core.OutDTO.Auth;
using TiktokLocalAPI.Core.OutDto.User;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Services.Services
{
    /// <summary>
    /// Provides authentication-related services and implements the <see cref="IAuthService"/> interface.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly IPasswordService _passwordService;
        private readonly ISessionRepo _sessionRepo;
        private readonly IConfiguration _configuration;
        int validTokenMinutes = 60;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        public AuthService(
            IUserRepo userRepo,
            IPasswordService passwordService,
            ISessionRepo sessionRepo,
            IConfiguration configuration
        )
        {
            _userRepo = userRepo;
            _passwordService = passwordService;
            _sessionRepo = sessionRepo;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public async Task<UserOutDto> Register(UserCreateDto dto)
        {
            if (dto == null)
                throw new QlBadRequestException(ExceptionMessages.InvalidData);

            if (dto.UserRole == null || !Enum.TryParse(dto.UserRole, true, out SystemRole role))
                throw new QlBadRequestException(ExceptionMessages.InvalidUserRole);

            UserModel user = new UserModel(dto, role); // TODO SECURITY: Ensure password policies and rate-limiting are enforced at registration

            var existinUser = await _userRepo.GetUser(user.Email);
            if (existinUser != null)
            {
                throw new QlBadRequestException(ExceptionMessages.EmailAlreadyRegistered);
            }

            var registeredUser = await _userRepo.CreateUser(user);
            if (registeredUser == null)
                throw new QlBadRequestException(ExceptionMessages.UserCreationFailed);

            return registeredUser.ToOutDto();
        }

        /// <inheritdoc/>
        public async Task<AuthOutDto> Login(
            string email,
            string password,
            string? ipAddress,
            string? userAgent
        )
        {
            var user = await _userRepo.GetUser(email);
            if (user == null || !_passwordService.ValidatePassword(password, user.Password))
            {
                throw new QlBadRequestException(ExceptionMessages.InvalidPasswordOrEmail);
            }

            switch (user.Status)
            {
                case ActivationStatus.New:
                    throw new QlForbiddenException(ExceptionMessages.AccountNew);
                case ActivationStatus.Deactivated:
                    throw new QlForbiddenException(ExceptionMessages.AccountDeactivated);
                case ActivationStatus.Blocked:
                    throw new QlForbiddenException(ExceptionMessages.AccountBlocked);
            }

            await _sessionRepo.DeleteUserSessions(user.Id);
            var (jti, newSession) = await GenerateNewSession(
                user,
                ipAddress,
                userAgent,
                validTokenMinutes
            );

            return new AuthOutDto()
            {
                AuthToken = newSession.AuthToken,
                RefreshToken = newSession.RefreshToken,
            };
        }

        /// <inheritdoc/>
        public async Task<RenewOutDto> RenewAuthToken(
            string sessionToken,
            string? ipAddress,
            string? userAgent
        )
        {
            var dbSession = await _sessionRepo.GetSessionForRefreshToken(sessionToken);
            if (dbSession == null || dbSession.RefreshTokenExpiresAt < DateTime.UtcNow)
            {
                if (dbSession != null)
                    await _sessionRepo.DeleteSession(dbSession);
                throw new QlNotFoundException(ExceptionMessages.SessionNotFound);
            }

            var (jti, newAuthToken) = GenerateJwtToken(dbSession.User, validTokenMinutes);
            dbSession.AuthToken = newAuthToken;
            dbSession.AuthTokenExpiresAt = DateTime.UtcNow.AddHours(validTokenMinutes);
            dbSession.AuthTokenRefreshes++;

            await _sessionRepo.UpdateSession(dbSession);

            return new RenewOutDto()
            {
                Token = newAuthToken,
                SessionToken = dbSession.RefreshToken,
            };
        }

        /// <inheritdoc/>
        public async Task Logout(Guid requestorGuid, string? authToken)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new QlNotFoundException(ExceptionMessages.InvalidData);

            var dbSession = await _sessionRepo.GetSession(requestorGuid, authToken);
            if (dbSession != null)
                await _sessionRepo.DeleteSession(dbSession);
        }

        #region Private methods

        private (string, string) GenerateJwtToken(UserModel user, int validityMinutes)
        {
            var jti = Guid.NewGuid().ToString();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Guid", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(validityMinutes)
            );

            return (jti, new JwtSecurityTokenHandler().WriteToken(token));
        }

        private string GenerateRefreshToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[64];
            rng.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        private async Task<(string, SessionModel)> GenerateNewSession(
            UserModel user,
            string? ipAddress,
            string? userAgent,
            int validTokenMinutes
        )
        {
            var (jti, authToken) = GenerateJwtToken(user, validTokenMinutes);
            string refreshToken = GenerateRefreshToken();

            Console.WriteLine($"whyyyy {user.Id}");

            var session = new SessionModel
            {
                UserId = user.Id,
                AuthToken = authToken,
                AuthTokenExpiresAt = DateTime.UtcNow.AddMinutes(validTokenMinutes),
                RefreshToken = refreshToken,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                LastLogin = DateTime.UtcNow,
                AuthTokenRefreshes = 1,
            };

            await _sessionRepo.CreateSession(session);
            return (jti, session);
        }

        #endregion
    }
}
