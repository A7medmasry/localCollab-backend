using Framework.Exceptions;
using Framework.Models;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.PasswordReset;
using TiktokLocalAPI.Core.DTO.User;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Services.Services
{
	/// <summary>
	/// Provides password-related functionality.
	/// </summary>
	public class PasswordService : IPasswordService
	{
		private readonly IUserRepo _userRepo;

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordService"/> class.
		/// </summary>
		/// <param name="userRepo">The user repository used for password operations.</param>
		/// <param name="logger">The logger instance.</param>
		public PasswordService(IUserRepo userRepo)
		{
			_userRepo = userRepo;
		}

		/// <inheritdoc/>
		public async Task<string> RequestNewPassword(EmailDto dto)
		{
			if (dto == null)
				throw new QlBadRequestException(ExceptionMessages.InvalidData);

			var user = await _userRepo.GetUser(dto.Email);
			if (user == null)
				throw new QlBadRequestException(ExceptionMessages.UserProfileNotFound);

			var existingResetRequest = await _userRepo.GetPasswordResetRequestsForUser(user.Id);
			if (existingResetRequest != null)
			{
				await _userRepo.RemovePasswordResetRequests(existingResetRequest);
			}

			var token = await _userRepo.CreatePasswordResetToken(user);

			return token;
		}

		/// <inheritdoc/>
		public async Task ResetPassword(PasswordResetDto dto)
		{
			if (dto == null)
				throw new QlBadRequestException(ExceptionMessages.InvalidData);

			var resetRequest = await _userRepo.GetPasswordResetRequest(dto.Token);
			if (resetRequest == null)
				throw new QlBadRequestException(ExceptionMessages.PasswordResetRequestNotFound);

			var user = await _userRepo.GetUser(resetRequest.Guid);
			if (user == null)
				throw new QlBadRequestException(ExceptionMessages.UserProfileNotFound);

			var hashedPassword = HashPassword(dto.NewPassword);
			await _userRepo.UpdatePassword(user, hashedPassword);
			await _userRepo.RemovePasswordResetRequests(new List<PasswordResetRequestModel> { resetRequest });
		}

		/// <inheritdoc/>
		public async Task ChangePassword(Guid requestorGuid, AuthenticatedPasswordResetDto dto)
		{
			if (dto == null)
				throw new QlBadRequestException(ExceptionMessages.InvalidData);

			var user = await _userRepo.GetUser(requestorGuid);
			if (user == null)
				throw new QlBadRequestException(ExceptionMessages.UserProfileNotFound);

			var validOldPassword = ValidatePassword(dto.OldPassword, user.Password);
			if (!validOldPassword)
				throw new QlBadRequestException(ExceptionMessages.OldPasswordNotMatching);

			var newHashPassword = HashPassword(dto.NewPassword);
			await _userRepo.UpdatePassword(user, newHashPassword);

		}

		/// <inheritdoc/>
		public bool ValidatePassword(string dtoPassword, string dbPassword)
		{
			return BCrypt.Net.BCrypt.Verify(dtoPassword, dbPassword);
		}

		/// <summary>
		/// Hashes a password using BCrypt.
		/// </summary>
		/// <param name="password">The password to be hashed.</param>
		/// <returns>The hashed password.</returns>
		private static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password); // TODO SECURITY: Use versioning and cost factor management for hashing
		}
	}
}
