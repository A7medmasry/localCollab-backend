using Framework.Enums;
using Framework.Extensions;
using Framework.Objects;
using Framework.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.User;

namespace TiktokLocalAPI.Controllers
{
    /// <summary>
    /// Controller responsible for managing user accounts, including retrieval, updates, deletions, and status changes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">Service that provides user management functionality.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="guid">The GUID of the user to delete.</param>
        /// <returns>A result indicating whether the deletion was successful.</returns>
        [HttpDelete]
        [Authorize(Roles = nameof(SystemRole.Admin))]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid guid)
        {
            var session = User.GetUserSession();

            await _userService.DeleteUser(session.Guid, guid);

            return QlResult.Success(InformationMessages.UserDeletedSuccessfully);
        }

        /// <summary>
        /// Updates the profile information of a specified user.
        /// </summary>
        /// <param name="formDto">An object containing the updated user information.</param>
        /// <returns>A result with the updated user information.</returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] UserEditFormDto formDto)
        {
            // Deserialize JSON to real DTO
            var dto = JsonConvert.DeserializeObject<UserEditDto>(formDto.Dto);
            var session = User.GetUserSession();

            // Now call your service
            var result = await _userService.UpdateUserProfile(session.Guid, dto, formDto);
            return Ok(result);
        }

        // public async Task<IActionResult> UpdateUserProfile(
        //     UserEditDto dto,
        //     [FromQuery] Guid profileGuid
        // )
        // {
        //     var session = User.GetUserSession();

        //     var user = await _userService.UpdateUserProfile(session.Guid, dto);

        //     return QlResult.Success(InformationMessages.UserProfileUpdatedSuccessfully, user);
        // }

        /// <summary>
        /// Retrieves a user account by its GUID.
        /// </summary>
        /// <param name="guid">The GUID of the user to retrieve.</param>
        /// <returns>A result with the user data.</returns>
        [HttpGet]
        [Authorize(Roles = nameof(SystemRole.Admin))]
        public async Task<IActionResult> GetAccount([FromQuery] Guid guid)
        {
            var session = User.GetUserSession();

            var user = await _userService.GetUserByGuid(session.Guid, guid);

            return QlResult.Success(InformationMessages.UserRetrieved, user);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetUser(Guid guid)
        {
            var user = await _userService.GetUserPublicByGuid(guid);

            return QlResult.Success(InformationMessages.UserRetrieved, user);
        }

        /// <summary>
        /// Retrieves the profile of the currently logged-in user.
        /// </summary>
        /// <returns>A result with the current user's profile information.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetLoggedInUserProfile()
        {
            var session = User.GetUserSession();

            var user = await _userService.GetUserByGuid(session.Guid, session.Guid);

            return QlResult.Success(InformationMessages.UserRetrieved, user);
        }

        /// <summary>
        /// Retrieves a paginated list of users based on filtering and sorting criteria.
        /// </summary>
        /// <param name="queryParameters">Query parameters for filtering, sorting, and paginating users.</param>
        /// <returns>A result with a list of users matching the query.</returns>
        [HttpGet("all")]
        [Authorize(Roles = nameof(SystemRole.Admin))]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] UserQueryParameters queryParameters
        )
        {
            var session = User.GetUserSession();

            var users = await _userService.GetAllUsers(session.Guid, queryParameters);

            return QlResult.Success(InformationMessages.UsersRetrieved, users);
        }

        /// <summary>
        /// Updates the account status of a user.
        /// </summary>
        /// <param name="guid">The GUID of the user to update.</param>
        /// <param name="statusDto">An object containing the new status for the user.</param>
        /// <returns>A result indicating whether the status was updated successfully.</returns>
        [HttpPost("status")]
        [Authorize(Roles = nameof(SystemRole.Admin))]
        public async Task<IActionResult> UpdateUserStatus(
            [FromQuery] Guid guid,
            UserStatusDto statusDto
        )
        {
            var session = User.GetUserSession();

            await _userService.UpdateAccountStatus(session.Guid, guid, statusDto);

            return QlResult.Success(InformationMessages.AccountStatusUpdated);
        }

        /// <summary>
        /// Retrieves a list of all possible user statuses.
        /// </summary>
        /// <returns>A result with an array of user status strings.</returns>
        [HttpGet("status/all")]
        [Authorize(Roles = nameof(SystemRole.Admin))]
        public ActionResult<string[]> GetUserStatuses()
        {
            ActivationStatus[] statuses = (ActivationStatus[])
                Enum.GetValues(typeof(ActivationStatus));

            string[] statusStrings = statuses.Select(status => status.ToString()).ToArray();

            return QlResult.Success(
                InformationMessages.UserStatusesRetrievedSuccessfully,
                statusStrings
            );
        }
    }
}
