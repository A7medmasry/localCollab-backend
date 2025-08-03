using Framework.QueryParameters;
using TiktokLocalAPI.Core.DTO.Service;
using TiktokLocalAPI.Core.OutDto.Service;

namespace TiktokLocalAPI.Contracts.Services
{
    public interface IServiceService
    {
        Task<ServiceOutDto> CreateService(ServiceCreateDto dto, Guid userId);
        Task DeleteService(Guid userId, Guid id);
        Task<ServiceOutDto> GetServiceBySlug(string slug);
        Task<ServiceOutDto> GetServiceById(Guid id);
        Task<FilteredServicesOutDto> GetAllServices(ServiceQueryParameters query);
        Task<List<ServiceOutDto>> GetMyServices(Guid userId);
        Task<ServiceOutDto> UpdateService(Guid userId, Guid id, ServiceEditDto dto);
    }
}
