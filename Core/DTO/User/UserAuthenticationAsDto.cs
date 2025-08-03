using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
    /// <summary>
    /// Represents a partial authentication data transfer object, containing only the user's email address.
    /// </summary>
    public class UserAuthenticationAsDto
    {
        /// <summary>
        /// Gets or sets the email address of the user attempting authentication.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
