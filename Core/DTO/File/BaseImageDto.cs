using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.File
{
    public class BaseImageDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
