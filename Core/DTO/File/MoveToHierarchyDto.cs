using System.ComponentModel.DataAnnotations;

namespace TiktokLocalAPI.Core.DTO.File
{
	public class MoveToHierarchyDto
	{
		[Required]
		public int FileId { get; set; }

		[Required]
		public int HierarchyId { get; set; }
	}
}
