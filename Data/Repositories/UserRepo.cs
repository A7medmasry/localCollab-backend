using Framework.Enums;
using Framework.QueryParameters;
using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Data.Database;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Data.Repositories
{
    /// <summary>
    /// Represents the repository for performing user-related operations.
    /// Provides functionalities such as creating, updating, deleting users,
    /// managing roles and statuses, and retrieving user data.
    /// </summary>
    public class UserRepo : IUserRepo
    {
        private readonly TiktokLocalDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepo"/> class.
        /// </summary>
        /// <param name="context">The database context used for user-related operations.</param>
        public UserRepo(TiktokLocalDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<UserModel> CreateUser(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <inheritdoc/>
        public async Task DeleteUser(Guid userGuid)
        {
            var user = await GetUser(userGuid);
            if (user == null)
                return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateUserProfile(UserModel userProfile)
        {
            _context.Users.Update(userProfile);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> AssignRole(UserModel? user, SystemRole newRole)
        {
            if (user == null)
                return false;

            user.Role = newRole;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<UserModel?> GetUser(string? email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserModel?> GetUserBySlug(string? slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;
            return await _context.Users.FirstOrDefaultAsync(u => u.Slug == slug);
        }

        /// <inheritdoc/>
        public async Task<UserModel?> GetUser(Guid? guid)
        {
            if (guid == null || guid == Guid.Empty)
                return null;
            return await _context
                .Users.Include(u => u.ReviewsReceived)
                .ThenInclude(r => r.FromUser)
                .Include(u => u.ReviewsGiven)
                .ThenInclude(r => r.ToUser)
                .FirstOrDefaultAsync(u => u.Id == guid);
        }

        /// <inheritdoc/>
        public async Task<(int, IEnumerable<UserModel>)> GetAllUsers(
            UserQueryParameters queryParameters
        )
        {
            return await ApplyUsersFilter(_context.Users, queryParameters);
        }

        #region Private Methods

        /// <summary>
        /// Applies filtering, searching, sorting, and pagination to the user query based on the provided parameters.
        /// </summary>
        /// <param name="query">The base user query.</param>
        /// <param name="queryParameters">The filtering and pagination parameters.</param>
        /// <returns>A tuple containing the total record count and the filtered user list.</returns>
        private async Task<(int, IEnumerable<UserModel>)> ApplyUsersFilter(
            IQueryable<UserModel> query,
            UserQueryParameters queryParameters
        )
        {
            if (!string.IsNullOrEmpty(queryParameters.Search))
            {
                query = query.Where(u =>
                    u.Email.Contains(queryParameters.Search)
                    || u.FirstName.Contains(queryParameters.Search)
                    || u.LastName.Contains(queryParameters.Search)
                );
            }

            if (
                !string.IsNullOrEmpty(queryParameters.Role)
                && Enum.TryParse(queryParameters.Role, true, out SystemRole role)
            )
            {
                query = query.Where(u => u.Role == role);
            }

            if (
                !string.IsNullOrEmpty(queryParameters.Status)
                && Enum.TryParse(queryParameters.Status, true, out ActivationStatus status)
            )
            {
                query = query.Where(u => u.Status == status);
            }

            if (!string.IsNullOrEmpty(queryParameters.OrderByField))
            {
                bool isAscending = queryParameters.IsAscending.GetValueOrDefault(true);
                query = queryParameters.OrderByField.ToLower() switch
                {
                    "firstname" => isAscending
                        ? query.OrderBy(u => u.FirstName)
                        : query.OrderByDescending(u => u.FirstName),
                    "lastname" => isAscending
                        ? query.OrderBy(u => u.LastName)
                        : query.OrderByDescending(u => u.LastName),
                    "email" => isAscending
                        ? query.OrderBy(u => u.Email)
                        : query.OrderByDescending(u => u.Email),
                    "role" => isAscending
                        ? query.OrderBy(u => u.Role.ToString())
                        : query.OrderByDescending(u => u.Role.ToString()),
                    "createdat" => isAscending
                        ? query.OrderBy(u => u.CreatedAt)
                        : query.OrderByDescending(u => u.CreatedAt),
                    _ => query.OrderByDescending(u => u.CreatedAt),
                };
            }

            int totalCount = await query.CountAsync();
            var results = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (totalCount, results);
        }

        #endregion

        #region Password and Security Management

        /// <inheritdoc/>
        public async Task<string> CreatePasswordResetToken(UserModel user)
        {
            if (user == null)
                return string.Empty;

            var token = Guid.NewGuid().ToString();
            var resetRequest = new PasswordResetRequestModel
            {
                Guid = user.Id,
                Token = token,
                ExpiryDate = DateTime.UtcNow.AddMinutes(30),
            };

            _context.PasswordResetRequests.Add(resetRequest);
            await _context.SaveChangesAsync();
            return token;
        }

        /// <inheritdoc/>
        public async Task UpdatePassword(UserModel user, string passwordHash)
        {
            if (user == null || string.IsNullOrEmpty(passwordHash))
                return;

            user.Password = passwordHash;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<PasswordResetRequestModel?> GetPasswordResetRequest(string? token)
        {
            if (string.IsNullOrEmpty(token))
                return null;
            return await _context.PasswordResetRequests.FirstOrDefaultAsync(r => r.Token == token);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PasswordResetRequestModel>?> GetPasswordResetRequestsForUser(
            Guid? guid
        )
        {
            if (guid == null)
                return null;
            return await _context.PasswordResetRequests.Where(r => r.Guid == guid).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task RemovePasswordResetRequests(
            IEnumerable<PasswordResetRequestModel>? requests
        )
        {
            if (requests == null)
                return;

            _context.PasswordResetRequests.RemoveRange(requests);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
