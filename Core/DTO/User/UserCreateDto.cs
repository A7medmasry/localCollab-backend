using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
    /// <summary>
    /// Represents the data required to create a new user account.
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(255, ErrorMessage = "Email cannot be longer than 255 characters.")]
        [RegularExpression(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Email must have a valid domain."
        )]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the user's account.
        /// Must be at least 8 characters and include uppercase, lowercase, and numeric characters.
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
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role to assign to the user.
        /// Optional for systems that auto-assign default roles.
        /// </summary>
        public string? UserRole { get; set; }

        /// <summary>
        /// Gets or sets the Slug.
        /// Optional for systems that auto-assign default roles.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Gets or sets the country where the user resides.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the city where the user resides.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the user's street address or physical location.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the user's Avatar
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the user's Gender
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the user's bio
        /// </summary>
        public string? Bio { get; set; }

        /// <summary>
        /// Gets or sets the user's bio
        /// </summary>
        public string? CollaborationTypes { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        public string? Phone { get; set; }

        public string? StripeCustomerId { get; set; }
        public string? SubscriptionId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ValidCredits { get; set; }
        public int Credits { get; set; }

        public string? Plan { get; set; }
    }
}
