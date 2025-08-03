using Framework.Enums;
using Framework.QueryParameters;
using TiktokLocalAPI.Core.Models.Service;

namespace TiktokLocalAPI.Contracts.Repositories
{
    /// <summary>
    /// Represents the repository interface for service operations, including service management, retrieval,
    /// password management, and role assignment.
    /// </summary>
    public interface IServiceRepo
    {
        /// <summary>
        /// Retrieves a service by email.
        /// </summary>
        /// <param name="slug">The email of the service to retrieve.</param>
        /// <returns>The service model if found; otherwise, null.</returns>
        Task<ServiceModel?> GetService(string? slug);

        /// <summary>
        /// Retrieves a service by GUID.
        /// </summary>
        /// <param name="id">The GUID of the service to retrieve.</param>
        /// <returns>The service model if found; otherwise, null.</returns>
        Task<ServiceModel?> GetService(Guid? id);

        Task<List<ServiceModel>> GetMyServices(Guid userId);

        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param name="service">The service model to create.</param>
        /// <returns>The created service model.</returns>
        Task<ServiceModel> CreateService(ServiceModel service);

        /// <summary>
        /// Deletes a service by their GUID.
        /// </summary>
        /// <param name="id">The GUID of the service to delete.</param>
        Task DeleteService(Guid id);

        /// <summary>
        /// Updates the profile information of a service.
        /// </summary>
        /// <param name="service">The service model with updated data.</param>
        Task UpdateService(ServiceModel service);

        /// <summary>
        /// Retrieves all services with pagination and filtering.
        /// </summary>
        /// <param name="queryParameters">Query parameters for filtering and pagination.</param>
        /// <returns>A tuple containing the total count and the list of services.</returns>
        Task<(int, IEnumerable<ServiceModel>)> GetAllServices(
            ServiceQueryParameters queryParameters
        );
    }
}
