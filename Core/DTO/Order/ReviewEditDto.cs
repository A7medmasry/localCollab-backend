namespace TiktokLocalAPI.Core.DTO.Order
{
    public class ReviewEditDto
    {
        public double ProviderRating { get; set; }
        public required string ProviderComment { get; set; }
    }
}
