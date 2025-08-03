using Framework.Enums;
using Framework.QueryParameters;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Contracts.Repositories
{
    /// <summary>
    /// Represents the repository interface for user operations, including user management, retrieval,
    /// password management, and role assignment.
    /// </summary>
    public interface IUserRepo
    {
        #region User Basic Operations

        /// <summary>
        /// Retrieves a user by email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user model if found; otherwise, null.</returns>
        Task<UserModel?> GetUser(string? email);

        /// <summary>
        /// Retrieves a user by email.
        /// </summary>
        /// <param name="slug">The email of the user to retrieve.</param>
        /// <returns>The user model if found; otherwise, null.</returns>
        Task<UserModel?> GetUserBySlug(string? slug);

        /// <summary>
        /// Retrieves a user by GUID.
        /// </summary>
        /// <param name="guid">The GUID of the user to retrieve.</param>
        /// <returns>The user model if found; otherwise, null.</returns>
        Task<UserModel?> GetUser(Guid? guid);

        /// <summary>
        /// Retrieves all users with pagination and filtering.
        /// </summary>
        /// <param name="queryParameters">Query parameters for filtering and pagination.</param>
        /// <returns>A tuple containing the total count and the list of users.</returns>
        Task<(int, IEnumerable<UserModel>)> GetAllUsers(UserQueryParameters queryParameters);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user model to create.</param>
        /// <returns>The created user model.</returns>
        Task<UserModel> CreateUser(UserModel user);

        /// <summary>
        /// Deletes a user by their GUID.
        /// </summary>
        /// <param name="userGuid">The GUID of the user to delete.</param>
        Task DeleteUser(Guid userGuid);

        /// <summary>
        /// Updates the profile information of a user.
        /// </summary>
        /// <param name="user">The user model with updated data.</param>
        Task UpdateUserProfile(UserModel user);

        /// <summary>
        /// Assigns a new role to the specified user.
        /// </summary>
        /// <param name="user">The user to assign the role to.</param>
        /// <param name="newRole">The new role to assign.</param>
        /// <returns>True if the role was assigned successfully; otherwise, false.</returns>
        Task<bool> AssignRole(UserModel? user, SystemRole newRole);

        #endregion

        #region Password and Security Management

        /// <summary>
        /// Retrieves all password reset requests for a user.
        /// </summary>
        /// <param name="guid">The GUID of the user whose requests are to be retrieved.</param>
        /// <returns>A collection of password reset requests, or null if none exist.</returns>
        Task<IEnumerable<PasswordResetRequestModel>?> GetPasswordResetRequestsForUser(Guid? guid);

        /// <summary>
        /// Creates a password reset token for a user.
        /// </summary>
        /// <param name="user">The user model for which to create the token.</param>
        /// <returns>The generated password reset token.</returns>
        Task<string> CreatePasswordResetToken(UserModel user);

        /// <summary>
        /// Updates a user's password.
        /// </summary>
        /// <param name="user">The user model whose password is to be updated.</param>
        /// <param name="newPassword">The new password to set.</param>
        Task UpdatePassword(UserModel user, string newPassword);

        /// <summary>
        /// Retrieves a password reset request by its token.
        /// </summary>
        /// <param name="token">The reset token to look up.</param>
        /// <returns>The matching password reset request, or null if not found.</returns>
        Task<PasswordResetRequestModel?> GetPasswordResetRequest(string? token);

        /// <summary>
        /// Removes specified password reset requests.
        /// </summary>
        /// <param name="request">The collection of requests to remove.</param>
        Task RemovePasswordResetRequests(IEnumerable<PasswordResetRequestModel>? request);
        #endregion
    }
}
