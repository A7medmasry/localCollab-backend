using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using TiktokLocalAPI.Core.DTO.File;

namespace TiktokLocalAPI.Contracts.Services
{
    public interface IFileService
    {
        Task<string> CreateImage(UserSession requestor, BaseImageDto dto);
        Task<PhysicalFileResult> GetImage(string guid, string filename);
        Task<(MemoryStream, string)> ReadImageAsStream(
            string guid,
            string fullDate,
            string filename
        );
        Task<string> SaveImageToDisk(IFormFile file, Guid userId, string name);
    }
}
