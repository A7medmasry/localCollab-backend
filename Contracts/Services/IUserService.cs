using Framework.Enums;
using Framework.Models;
using Framework.QueryParameters;
using TiktokLocalAPI.Core.DTO.User;
using TiktokLocalAPI.Core.OutDto.User;
using TiktokLocalAPI.Core.OutDTO.User;

namespace TiktokLocalAPI.Contracts.Services
{
    /// <summary>
    /// Represents a service for managing user-related operations, such as retrieval, update, deletion, and synchronization.
    /// </summary>
    public interface IUserService
    {
        Task DeleteUser(Guid requestorGuid, Guid userGuidToDelete);

        Task<UserOutDto> UpdateUserProfile(
            Guid requestorGuid,
            UserEditDto dto,
            UserEditFormDto? form = null
        );
        Task<UserOutDto> UpdateCreditsProfile(Guid requestorGuid, UserEditDto dto);

        Task<UserOutDto> GetUserByGuid(Guid requestorGuid, Guid guid);
        Task<UserOutDto> GetUserPublicByGuid(Guid guid);

        Task<FilteredUsersOutDto> GetAllUsers(
            Guid requestorGuid,
            UserQueryParameters queryParameters
        );

        Task UpdateAccountStatus(Guid requestorGuid, Guid guid, UserStatusDto status);
    }
}
