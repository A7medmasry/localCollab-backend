namespace TiktokLocalAPI.Core.DTO.Order
{
    public class ReviewResponseDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public double CommunicationRating { get; set; }
        public double DeliveryRating { get; set; }
        public double QualityRating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
