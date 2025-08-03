using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.PasswordReset
{
    /// <summary>
    /// Represents the data required to reset a password using a reset token and a new password.
    /// </summary>
    public class PasswordResetDto
    {
        /// <summary>
        /// Gets or sets the token provided to authorize the password reset.
        /// </summary>
        [Required(ErrorMessage = "Reset token is required.")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the new password to apply to the user's account.
        /// Must be between 8 and 100 characters and include at least one uppercase letter, one lowercase letter, and one digit.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(
            100,
            MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters and no more than 100 characters."
        )]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit."
        )]
        public string NewPassword { get; set; }
    }
}
