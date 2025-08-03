namespace TiktokLocalAPI.Core.DTO.Service
{
    public class ServiceCreateDto
    {
        public required string Title { get; set; }
        public required string Type { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required string Location { get; set; }
        public string? CompensationType { get; set; }
        public string? CompensationAmount { get; set; }
        public string? CompensationCurrency { get; set; }
        public string? compensationProduct { get; set; }
        public string? FollowerRequirement { get; set; }
        public string? Duration { get; set; }
        public string? Requirements { get; set; }
    }
}
