using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
    /// <summary>
    /// Represents a data transfer object used for authenticating a user, including email and password fields.
    /// </summary>
    public class UserAuthenticationDto
    {
        /// <summary>
        /// Gets or sets the email address of the user attempting to log in.
        /// This field is required and must be in a valid email format.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password associated with the user's account.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
