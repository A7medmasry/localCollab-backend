using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Enums;
using TiktokLocalAPI.Core.DTO.User;
using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Core.Models.Service;
using TiktokLocalAPI.Core.OutDto.User;

namespace TiktokLocalAPI.Data.Models.User
{
    /// <summary>
    /// Represents a user entity in the system, including profile information, credentials, and metadata.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Slug of the user.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the country where the user is located.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the city where the user is located.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the user's address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the user's avatar.
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets the user's Bio.
        /// </summary>
        public string? Bio { get; set; }
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the user's Collaboration Types.
        /// </summary>
        public string? CollaborationTypes { get; set; }

        /// <summary>
        /// Gets or sets the phone number associated with the user.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the system role of the user (e.g., User, Admin).
        /// </summary>
        public SystemRole Role { get; set; } = SystemRole.User;

        /// <summary>
        /// Gets or sets the account activation status of the user.
        /// </summary>
        public ActivationStatus Status { get; set; } = ActivationStatus.Confirmed;

        /// <summary>
        /// Gets or sets the date and time when the user account was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the user account was last edited.
        /// </summary>
        public DateTime? LastEditedAt { get; set; }

        /// <summary>
        /// Gets or sets the GUID of the user who last edited this account.
        /// </summary>
        public Guid? EditedBy { get; set; }

        public BusinessInformation BusinessInformation { get; set; } = new();
        public Creator Creator { get; set; } = new();

        public List<ServiceProvider> ServiceProvider { get; set; } = new();

        public StatusUser StatusUser { get; set; } = new();

        public string? StripeCustomerId { get; set; }
        public string? SubscriptionId { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime? ValidCredits { get; set; }
        public int Credits { get; set; } = 0;
        public string? Plan { get; set; }

        // Navigation
        public List<ServiceModel> Services { get; set; } = new();
        public List<OrderModel> OrdersSent { get; set; } = new();
        public List<OrderModel> OrdersReceived { get; set; } = new();
        public List<ReviewModel> ReviewsGiven { get; set; } = new();
        public List<ReviewModel> ReviewsReceived { get; set; } = new();

        [NotMapped]
        public double AverageCommunicationRating =>
            ReviewsReceived.Any() ? ReviewsReceived.Average(r => r.CommunicationRating) : 0;

        [NotMapped]
        public double AverageDeliveryRating =>
            ReviewsReceived.Any() ? ReviewsReceived.Average(r => r.DeliveryRating) : 0;

        [NotMapped]
        public double AverageQualityRating =>
            ReviewsReceived.Any() ? ReviewsReceived.Average(r => r.QualityRating) : 0;

        [NotMapped]
        public double AverageOverallRating =>
            ReviewsReceived.Any() ? ReviewsReceived.Average(r => r.OverallRating) : 0;

        [NotMapped]
        public double AverageProviderRating =>
            ReviewsGiven.Any(r => r.ProviderRating.HasValue)
                ? ReviewsGiven.Average(r => r.ProviderRating) ?? 0
                : 0;

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        public UserModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class using creation data and role.
        /// </summary>
        /// <param name="dto">The user creation data transfer object.</param>
        /// <param name="role">The role to assign to the user.</param>
        public UserModel(UserCreateDto dto, SystemRole role)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Slug = dto.Slug;
            Email = dto.Email;
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password); // TODO SECURITY: Store only hashed passwords
            Role = role;
            Country = dto.Country;
            City = dto.City;
            Address = dto.Address;
            Avatar = dto.Avatar;
            Gender = dto.Gender;
            Bio = dto.Bio;
            CollaborationTypes = dto.CollaborationTypes;
            Phone = dto.Phone;
            StripeCustomerId = dto.StripeCustomerId;
            SubscriptionId = dto.SubscriptionId;
            IsActive = dto.IsActive;
            ValidCredits = dto.ValidCredits;
            Credits = dto.Credits;
            Plan = dto.Plan;
        }

        /// <summary>
        /// Converts the user model to an output DTO for external use.
        /// </summary>
        /// <returns>A <see cref="UserOutDto"/> populated with the user's data.</returns>
        public UserOutDto ToOutDto()
        {
            return new UserOutDto
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Slug = Slug,
                Email = Email,
                UserRole = Role.ToString(),
                Status = Status.ToString(),
                CreatedAt = CreatedAt,
                LastEditedAt = LastEditedAt,
                Country = Country,
                City = City,
                Address = Address,
                Avatar = Avatar,
                Gender = Gender,
                Bio = Bio,
                CollaborationTypes = CollaborationTypes,
                Phone = Phone,
                StatusUser = StatusUser.ToOutDto(),
                BusinessInformation = BusinessInformation.ToOutDto(),
                Creator = Creator.ToOutDto(),
                ServiceProvider = ServiceProvider.Select(sp => sp.ToOutDto()).ToList(),

                AverageCommunicationRating = AverageCommunicationRating,
                AverageDeliveryRating = AverageDeliveryRating,
                AverageQualityRating = AverageQualityRating,
                AverageOverallRating = AverageOverallRating,
                ReviewsReceived = ReviewsReceived.Select(r => r.ToOutDto()).ToList(),
                ReviewsGiven = ReviewsGiven.Select(r => r.ToOutDto()).ToList(),
                AverageProviderRating = AverageProviderRating,

                StripeCustomerId = StripeCustomerId,
                SubscriptionId = SubscriptionId,
                IsActive = IsActive,
                ValidCredits = ValidCredits,
                Credits = Credits,
                Plan = Plan,
            };
        }

        /// <summary>
        /// Updates the user model with new values from the edit DTO, if provided.
        /// </summary>
        /// <param name="dto">The DTO containing fields to update.</param>
        /// <param name="updatedBy">The GUID of the user performing the update.</param>
        public void UpdateIfNew(UserEditDto dto, Guid updatedBy)
        {
            FirstName = dto.FirstName ?? FirstName;
            LastName = dto.LastName ?? LastName;
            Slug = dto.Slug ?? Slug;
            Country = dto.Country ?? Country;
            City = dto.City ?? City;
            Address = dto.Address ?? Address;
            Avatar = dto.Avatar ?? Avatar;
            Phone = dto.Phone ?? Phone;
            Bio = dto.Bio ?? Bio;
            Gender = dto.Gender ?? Gender;
            CollaborationTypes = dto.CollaborationTypes ?? CollaborationTypes;
            EditedBy = updatedBy;
            LastEditedAt = DateTime.UtcNow;

            if (dto.BusinessInformation != null)
            {
                if (BusinessInformation == null)
                    BusinessInformation = new BusinessInformation();

                BusinessInformation.Name = dto.BusinessInformation.Name;
                BusinessInformation.Website = dto.BusinessInformation.Website;
                BusinessInformation.Location = dto.BusinessInformation.Location;
                BusinessInformation.Size = dto.BusinessInformation.Size;
                BusinessInformation.Contact = dto.BusinessInformation.Contact;
                BusinessInformation.Logo = dto.BusinessInformation.Logo;
                BusinessInformation.VerificationDocuments =
                    dto.BusinessInformation.VerificationDocuments;
                BusinessInformation.Type = dto.BusinessInformation.Type;
            }

            if (dto.ServiceProviders != null)
            {
                ServiceProvider = dto
                    .ServiceProviders.Select(sp => new ServiceProvider
                    {
                        Id = sp.Id != Guid.Empty ? sp.Id : Guid.NewGuid(),
                        Name = sp.Name!,
                        StartingRate = sp.StartingRate,
                        UploadFile = sp.UploadFile,
                        Link = sp.Link,
                        Category =
                            sp.Category?.Select(c => new ServiceProviderCategory
                                {
                                    Id = c.Id != Guid.Empty ? c.Id : Guid.NewGuid(),
                                    Name = c.Name!,
                                    Status = c.Status,
                                })
                                .ToList() ?? new List<ServiceProviderCategory>(),
                    })
                    .ToList();
            }
        }

        public void UpdateIfNewCredit(UserEditDto dto)
        {
            StripeCustomerId = dto.StripeCustomerId ?? StripeCustomerId;
            SubscriptionId = dto.SubscriptionId ?? SubscriptionId;
            IsActive = dto.IsActive ?? IsActive;
            ValidCredits = dto.ValidCredits ?? ValidCredits;
            Credits = Credits + dto.Credits ?? 0;
            Plan = dto.Plan ?? Plan;
        }
        #endregion
    }

    public class BusinessInformation
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public string? Size { get; set; }
        public string? Website { get; set; }
        public string? Contact { get; set; }
        public string? Logo { get; set; }
        public string? VerificationDocuments { get; set; }
        public bool IsActive { get; set; } = false;

        public BusinessInformationOutDto ToOutDto()
        {
            return new BusinessInformationOutDto
            {
                Name = Name,
                Location = Location,
                Type = Type,
                Size = Size,
                Contact = Contact,
                Website = Website,
                Logo = Logo,
                VerificationDocuments = VerificationDocuments,
                IsActive = IsActive,
            };
        }
    }

    public class Creator
    {
        public bool ShowFollowerCountPublicly { get; set; } = true;
        public List<Platforms> Platforms { get; set; } = new();
        public bool IsActive { get; set; } = false;

        public CreatorOutDto ToOutDto()
        {
            return new CreatorOutDto
            {
                ShowFollowerCountPublicly = ShowFollowerCountPublicly,
                Platforms = Platforms.Select(p => p.ToOutDto()).ToList(),
                IsActive = IsActive,
            };
        }
    }

    public class Platforms
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Handle { get; set; }
        public string? Subscribers { get; set; }
        public string Status { get; set; } = "Pendding";
        public bool Connect { get; set; } = false;

        public PlatformsOutDto ToOutDto()
        {
            return new PlatformsOutDto
            {
                Id = Id,
                Name = Name,
                Handle = Handle,
                Subscribers = Subscribers,
                Status = Status,
                Connect = Connect,
            };
        }
    }

    public class ServiceProvider
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public double StartingRate { get; set; }
        public string? Link { get; set; }
        public string? UploadFile { get; set; }
        public List<ServiceProviderCategory> Category { get; set; } = new();
        public bool IsActive { get; set; } = false;

        public ServiceProviderOutDto ToOutDto()
        {
            return new ServiceProviderOutDto
            {
                Id = Id,
                Name = Name,
                StartingRate = StartingRate,
                Link = Link,
                UploadFile = UploadFile,
                Category = Category.Select(c => c.ToOutDto()).ToList(),
                IsActive = IsActive,
            };
        }
    }

    public class ServiceProviderCategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool Status { get; set; } = false;

        public ServiceProviderCategoryOutDto ToOutDto()
        {
            return new ServiceProviderCategoryOutDto
            {
                Id = Id,
                Name = Name,
                Status = Status,
            };
        }
    }

    public class StatusUser
    {
        public string TopRated { get; set; } = "Top Rated";
        public bool TopRatedStatus { get; set; } = false;

        public string Reliable { get; set; } = "Reliable";
        public bool ReliableStatus { get; set; } = false;
        public string FastResponder { get; set; } = "Fast Responder";
        public bool FastResponderStatus { get; set; } = false;
        public double ResponseRate { get; set; }
        public double ShowUpRate { get; set; }

        public StatusUserOutDto ToOutDto()
        {
            return new StatusUserOutDto
            {
                TopRated = TopRated,
                TopRatedStatus = TopRatedStatus,
                Reliable = Reliable,
                ReliableStatus = ReliableStatus,
                FastResponder = FastResponder,
                FastResponderStatus = FastResponderStatus,
                ResponseRate = ResponseRate,
                ShowUpRate = ShowUpRate,
            };
        }
    }
}
