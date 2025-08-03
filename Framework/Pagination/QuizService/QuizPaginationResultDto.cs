namespace Framework.Pagination.QuizService
{
	public class QuizPaginationResultDto
	{
		public IEnumerable<object> Quizzes { get; set; }
		public int TotalRecords { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
