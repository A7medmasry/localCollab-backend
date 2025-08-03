using System.ComponentModel.DataAnnotations;
using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Core.OutDto.Order;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Core.OutDto.User
{
    /// <summary>
    /// Represents the data returned when retrieving detailed information about a user.
    /// </summary>
    public class UserOutDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's Slug.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user.
        /// </summary>
        public string? UserRole { get; set; }

        /// <summary>
        /// Gets or sets the status of the user account (e.g., Active, Inactive).
        /// </summary>
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Gets or sets the timestamp indicating when the user account was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating the last time the user account was updated, if available.
        /// </summary>
        public DateTime? LastEditedAt { get; set; }

        /// <summary>
        /// Gets or sets the country of residence for the user.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the city where the user resides.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the user's address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the user's Avatar.
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the user's Gender.
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the user's Bio.
        /// </summary>
        public string? Bio { get; set; }

        /// <summary>
        /// Gets or sets the user's Bio.
        /// </summary>
        public string? CollaborationTypes { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        public string? Phone { get; set; }

        public StatusUserOutDto StatusUser { get; set; }
        public BusinessInformationOutDto? BusinessInformation { get; set; }
        public CreatorOutDto? Creator { get; set; }
        public List<ServiceProviderOutDto>? ServiceProvider { get; set; }

        public double AverageCommunicationRating { get; set; }
        public double AverageDeliveryRating { get; set; }
        public double AverageQualityRating { get; set; }
        public double AverageOverallRating { get; set; }
        public List<ReviewOutDto> ReviewsReceived { get; set; } = new();
        public List<ReviewOutDto> ReviewsGiven { get; set; } = new();

        public double AverageProviderRating { get; set; }

        public string? StripeCustomerId { get; set; }
        public string? SubscriptionId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ValidCredits { get; set; }
        public int Credits { get; set; }

        public string? Plan { get; set; }
    }
}
