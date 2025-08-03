using Framework.Exceptions;
using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Data.Models.User;
using TiktokLocalAPI.Data.Database;

namespace TiktokLocalAPI.Data.Repositories
{
    /// <summary>
    /// Repository for managing user session data, including creation, retrieval, update, and deletion.
    /// </summary>
    public class SessionRepo : ISessionRepo
    {
        private readonly TiktokLocalDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionRepo"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public SessionRepo(TiktokLocalDbContext context)
        {
            _context = context;
        }

        #region Session

        /// <summary>
        /// Creates a new user session in the database.
        /// </summary>
        /// <param name="session">The session model to create.</param>
        /// <returns>The created <see cref="SessionModel"/>.</returns>
        /// <exception cref="QlException">Thrown if the provided session is null.</exception>
        public async Task<SessionModel> CreateSession(SessionModel session)
        {
            if (session == null)
                throw new QlException(nameof(session));

            Console.WriteLine($"whyyyy2 {session.UserId}");

            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        /// <summary>
        /// Retrieves a session associated with a user by GUID, optionally filtered by auth token.
        /// </summary>
        /// <param name="userGuid">The GUID of the user.</param>
        /// <param name="authToken">The JWT token associated with the session (optional).</param>
        /// <returns>The matching <see cref="SessionModel"/>, or null if not found.</returns>
        public async Task<SessionModel?> GetSession(Guid userGuid, string? authToken)
        {
            var query = _context.Sessions.Where(s => s.UserId == userGuid);

            if (!string.IsNullOrEmpty(authToken))
            {
                query = query.Where(s => s.AuthToken == authToken);
            }

            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a session based on the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token used for authentication renewal.</param>
        /// <returns>The matching <see cref="SessionModel"/>, or null if not found.</returns>
        public async Task<SessionModel?> GetSessionForRefreshToken(string refreshToken)
        {
            return await _context
                .Sessions.Include(x => x.User)
                .FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
        }

        /// <summary>
        /// Updates an existing session in the database.
        /// </summary>
        /// <param name="session">The session to update.</param>
        /// <exception cref="ArgumentNullException">Thrown if the session is null.</exception>
        public async Task UpdateSession(SessionModel session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a specific session from the database.
        /// </summary>
        /// <param name="session">The session to delete.</param>
        public async Task DeleteSession(SessionModel session)
        {
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes all sessions associated with a specific user.
        /// </summary>
        /// <param name="userGuid">The GUID of the user whose sessions are to be removed.</param>
        public async Task DeleteUserSessions(Guid userGuid)
        {
            var expiredSessions = await _context
                .Sessions.Where(s => s.UserId == userGuid)
                .ToListAsync();

            _context.Sessions.RemoveRange(expiredSessions);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
