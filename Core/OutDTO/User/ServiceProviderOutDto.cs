using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.OutDto.User
{
    public class ServiceProviderOutDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public double StartingRate { get; set; }
        public string? Link { get; set; }
        public string? UploadFile { get; set; }
        public bool IsActive { get; set; }
        public List<ServiceProviderCategoryOutDto> Category { get; set; } = new();
    }

    public class ServiceProviderCategoryOutDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = false;
    }
}
