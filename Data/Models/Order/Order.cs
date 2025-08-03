using TiktokLocalAPI.Core.Models.Service;
using TiktokLocalAPI.Core.OutDto.Order;
using TiktokLocalAPI.Data.Models.User;
using TiktokLocalAPIs.Core.Enums;

namespace TiktokLocalAPI.Core.Models.Order
{
    public class OrderModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ServiceId { get; set; }

        public ServiceModel? Service { get; set; }
        public Guid FromUserId { get; set; } // client
        public UserModel? FromUser { get; set; }

        public Guid ToUserId { get; set; } // service provider
        public UserModel? ToUser { get; set; }

        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Description { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ReviewModel? Review { get; set; } // Navigation to review (optional)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public OrderOutDto ToOutDto()
        {
            return new OrderOutDto
            {
                Id = this.Id,
                ServiceId = this.ServiceId,
                ServiceName = this.Service?.Title, // assuming you want to expose Service details
                FromUserId = this.FromUserId,
                ToUserId = this.ToUserId,
                FromUserName =
                    $"{this.FromUser?.FirstName ?? ""} {this.FromUser?.LastName ?? ""}".Trim(),
                ToUserName = $"{this.ToUser?.FirstName ?? ""} {this.ToUser?.LastName ?? ""}".Trim(),
                Amount = this.Amount,
                StartDate = this.StartDate,
                DeliveryDate = this.DeliveryDate,
                Description = this.Description,
                Status = this.Status, // assuming you have a matching enum
                Review = this.Review?.ToOutDto(), // if you have a conversion method
                CreatedAt = CreatedAt,
            };
        }
    }
}
