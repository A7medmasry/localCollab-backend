using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.PasswordReset
{
	/// <summary>
	/// Represents the data required to reset a password for an authenticated user,
	/// including the current (old) password and the desired new password.
	/// </summary>
	public class AuthenticatedPasswordResetDto
	{
		/// <summary>
		/// Gets or sets the user's current password.
		/// </summary>
		[Required(ErrorMessage = "Old password is required.")]
		public string OldPassword { get; set; }

		/// <summary>
		/// Gets or sets the new password to apply to the user's account.
		/// Must be between 8 and 100 characters and include at least one uppercase letter, one lowercase letter, and one digit.
		/// </summary>
		[Required(ErrorMessage = "New password is required.")]
		[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters and no more than 100 characters.")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit.")]
		public string NewPassword { get; set; }
	}
}
