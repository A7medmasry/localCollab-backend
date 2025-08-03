using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.File
{
	public class UpdateFileDto
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string? FileName { get; set; }

		[Required]
		public int HierarchyId { get; set; }
	}
}
