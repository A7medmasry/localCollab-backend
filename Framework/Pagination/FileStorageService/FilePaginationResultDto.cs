namespace Framework.Pagination.FileStorageService
{
	public class FilePaginationResultDto
	{
		public IEnumerable<object> Files { get; set; }
		public int TotalRecords { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
