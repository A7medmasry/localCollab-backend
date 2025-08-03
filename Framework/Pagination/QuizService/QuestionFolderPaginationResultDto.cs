namespace Framework.Pagination.QuizService
{
	public class QuestionFolderPaginationResultDto
	{
		public IEnumerable<object> Hierarchies { get; set; }
		public int TotalRecords { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
