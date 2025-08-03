using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.OutDto.User
{
    public class CreatorOutDto
    {
        public bool ShowFollowerCountPublicly { get; set; } = true;
        public List<PlatformsOutDto> Platforms { get; set; } = new();
        public bool IsActive { get; set; }
    }

    public class PlatformsOutDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Handle { get; set; }
        public string? Subscribers { get; set; }
        public string Status { get; set; } = "Pendding";
        public bool Connect { get; set; } = false;
    }
}
