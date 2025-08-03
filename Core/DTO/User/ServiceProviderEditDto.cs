namespace TiktokLocalAPI.Core.DTO.User
{
    public class ServiceProviderEditDto
    {
        public Guid Id { get; set; } // If new, set as Guid.Empty
        public string Name { get; set; } = string.Empty;
        public double StartingRate { get; set; }
        public string? Link { get; set; }
        public string? UploadFile { get; set; }

        public List<ServiceProviderCategoryEditDto>? Category { get; set; }
    }

    public class ServiceProviderCategoryEditDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
