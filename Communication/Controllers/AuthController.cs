using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Framework.Extensions;
using Framework.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.Session;
using TiktokLocalAPI.Core.DTO.User;

namespace TiktokLocalAPI.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and registration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration; // إضافة IConfiguration

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Service for handling authentication operations.</param>
        /// <param name="configuration">App configuration instance to access settings.</param>
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        /// <summary>
        /// Registers a new user with the provided details.
        /// </summary>
        /// <param name="dto">User data transfer object for registration.</param>
        /// <returns>A result indicating success or failure of the registration process.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto dto)
        {
            var user = await _authService.Register(dto);

            return QlResult.Success(InformationMessages.UserCreationSucceded, user);
        }

        /// <summary>
        /// Authenticates a user using email and password.
        /// </summary>
        /// <param name="dto">Authentication data transfer object containing user credentials.</param>
        /// <returns>A result indicating success or failure of the login process.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAuthenticationDto dto)
        {
            if (dto == null)
            {
                QlResult.BadRequest(ExceptionMessages.InvalidData);
            }

            string? ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            string? userAgent = Request.Headers["User-Agent"].ToString();

            var result = await _authService.Login(dto.Email, dto.Password, ipAddress, userAgent);
            return QlResult.Success(InformationMessages.AuthenticationSucceeded, result);
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>A result indicating success or failure of the logout process.</returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var session = User.GetUserSession();

            string? authToken = HttpContext
                .Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")
                .Last();

            await _authService.Logout(session.Guid, authToken);
            return QlResult.Success(InformationMessages.LogoutSucceeded);
        }

        /// <summary>
        /// Refreshes the user's authentication session using a refresh token.
        /// </summary>
        /// <param name="dto">Session refresh data transfer object containing the refresh token.</param>
        /// <returns>A result containing the new authentication token and related information.</returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh(RefreshSessionDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.RefreshToken))
            {
                return QlResult.BadRequest(ExceptionMessages.InvalidData);
            }

            string? ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            string? userAgent = Request.Headers["User-Agent"].ToString();

            var tokens = await _authService.RenewAuthToken(dto.RefreshToken, ipAddress, userAgent);
            return QlResult.Success(InformationMessages.TokenRefreshed, tokens);
        }

        /// <summary>
        /// Validates the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token passed as a query parameter.</param>
        /// <returns>
        /// Returns 200 OK with a success message if the token is valid;
        /// otherwise, returns 401 Unauthorized with an error message.
        /// </returns>
        [HttpGet("validate")]
        public IActionResult ValidateToken([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { error = "Token is missing" });
            }

            // Validate the JWT token and get the claims principal
            var claimsPrincipal = ValidateJwtToken(token);

            if (claimsPrincipal == null)
            {
                return Unauthorized(new { error = "Invalid token" });
            }

            // Extract relevant user info from claims
            var userData = new Dictionary<string, string>();

            // Example: get user id, email, role from claims
            var GuidlaimClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(GuidlaimClaim))
                userData["Guid"] = GuidlaimClaim;

            // Return the user data as JSON to the gateway middleware
            return Ok(userData);
        }

        /// <summary>
        /// Validates the provided JWT token and returns the claims principal if valid.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/> if token is valid; otherwise, <c>null</c>.</returns>
        private ClaimsPrincipal? ValidateJwtToken(string token)
        {
            var jwtKey =
                _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key is not configured");

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    },
                    out SecurityToken validatedToken
                );

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
