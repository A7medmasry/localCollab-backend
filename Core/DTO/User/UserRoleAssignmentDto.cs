using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
	/// <summary>
	/// Represents a data transfer object used to assign a role to a user.
	/// </summary>
	public class UserRoleAssignmentDto
	{
		/// <summary>
		/// Gets or sets the email address of the user to whom the role will be assigned.
		/// </summary>
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address format.")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the role that will be assigned to the user.
		/// </summary>
		[Required(ErrorMessage = "User Role is required.")]
		public string Role { get; set; }
	}
}
