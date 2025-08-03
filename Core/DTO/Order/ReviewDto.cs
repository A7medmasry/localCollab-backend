namespace TiktokLocalAPI.Core.DTO.Order
{
    public class ReviewDto
    {
        public Guid OrderId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public double CommunicationRating { get; set; }
        public double DeliveryRating { get; set; }
        public double QualityRating { get; set; }
        public required string Comment { get; set; }
    }
}
