using TiktokLocalAPI.Core.DTO.PasswordReset;
using TiktokLocalAPI.Core.DTO.User;

namespace TiktokLocalAPI.Contracts.Services
{
	/// <summary>
	/// Defines the contract for a service responsible for managing user passwords.
	/// </summary>
	public interface IPasswordService
	{
		/// <summary>
		/// Initiates a process to request a new password for a user by email.
		/// Generates a password reset token and typically sends it to the user's email.
		/// </summary>
		/// <param name="dto">Data Transfer Object containing the user's email address.</param>
		/// <returns>A message indicating the outcome of the password reset request.</returns>
		Task<string> RequestNewPassword(EmailDto dto);

		/// <summary>
		/// Resets a user's password using a password reset token.
		/// </summary>
		/// <param name="dto">Data Transfer Object containing the reset token and new password.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task ResetPassword(PasswordResetDto dto);


		Task ChangePassword(Guid requestorGuid, AuthenticatedPasswordResetDto dto);

		/// <summary>
		/// Validates a user-provided password against the stored hashed password.
		/// </summary>
		/// <param name="dtoPassword">The plain-text password provided by the user.</param>
		/// <param name="dbPassword">The hashed password stored in the database.</param>
		/// <returns>True if the password is valid; otherwise, false.</returns>
		bool ValidatePassword(string dtoPassword, string dbPassword);
	}
}
