namespace TiktokLocalAPI.Core.DTO.Order
{
    public class OrderCreateDto
    {
        public Guid ServiceId { get; set; }
        public Guid ToUserId { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Description { get; set; }
    }
}
