using System.Security.Claims;
using Framework.Extensions;
using Framework.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.DTO.Service;

namespace TiktokLocalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ServiceCreateDto dto)
        {
            var session = User.GetUserSession();
            if (session == null || session.Guid == Guid.Empty)
                return Unauthorized();

            var result = await _serviceService.CreateService(dto, session.Guid);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceEditDto dto)
        {
            var session = User.GetUserSession();
            if (session == null || session.Guid == Guid.Empty)
                return Unauthorized();

            var result = await _serviceService.UpdateService(session.Guid, id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var session = User.GetUserSession();
            if (session == null || session.Guid == Guid.Empty)
                return Unauthorized();

            await _serviceService.DeleteService(session.Guid, id);
            return NoContent();
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> MyServices()
        {
            var session = User.GetUserSession();
            if (session == null || session.Guid == Guid.Empty)
                return Unauthorized();

            var services = await _serviceService.GetMyServices(session.Guid);
            return Ok(services);
        }

        [HttpGet("my/{id}")]
        public async Task<IActionResult> MyServicesById(Guid id)
        {
            var services = await _serviceService.GetMyServices(id);
            return Ok(services);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ServiceQueryParameters queryParameters)
        {
            var services = await _serviceService.GetAllServices(queryParameters);
            return Ok(services);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _serviceService.GetServiceById(id);
            return Ok(service);
        }

        [HttpGet("slug/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var service = await _serviceService.GetServiceBySlug(slug);
            return Ok(service);
        }
    }
}
