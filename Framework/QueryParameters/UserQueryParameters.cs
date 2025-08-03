namespace Framework.QueryParameters
{
    /// <summary>
    /// Represents the parameters for querying user data, including pagination, filtering, and sorting options.
    /// </summary>
    public class UserQueryParameters // to be deleted and only the one in Identity.core should be used
    {
        /// <summary>
        /// Gets or sets the page number for the result page. Defaults to 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of records to display per page. Defaults to 10.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the search term used for filtering user data.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Gets or sets the role used for filtering user data.
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        /// Gets or sets the status used for filtering user data.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the field to use for sorting user data.
        /// </summary>
        public string? OrderByField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sorting order is ascending.
        /// </summary>
        public bool? IsAscending { get; set; }
    }
}
