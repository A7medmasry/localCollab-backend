using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
    /// <summary>
    /// Data transfer object (DTO) used to update or assign the status of a user account.
    /// </summary>
    public class UserStatusDto
    {
        /// <summary>
        /// Gets or sets the status of the user account.
        /// This field is required and typically maps to predefined status values such as Active, Inactive, or Suspended.
        /// </summary>
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
    }
}
