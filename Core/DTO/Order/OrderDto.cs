using TiktokLocalAPI.Core.Models.Order;
using TiktokLocalAPI.Core.OutDto.Order;
using TiktokLocalAPIs.Core.Enums;

namespace TiktokLocalAPI.Core.DTO.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid FromUserId { get; set; }
        public required string FromUserName { get; set; }
        public Guid ToUserId { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Description { get; set; }
        public OrderStatus Status { get; set; }

        public ReviewOutDto? Review { get; set; }
    }
}
