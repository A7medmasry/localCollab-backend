using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Contracts.Repositories
{
    /// <summary>
    /// Interface for repository handling session and login record operations.
    /// </summary>
    public interface ISessionRepo
    {
        /// <summary>
        /// Retrieves a session associated with a user by their GUID, optionally using a JWT token.
        /// </summary>
        /// <param name="userGuid">The GUID of the user to retrieve the session for.</param>
        /// <param name="authToken">The JWT token associated with the session (optional).</param>
        /// <returns>The session model if found; otherwise, null.</returns>
        Task<SessionModel?> GetSession(Guid userGuid, string? authToken = null);

        /// <summary>
        /// Retrieves a session based on the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to identify the session.</param>
        /// <returns>The session model if found; otherwise, null.</returns>
        Task<SessionModel?> GetSessionForRefreshToken(string refreshToken);

        /// <summary>
        /// Creates a new user session.
        /// </summary>
        /// <param name="session">The session model to be created.</param>
        /// <returns>The created session model.</returns>
        Task<SessionModel> CreateSession(SessionModel session);

        /// <summary>
        /// Updates an existing user session.
        /// </summary>
        /// <param name="session">The session model with updated information.</param>
        Task UpdateSession(SessionModel session);

        /// <summary>
        /// Deletes a specific user session.
        /// </summary>
        /// <param name="session">The session model to delete.</param>
        Task DeleteSession(SessionModel session);

        /// <summary>
        /// Deletes all sessions associated with a specific user.
        /// </summary>
        /// <param name="userGuid">The GUID of the user whose sessions are to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteUserSessions(Guid userGuid);
    }
}
