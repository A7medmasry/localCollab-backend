using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.User
{
    /// <summary>
    /// Represents a data transfer object used to update user profile information.
    /// Only fields that need to be updated should be provided.
    /// </summary>
    public class UserEditDto
    {
        [StringLength(
            50,
            MinimumLength = 3,
            ErrorMessage = "First name must be between 3 and 50 characters."
        )]
        public string? FirstName { get; set; }

        [StringLength(
            50,
            MinimumLength = 3,
            ErrorMessage = "Last name must be between 3 and 50 characters."
        )]
        public string? LastName { get; set; }
        public string? Slug { get; set; }
        public string? UserRole { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Bio { get; set; }
        public string? CollaborationTypes { get; set; }

        public BusinessInformationEditDto? BusinessInformation { get; set; }

        public List<ServiceProviderEditDto>? ServiceProviders { get; set; }

        public IFormFile? AvatarFile { get; set; }
        public IFormFile? BusinessLogoFile { get; set; }
        public IFormFile? VerificationDocumentsFile { get; set; }
        public IFormFile? UploadServiceFile { get; set; }

        public string? StripeCustomerId { get; set; }
        public string? SubscriptionId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ValidCredits { get; set; }
        public int? Credits { get; set; }

        public string? Plan { get; set; }
    }
}
