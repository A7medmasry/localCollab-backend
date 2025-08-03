using Framework.Extensions;
using Framework.Objects;
using Microsoft.AspNetCore.Mvc;
using TiktokLocalAPI.Contracts.Services;

namespace FileStorageAPI.Communication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="fileService">An <see cref="IFileService"/> instance used to manage operations related to files,such as creation, retrieval, updates, and deletion.</param>
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        [HttpGet("image/{guid}/{filename}")]
        public async Task<IActionResult> GetImageAvatar(string guid, string filename)
        {
            // var session = User.GetUserSession();
            // if (session == null)
            // {
            // 	return Unauthorized("Invalid session");
            // }

            var result = await _fileService.GetImage(guid, filename);

            return result;
            // try
            // {
            // 	var (stream, contentType) = await _fileService.ReadImageAsStream(guid, fullDate, filename);
            // 	Response.Headers.Add("Content-Disposition", "attachment; filename=protected-img");
            // 	return File(stream, contentType);
            // }
            // catch (FileNotFoundException)
            // {
            // 	return NotFound("Image not found");
            // }
        }
    }
}
