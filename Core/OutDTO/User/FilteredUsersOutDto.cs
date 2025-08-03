using TiktokLocalAPI.Core.OutDto.User;

namespace TiktokLocalAPI.Core.OutDTO.User
{
    /// <summary>
    /// Represents a paginated and filtered list of users, typically returned from a user query operation.
    /// </summary>
    public class FilteredUsersOutDto
    {
        /// <summary>
        /// Gets or sets the list of users returned for the current page.
        /// </summary>
        public List<UserOutDto> Users { get; set; }

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
