using TiktokLocalAPI.Data.Models.User;
using TiktokLocalAPI.Core.Models.Order;

namespace TiktokLocalAPI.Core.OutDto.Service
{
    public class ServiceOutDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Type { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required string Location { get; set; }

        public string? CompensationType { get; set; }
        public string? CompensationAmount { get; set; }
        public string? CompensationCurrency { get; set; }
        public string? compensationProduct { get; set; }

        public string? FollowerRequirement { get; set; }
        public string? Duration { get; set; }
        public string? Requirements { get; set; }

        public int? Views { get; set; }
        public Guid UserId { get; set; }
        // public required UserModel User { get; set; }
        public List<OrderModel> Orders { get; set; } = new();

        public DateTime CreatedAt { get; set; }

        public ServiceStatus Status { get; set; }
    }

    public enum ServiceStatus
    {
        Pending,
        Active,

        Paused,
        Expired,
    }
}
