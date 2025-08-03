using Framework.Enums;
using Framework.Exceptions;
using Framework.QueryParameters;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.User;
using TiktokLocalAPI.Core.OutDto.User;
using TiktokLocalAPI.Core.OutDTO.User;

namespace TiktokLocalAPI.Services.Services
{
    /// <summary>
    /// Represents the user service responsible for handling user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService(IUserRepo userRepo, IFileService fileService)
        {
            _userRepo = userRepo;
            _fileService = fileService;
        }

        /// <inheritdoc/>
        public async Task DeleteUser(Guid requestorGuid, Guid userGuidToDelete)
        {
            await _userRepo.DeleteUser(userGuidToDelete);
        }

        /// <inheritdoc/>
        // public async Task<UserOutDto> UpdateUserProfile(Guid requestorGuid, UserEditDto dto)
        // {
        //     var userProfile = await _userRepo.GetUser(requestorGuid);
        //     if (userProfile == null)
        //         throw new QlBadRequestException(ExceptionMessages.UserProfileNotFound);

        //     if (requestorGuid != userProfile.Id && userProfile.Role == SystemRole.Admin)
        //         throw new QlBadRequestException(ExceptionMessages.CannotEditAdminRole);

        //     if (requestorGuid == userProfile.Id && !string.IsNullOrEmpty(dto.UserRole))
        //         throw new QlBadRequestException(ExceptionMessages.MissingPermissionsEditOwnRole);

        //     if (!string.IsNullOrEmpty(dto.Slug))
        //     {
        //         var existingUser = await _userRepo.GetUser(dto.Slug);
        //         if (existingUser != null && existingUser.Id != userProfile.Id)
        //         {
        //             throw new QlBadRequestException("This handle is already taken.");
        //         }
        //     }
        //     if (dto.AvatarFile != null)
        //     {
        //         var avatarPath = await _fileService.SaveImageToDisk(
        //             dto.AvatarFile,
        //             userProfile.Id,
        //             "avatar"
        //         );
        //         dto.Avatar = avatarPath;
        //     }
        //     Console.WriteLine($"wooo {dto.BusinessInformation}");
        //     Console.WriteLine($"wooo {dto.FirstName}");
        //     Console.WriteLine($"wooo {dto.Address}");

        //     userProfile.UpdateIfNew(dto, requestorGuid);

        //     if (dto.BusinessLogoFile != null)
        //     {
        //         var businessLogoFilePath = await _fileService.SaveImageToDisk(
        //             dto.BusinessLogoFile,
        //             userProfile.Id,
        //             "businessLogo"
        //         );

        //         userProfile.BusinessInformation.Logo = businessLogoFilePath;
        //     }

        //     if (dto.VerificationDocumentsFile != null)
        //     {
        //         var verificationDocumentsPath = await _fileService.SaveImageToDisk(
        //             dto.VerificationDocumentsFile,
        //             userProfile.Id,
        //             "verificationDocuments"
        //         );
        //         userProfile.BusinessInformation.VerificationDocuments = verificationDocumentsPath;
        //     }
        //     await _userRepo.UpdateUserProfile(userProfile);

        //     return userProfile.ToOutDto();
        // }

        public async Task<UserOutDto> UpdateUserProfile(
            Guid requestorGuid,
            UserEditDto dto,
            UserEditFormDto? form
        )
        {
            var userProfile = await _userRepo.GetUser(requestorGuid);
            if (userProfile == null)
                throw new QlBadRequestException(ExceptionMessages.UserProfileNotFound);

            if (!string.IsNullOrEmpty(dto.Slug))
            {
                var existingUser = await _userRepo.GetUserBySlug(dto.Slug);
                if (existingUser != null && existingUser.Id != userProfile.Id)
                    throw new QlBadRequestException("This handle is already taken.");
            }

            if (form.AvatarFile != null)
                dto.Avatar = await _fileService.SaveImageToDisk(
                    form.AvatarFile,
                    userProfile.Id,
                    "avatar"
                );

            userProfile.UpdateIfNew(dto, requestorGuid);

            if (form.BusinessLogoFile != null)
                userProfile.BusinessInformation.Logo = await _fileService.SaveImageToDisk(
                    form.BusinessLogoFile,
                    userProfile.Id,
                    "businessLogo"
                );

            if (form.VerificationDocumentsFile != null)
                userProfile.BusinessInformation.VerificationDocuments =
                    await _fileService.SaveImageToDisk(
                        form.VerificationDocumentsFile,
                        userProfile.Id,
                        "verificationDocuments"
                    );

            await _userRepo.UpdateUserProfile(userProfile);

            return userProfile.ToOutDto();
        }

        public async Task<UserOutDto> UpdateCreditsProfile(Guid requestorGuid, UserEditDto dto)
        {
            var userProfile = await _userRepo.GetUser(requestorGuid);
            if (userProfile == null)
                throw new QlBadRequestException(ExceptionMessages.UserProfileNotFound);

            userProfile.UpdateIfNewCredit(dto);

            await _userRepo.UpdateUserProfile(userProfile);

            return userProfile.ToOutDto();
        }

        /// <inheritdoc/>
        public async Task<UserOutDto> GetUserByGuid(Guid requestorGuid, Guid guid)
        {
            // TODO: requestorGuid is not used, consider removing it or using it for permission checks
            var user = await _userRepo.GetUser(guid);
            if (user == null)
                throw new QlNotFoundException(ExceptionMessages.UserProfileNotFound);

            return user.ToOutDto();
        }

        public async Task<UserOutDto> GetUserPublicByGuid(Guid guid)
        {
            // TODO: requestorGuid is not used, consider removing it or using it for permission checks
            var user = await _userRepo.GetUser(guid);
            if (user == null)
                throw new QlNotFoundException(ExceptionMessages.UserProfileNotFound);

            return user.ToOutDto();
        }

        /// <inheritdoc/>
        public async Task<FilteredUsersOutDto> GetAllUsers(
            Guid requestorGuid,
            UserQueryParameters queryParameters
        )
        {
            // TODO: requestorGuid is not used, consider removing it or using it for permission checks
            var (numberOfUsers, users) = await _userRepo.GetAllUsers(queryParameters);
            var outDtos = users.Select(x => x.ToOutDto()).ToList();

            return new FilteredUsersOutDto
            {
                Users = outDtos,
                TotalRecords = numberOfUsers,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize,
            };
        }

        /// <inheritdoc/>
        public async Task UpdateAccountStatus(
            Guid requestorGuid,
            Guid guid,
            UserStatusDto statusDto
        )
        {
            if (!Enum.TryParse(statusDto.Status, true, out ActivationStatus parsedStatus))
                throw new QlBadRequestException(ExceptionMessages.InvalidStatus);

            var user = await _userRepo.GetUser(guid);
            if (user == null)
                throw new QlBadRequestException(ExceptionMessages.AccountStatusUpdateFailed);

            user.Status = parsedStatus;
            await _userRepo.UpdateUserProfile(user);
        }
    }

    internal class BusinessInformationDto : BusinessInformationEditDto { }
}
