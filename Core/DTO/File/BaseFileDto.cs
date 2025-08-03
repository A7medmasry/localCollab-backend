using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.File
{
	public class BaseFileDto
	{
		[Required]
		public string FileName { get; set; }

		[Required]
		public IFormFile File { get; set; }

		[Required]
		public int HierarchyId { get; set; }
	}
}
