namespace Framework.QueryParameters
{
	public interface IQueryableService<T>
	{
		/// <summary>
		/// Applies a search operation to the query.
		/// </summary>
		/// <param name="query">The original queryable data.</param>
		/// <param name="searchTerm">The search term to filter results.</param>
		/// <returns>A queryable result with the search applied.</returns>
		IQueryable<T> ApplySearch(IQueryable<T> query, string searchTerm);

		/// <summary>
		/// Applies a date interval filter to the query.
		/// </summary>
		/// <param name="query">The original queryable data.</param>
		/// <param name="fromDate">The start date for the interval.</param>
		/// <param name="toDate">The end date for the interval.</param>
		/// <param name="dateProperty">The date property to filter on.</param>
		/// <returns>A queryable result with the date interval filter applied.</returns>
		IQueryable<T> ApplyDateInterval(IQueryable<T> query, DateTime? fromDate, DateTime? toDate);

		/// <summary>
		/// Applies a filtering operation to the query.
		/// </summary>
		/// <param name="query">The original queryable data.</param>
		/// <param name="filterField">The field to filter by.</param>
		/// <param name="filterValue">The value to filter for.</param>
		/// <returns>A queryable result with the filter applied.</returns>
		IQueryable<T> ApplyFilter(IQueryable<T> query, string filterField, string filterValue);

		/// <summary>
		/// Applies an ordering operation to the query.
		/// </summary>
		/// <param name="query">The original queryable data.</param>
		/// <param name="orderByField">The field to order by.</param>
		/// <param name="isAscending">True for ascending order, false for descending.</param>
		/// <returns>A queryable result with the ordering applied.</returns>
		IQueryable<T> ApplyOrder(IQueryable<T> query, string orderByField, bool isAscending);

		/// <summary>
		/// Applies pagination to the query.
		/// </summary>
		/// <param name="query">The original queryable data.</param>
		/// <param name="pageNumber">The page number (1-based).</param>
		/// <param name="pageSize">The number of items per page.</param>
		/// <returns>A tuple containing the total count of items and the paginated result.</returns>
		Task<(int TotalCount, IEnumerable<T> Items)> ApplyPaginationAsync(IQueryable<T> query, int pageNumber, int pageSize);

	}
}
