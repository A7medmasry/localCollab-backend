using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Data.Models.User;
using TiktokLocalAPI.Core.OutDto.Service;

namespace TiktokLocalAPI.Core.Models.Service
{
    public enum ServiceStatus
    {
        Pending,
        Active,

        Paused,
        Expired,
    }

    public class ServiceModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Type { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required string Location { get; set; }

        // Compensation
        public string? CompensationType { get; set; }
        public string? CompensationAmount { get; set; }
        public string? CompensationCurrency { get; set; }
        public string? compensationProduct { get; set; }

        // Creator Requirements Minimum Followers
        public string? FollowerRequirement { get; set; }
        public string? Duration { get; set; }
        public string? Requirements { get; set; }

        // Owner
        public int? Views { get; set; } = 0;
        public Guid UserId { get; set; }
        // public required UserModel User { get; set; }

        public List<OrderModel> Orders { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ServiceStatus Status { get; set; } = ServiceStatus.Active;

        // ✅ Regular method, not an extension method
        public ServiceOutDto ToOutDto()
        {
            return new ServiceOutDto
            {
                Id = this.Id,
                Type = this.Type,
                Title = this.Title,
                Slug = this.Slug,
                Description = this.Description,
                Category = this.Category,
                Location = this.Location,

                CompensationType = this.CompensationType,
                CompensationAmount = this.CompensationAmount,
                CompensationCurrency = this.CompensationCurrency,
                compensationProduct = this.compensationProduct,

                FollowerRequirement = this.FollowerRequirement,
                Duration = this.Duration,
                Requirements = this.Requirements,

                Views = this.Views,
                UserId = this.UserId,
                // User = this.User,
                Orders = this.Orders,
                CreatedAt = this.CreatedAt,
                Status = (TiktokLocalAPI.Core.OutDto.Service.ServiceStatus)this.Status,
            };
        }
    }
}
