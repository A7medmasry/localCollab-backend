using Framework.Models;
using TiktokLocalAPI.Core.DTO.User;
using TiktokLocalAPI.Core.OutDTO.Auth;
using TiktokLocalAPI.Core.OutDto.User;

namespace TiktokLocalAPI.Contracts.Services
{
    /// <summary>
    /// Interface for authentication-related operations such as user registration, login, token renewal, and logout.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user with the provided data.
        /// </summary>
        /// <param name="dto">The data transfer object containing the user's registration details.</param>
        /// <returns>The registered user's public information.</returns>
        Task<UserOutDto> Register(UserCreateDto dto);

        /// <summary>
        /// Authenticates a user using their email and password.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="ipAddress">The user's IP address (optional).</param>
        /// <param name="userAgent">The user's browser or device user agent string (optional).</param>
        /// <returns>The authentication result containing access and refresh tokens.</returns>
        Task<AuthOutDto> Login(string email, string password, string? ipAddress, string? userAgent);

        /// <summary>
        /// Refreshes the authentication token for an existing user session.
        /// </summary>
        /// <param name="sessionToken">The session token to refresh.</param>
        /// <param name="ipAddress">The user's IP address (optional).</param>
        /// <param name="userAgent">The user's user agent string (optional).</param>
        /// <returns>The renewed authentication and session token data.</returns>
        Task<RenewOutDto> RenewAuthToken(string sessionToken, string? ipAddress, string? userAgent);

        Task Logout(Guid requestorGuid, string? authToken);
    }
}
