using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
    /// <summary>
    /// Represents a data transfer object used to retrieve summary information about a user.
    /// Suitable for listings and overviews in administrative or user management interfaces.
    /// </summary>
    public class RetrieveUserListItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        [Key]
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user.
        /// </summary>
        public string UserRole { get; set; }

        /// <summary>
        /// Gets or sets the activation or account status of the user.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time the user account was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
