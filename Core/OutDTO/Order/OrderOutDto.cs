using TiktokLocalAPI.Core.Models.Service;
using TiktokLocalAPIs.Core.Enums;

namespace TiktokLocalAPI.Core.OutDto.Order
{
    public class OrderOutDto
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }
        public string? ServiceName { get; set; }

        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }

        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string? Description { get; set; }

        public OrderStatus Status { get; set; }

        // Optional: expose FromUser and ToUser info (e.g., name or username)
        public string? FromUserName { get; set; }
        public string? ToUserName { get; set; }

        // Optional: Review DTO
        public ReviewOutDto? Review { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
