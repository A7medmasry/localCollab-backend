using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
	/// <summary>
	/// Represents a data transfer object containing a user's email address.
	/// Typically used in operations such as password reset requests or email-based lookups.
	/// </summary>
	public class EmailDto
	{
		/// <summary>
		/// Gets or sets the user's email address.
		/// This field is required and must be in a valid email format.
		/// </summary>
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address format.")]
		public string Email { get; set; }
	}
}
