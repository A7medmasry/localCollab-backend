namespace Framework.Pagination.EmailService
{
	public class EmailLogPaginationResultDto
	{
		public IEnumerable<object> EmailLogs { get; set; }
		public int TotalRecords { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
