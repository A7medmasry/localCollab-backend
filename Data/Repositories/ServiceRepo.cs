using Framework.Enums;
using Framework.QueryParameters;
using Microsoft.EntityFrameworkCore;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Core.Models.Service;
using TiktokLocalAPI.Data.Database;

namespace TiktokLocalAPI.Data.Repositories
{
    /// <summary>
    /// Represents the repository for performing service-related operations.
    /// Provides functionalities such as creating, updating, deleting services,
    /// managing roles and statuses, and retrieving service data.
    /// </summary>
    public class ServiceRepo : IServiceRepo
    {
        private readonly TiktokLocalDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepo"/> class.
        /// </summary>
        /// <param name="context">The database context used for service-related operations.</param>
        public ServiceRepo(TiktokLocalDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<ServiceModel> CreateService(ServiceModel service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service;
        }

        /// <inheritdoc/>
        public async Task DeleteService(Guid id)
        {
            var service = await GetService(id);
            if (service == null)
                return;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateService(ServiceModel service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ServiceModel?> GetService(string? slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;
            return await _context.Services.FirstOrDefaultAsync(u => u.Slug == slug);
        }

        /// <inheritdoc/>
        public async Task<ServiceModel?> GetService(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return null;
            return await _context.Services.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<ServiceModel>> GetMyServices(Guid userId)
        {
            return await _context.Services.Where(s => s.UserId == userId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<(int, IEnumerable<ServiceModel>)> GetAllServices(
            ServiceQueryParameters queryParameters
        )
        {
            return await ApplyServicesFilter(_context.Services, queryParameters);
        }

        private async Task<(int, IEnumerable<ServiceModel>)> ApplyServicesFilter(
            IQueryable<ServiceModel> query,
            ServiceQueryParameters queryParameters
        )
        {
            // Search by Title, Slug, Category, or Location
            if (!string.IsNullOrEmpty(queryParameters.Search))
            {
                query = query.Where(s =>
                    s.Title.Contains(queryParameters.Search)
                    || s.Slug.Contains(queryParameters.Search)
                    || s.Category.Contains(queryParameters.Search)
                    || s.Location.Contains(queryParameters.Search)
                );
            }

            // Filter by Category
            if (!string.IsNullOrEmpty(queryParameters.Category))
            {
                query = query.Where(s => s.Category == queryParameters.Category);
            }

            // Filter by Location
            if (!string.IsNullOrEmpty(queryParameters.Location))
            {
                query = query.Where(s => s.Location == queryParameters.Location);
            }

            // Sorting
            if (!string.IsNullOrEmpty(queryParameters.OrderByField))
            {
                bool isAscending = queryParameters.IsAscending.GetValueOrDefault(true);
                query = queryParameters.OrderByField.ToLower() switch
                {
                    "title" => isAscending
                        ? query.OrderBy(s => s.Title)
                        : query.OrderByDescending(s => s.Title),
                    "category" => isAscending
                        ? query.OrderBy(s => s.Category)
                        : query.OrderByDescending(s => s.Category),
                    "createdat" => isAscending
                        ? query.OrderBy(s => s.Id)
                        : query.OrderByDescending(s => s.Id),
                    _ => query.OrderByDescending(s => s.Id),
                };
            }

            int totalCount = await query.CountAsync();
            var results = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (totalCount, results);
        }


    }
}
