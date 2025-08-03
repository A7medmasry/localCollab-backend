using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Data.Models.User
{
    /// <summary>
    /// Represents a user session entity, storing authentication and token-related metadata.
    /// </summary>
    public class SessionModel
    {
        /// <summary>
        /// Gets or sets the unique session identifier.
        /// </summary>
        [Key]
        public int SessionId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the session.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user entity associated with this session.
        /// </summary>
        public virtual UserModel User { get; set; }

        /// <summary>
        /// Gets or sets the authentication token (JWT) used for accessing secured resources.
        /// </summary>
        public required string AuthToken { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the authentication token was created.
        /// </summary>
        public DateTime AuthTokenCreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the expiration timestamp of the authentication token.
        /// </summary>
        public DateTime AuthTokenExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the refresh token used to renew authentication without logging in again.
        /// </summary>
        public required string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the refresh token was issued.
        /// </summary>
        [Required]
        public DateTime RefreshTokenCreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the expiration timestamp of the refresh token.
        /// </summary>
        public DateTime RefreshTokenExpiresAt { get; set; } = DateTime.UtcNow.AddDays(30);

        /// <summary>
        /// Gets or sets the IP address associated with the session, if available.
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the user agent string from the client that initiated the session, if available.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the user's last login during this session.
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the number of times the auth token has been refreshed in this session.
        /// </summary>
        public int AuthTokenRefreshes { get; set; } = 0;
    }
}
