namespace TiktokLocalAPI.Core.OutDto.Service
{
    /// <summary>
    /// Represents a paginated and filtered list of users, typically returned from a user query operation.
    /// </summary>
    public class FilteredServicesOutDto
    {
        /// <summary>
        /// Gets or sets the list of Service returned for the current page.
        /// </summary>
        public List<ServiceOutDto> Services { get; set; }

        /// <summary>
        /// Gets or sets the total number of user records matching the filter criteria.
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the current page number in the result set.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of user records per page.
        /// </summary>
        public int PageSize { get; set; }
    }
}
