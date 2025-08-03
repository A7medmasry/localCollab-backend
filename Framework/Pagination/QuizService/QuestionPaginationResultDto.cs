namespace Framework.Pagination.QuizService
{
	public class QuestionPaginationResultDto
	{
		public IEnumerable<object> Questions { get; set; }
		public int TotalRecords { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
