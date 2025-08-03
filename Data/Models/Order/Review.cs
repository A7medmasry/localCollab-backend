using System.ComponentModel.DataAnnotations.Schema;
using TiktokLocalAPI.Core.OutDto.Order;
using TiktokLocalAPI.Data.Models.User;

namespace TiktokLocalAPI.Core.Models.Order
{
    public class ReviewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public required OrderModel Order { get; set; }

        public Guid FromUserId { get; set; }
        public required UserModel FromUser { get; set; }

        public Guid ToUserId { get; set; }
        public required UserModel ToUser { get; set; }

        // ✅ Individual Ratings
        public double CommunicationRating { get; set; }
        public double DeliveryRating { get; set; }
        public double QualityRating { get; set; }

        // ✅ Optional: Computed Total Rating (average of above)
        [NotMapped]
        public double OverallRating =>
            Math.Round((CommunicationRating + DeliveryRating + QualityRating) / 3, 2);
        public required string Comment { get; set; }
        public string? ProviderComment { get; set; }
        public double? ProviderRating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ReviewOutDto ToOutDto()
        {
            return new ReviewOutDto
            {
                Id = this.Id,
                OrderId = this.OrderId,
                FromUserId = this.FromUserId,
                ToUserId = this.ToUserId,
                FromUserName =
                    $"{this.FromUser?.FirstName ?? ""} {this.FromUser?.LastName ?? ""}".Trim(),
                ToUserName = $"{this.ToUser?.FirstName ?? ""} {this.ToUser?.LastName ?? ""}".Trim(),
                CommunicationRating = this.CommunicationRating,
                DeliveryRating = this.DeliveryRating,
                QualityRating = this.QualityRating,
                OverallRating = this.OverallRating,
                Comment = this.Comment,
                ProviderComment = this.ProviderComment,
                ProviderRating = this.ProviderRating,
                CreatedAt = CreatedAt,
            };
        }
    }
}
