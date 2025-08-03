namespace Framework.Pagination.EmailService
{
	public class EmailTemplatePaginationResultDto
	{
		public IEnumerable<object> EmailTemplates { get; set; }
		public int TotalRecords { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
