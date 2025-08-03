namespace Framework.QueryParameters
{
    public class QueryParameters
    {
        /// <summary>
        /// The search term to filter results.
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// The field to filter by.
        /// </summary>
        public string? FilterField { get; set; }

        /// <summary>
        /// The value to filter for.
        /// </summary>
        public string? FilterValue { get; set; }

        /// <summary>
        /// The field to order by.
        /// </summary>
        public string? OrderByField { get; set; }

        /// <summary>
        /// Indicates whether the ordering should be ascending.
        /// </summary>
        public bool IsAscending { get; set; } = true;

        /// <summary>
        /// The page number (1-based) for pagination.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// The number of items per page for pagination.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// The start date for filtering records based on a date range.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// The end date for filtering records based on a date range.
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
}
