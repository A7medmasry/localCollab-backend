using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.OutDto.User
{
    public class BusinessInformationOutDto
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public string? Size { get; set; }
        public string? Website { get; set; }
        public string? Contact { get; set; }
        public string? Logo { get; set; }
        public string? VerificationDocuments { get; set; }
        public bool IsActive { get; set; }
    }
}
