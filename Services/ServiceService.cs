using Framework.Exceptions;
using Framework.QueryParameters;
using TiktokLocalAPI.Contracts.Repositories;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.Service;
using TiktokLocalAPI.Core.Models.Service;
using TiktokLocalAPI.Core.OutDto.Service;

namespace TiktokLocalAPI.Services.Services
{
    /// <summary>
    /// Represents the service layer responsible for business logic related to services.
    /// </summary>
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly IUserRepo _userRepo;

        public ServiceService(IServiceRepo serviceRepo, IUserRepo userRepo)
        {
            _serviceRepo = serviceRepo;
            _userRepo = userRepo;
        }

        /// <inheritdoc/>
        public async Task<ServiceOutDto> CreateService(ServiceCreateDto dto, Guid userId)
        {
            // var user = await _userRepo.GetUser(userId);
            // if (user == null)
            //     throw new QlNotFoundException(ExceptionMessages.UserProfileNotFound);

            // Console.WriteLine($"userrr {user.Id}");
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new QlNotFoundException("Title is required to generate a slug.");

            string baseSlug = GenerateSlug(dto.Title);
            var slugExists = await _serviceRepo.GetService(baseSlug);

            if (slugExists != null)
                throw new QlNotFoundException(ExceptionMessages.TitleAlreadyExists);

            var service = new ServiceModel
            {
                Title = dto.Title,
                Type = dto.Type,
                Description = dto.Description,
                Category = dto.Category,
                Location = dto.Location,
                CompensationType = dto.CompensationType,
                CompensationAmount = dto.CompensationAmount,
                CompensationCurrency = dto.CompensationCurrency,
                compensationProduct = dto.compensationProduct,
                FollowerRequirement = dto.FollowerRequirement,
                Duration = dto.Duration,
                Requirements = dto.Requirements,
                UserId = userId,
                // User = user, // ✅ full user object
                Slug = baseSlug,
            };

            var createdService = await _serviceRepo.CreateService(service);
            return createdService.ToOutDto();
        }

        /// <inheritdoc/>
        public async Task DeleteService(Guid userId, Guid id)
        {
            var existing = await _serviceRepo.GetService(id);
            if (existing == null)
                throw new QlNotFoundException(ExceptionMessages.ServiceNotFound);

            if (existing.UserId != userId)
                throw new QlNotFoundException(ExceptionMessages.ServiceNotOwner);

            await _serviceRepo.DeleteService(id);
        }

        /// <inheritdoc/>
        public async Task<ServiceOutDto> GetServiceBySlug(string slug)
        {
            var service = await _serviceRepo.GetService(slug);
            if (service == null)
                throw new QlNotFoundException(ExceptionMessages.ServiceNotFound);

            return service.ToOutDto();
        }

        /// <inheritdoc/>
        public async Task<ServiceOutDto> GetServiceById(Guid id)
        {
            var service = await _serviceRepo.GetService(id);
            if (service == null)
                throw new QlNotFoundException(ExceptionMessages.ServiceNotFound);

            return service.ToOutDto();
        }

        /// <inheritdoc/>
        public async Task<FilteredServicesOutDto> GetAllServices(ServiceQueryParameters query)
        {
            var (total, services) = await _serviceRepo.GetAllServices(query);
            return new FilteredServicesOutDto
            {
                Services = services.Select(s => s.ToOutDto()).ToList(),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = total,
            };
        }

        /// <inheritdoc/>
        public async Task<ServiceOutDto> UpdateService(Guid userId, Guid id, ServiceEditDto dto)
        {
            var existing = await _serviceRepo.GetService(id);
            if (existing == null)
                throw new QlNotFoundException(ExceptionMessages.ServiceNotFound);
            if (existing.UserId != userId)
                throw new QlNotFoundException(ExceptionMessages.ServiceNotOwner);
            existing.Title = dto.Title ?? existing.Title;
            existing.Description = dto.Description ?? existing.Description;
            existing.Category = dto.Category ?? existing.Category;
            existing.Location = dto.Location ?? existing.Location;
            existing.CompensationType = dto.CompensationType ?? existing.CompensationType;
            existing.CompensationAmount = dto.CompensationAmount ?? existing.CompensationAmount;
            existing.CompensationCurrency =
                dto.CompensationCurrency ?? existing.CompensationCurrency;
            existing.compensationProduct = dto.compensationProduct ?? existing.compensationProduct;
            existing.FollowerRequirement = dto.FollowerRequirement ?? existing.FollowerRequirement;
            existing.Duration = dto.Duration ?? existing.Duration;
            existing.Requirements = dto.Requirements ?? existing.Requirements;

            await _serviceRepo.UpdateService(existing);

            return existing.ToOutDto();
        }

        /// <inheritdoc/>
        public async Task<List<ServiceOutDto>> GetMyServices(Guid userId)
        {
            var myServices = await _serviceRepo.GetMyServices(userId);
            return myServices.Select(s => s.ToOutDto()).ToList();
        }

        private string GenerateSlug(string title)
        {
            var slug = title.ToLowerInvariant().Trim();
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", ""); // remove non-alphanumeric
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-"); // replace spaces with hyphens
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-"); // collapse multiple hyphens
            return slug;
        }
    }
}
