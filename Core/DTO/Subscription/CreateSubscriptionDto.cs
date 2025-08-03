namespace TiktokLocalAPI.Core.DTO.Subscription
{
    public class CreateSubscriptionDto
    {
        public required string Email { get; set; }
        public required string PaymentMethodId { get; set; }
        public required string PriceId { get; set; }
    }
}
