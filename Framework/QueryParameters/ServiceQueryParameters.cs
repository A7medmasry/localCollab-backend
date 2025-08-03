namespace Framework.QueryParameters
{
    public class ServiceQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? Category { get; set; }
        public string? Location { get; set; }
        public string? OrderByField { get; set; }
        public bool? IsAscending { get; set; }
    }
}
