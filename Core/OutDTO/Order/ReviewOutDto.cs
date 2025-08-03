using TiktokLocalAPI.Core.Models.Service;
using TiktokLocalAPIs.Core.Enums;

namespace TiktokLocalAPI.Core.OutDto.Order
{
    public class ReviewOutDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public Guid FromUserId { get; set; }
        public string? FromUserName { get; set; }

        public Guid ToUserId { get; set; }
        public string? ToUserName { get; set; }

        public double CommunicationRating { get; set; }
        public double DeliveryRating { get; set; }
        public double QualityRating { get; set; }
        public double OverallRating { get; set; }
        public string? Comment { get; set; }
        public string? ProviderComment { get; set; }
        public double? ProviderRating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
