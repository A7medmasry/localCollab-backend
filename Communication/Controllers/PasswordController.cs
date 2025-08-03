using Framework.Extensions;
using Framework.Objects;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.PasswordReset;
using TiktokLocalAPI.Core.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TiktokLocalAPI.Controllers
{
	/// <summary>
	/// Controller responsible for handling password-related operations, such as resetting and changing user passwords.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class PasswordController : ControllerBase
	{
		private readonly IPasswordService _passwordService;

		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordController"/> class.
		/// </summary>
		/// <param name="passwordService">Service that provides password management functionality.</param>
		public PasswordController(IPasswordService passwordService)
		{
			_passwordService = passwordService;
		}

		/// <summary>
		/// Sends a password reset link to the specified email address.
		/// </summary>
		/// <param name="dto">An object containing the user's email address.</param>
		/// <returns>A result indicating whether the reset email was sent successfully.</returns>
		[HttpPost("new")]
		public async Task<IActionResult> RequestNewPassword(EmailDto dto)
		{
			var newPass = await _passwordService.RequestNewPassword(dto);

			return QlResult.Success(InformationMessages.ResetPasswordRequestSentSuccessfully, newPass);
		}

		/// <summary>
		/// Resets the user's password using the provided token and new password.
		/// </summary>
		/// <param name="dto">An object containing the reset token and new password.</param>
		/// <returns>A result indicating whether the password was reset successfully.</returns>
		[HttpPost("reset")]
		public async Task<IActionResult> ResetPassword(PasswordResetDto dto)
		{
			await _passwordService.ResetPassword(dto);

			return QlResult.Success(InformationMessages.PasswordResetedSuccessfully);
		}

		/// <summary>
		/// Changes the password of the currently authenticated user.
		/// </summary>
		/// <param name="dto">An object containing the current and new passwords.</param>
		/// <returns>A result indicating whether the password was changed successfully.</returns>
		[HttpPost("change")]
		[Authorize]
		public async Task<IActionResult> ChangePassword(AuthenticatedPasswordResetDto dto)
		{
			var session = User.GetUserSession();

			await _passwordService.ChangePassword(session.Guid, dto);

			return QlResult.Success(InformationMessages.PasswordChangedSuccessfully);
		}
	}
}
