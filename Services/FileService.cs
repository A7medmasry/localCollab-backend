using Framework.Exceptions;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TiktokLocalAPI.Contracts.Services;
using TiktokLocalAPI.Core.Constants;
using TiktokLocalAPI.Core.DTO.File;

namespace TiktokLocalAPI.Services.Services
{
    public class FileService : IFileService
    {
        public FileService() { }

        public async Task<string> CreateImage(UserSession requestor, BaseImageDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                throw new QlBadRequestException(ExceptionMessages.NoFileUploaded);
            }
            return await SaveImageToDisk(dto.File, requestor.Guid, "avator");
        }

        private void ValidateFileExtension(string fileName)
        {
            var allowedExtensions = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".pdf",
                ".txt",
                ".doc",
                ".docx",
                ".xls",
                ".xlsx",
                ".csv",
            };
            var fileExtension = Path.GetExtension(fileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new QlBadRequestException(ExceptionMessages.InvalidFileFormat);
            }
        }

        // private async Task<string> SaveImageToDisk(IFormFile file, Guid userId, string name)
        // {
        //     var uploadsFolder = Path.Combine(
        //         Directory.GetCurrentDirectory(),
        //         "uploads",
        //         userId.ToString()
        //     );
        //     Directory.CreateDirectory(uploadsFolder);

        //     // ❌ Delete all existing files starting with "Image"
        //     foreach (var existing in Directory.GetFiles(uploadsFolder, "Image*"))
        //     {
        //         System.IO.File.Delete(existing);
        //     }

        //     // 📅 Generate filename with today's date
        //     string dateSuffix = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        //     string extension = Path.GetExtension(file.FileName); // includes the dot
        //     string fileName = $"Image_{dateSuffix}{extension}";
        //     string filePath = Path.Combine(uploadsFolder, fileName);

        //     // ✅ Save file
        //     if (IsImage(file.FileName))
        //     {
        //         await ProcessAndSaveImage(file, filePath);
        //     }
        //     else
        //     {
        //         await SaveRegularFile(file, filePath);
        //     }

        //     return fileName; // return saved filename like Image_20250609.png
        // }
        public async Task<string> SaveImageToDisk(IFormFile file, Guid userId, string name)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "uploads",
                userId.ToString()
            );
            Directory.CreateDirectory(uploadsFolder);

            // ❌ Delete all existing files starting with the name prefix
            foreach (var existing in Directory.GetFiles(uploadsFolder, $"{name}*"))
            {
                System.IO.File.Delete(existing);
            }

            // 📅 Generate filename with today's date
            string dateSuffix = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string extension = Path.GetExtension(file.FileName); // includes the dot
            string fileName = $"{name}_{dateSuffix}{extension}";
            string filePath = Path.Combine(uploadsFolder, fileName);

            // ✅ Save file
            if (IsImage(file.FileName))
            {
                await ProcessAndSaveImage(file, filePath);
            }
            else
            {
                await SaveRegularFile(file, filePath);
            }

            return fileName; // example: Avatar_20250729.png
        }

        private string CreateUploadsFolder(Guid userId, DateTime now)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "uploads",
                userId.ToString(),
                $"{now.Year}_{now.Month}_{now.Day}"
            );
            Directory.CreateDirectory(uploadsFolder);
            return uploadsFolder;
        }

        private async Task ProcessAndSaveImage(IFormFile file, string filePath)
        {
            using var image = await Image.LoadAsync(file.OpenReadStream());
            if (image.Width > 1024 || image.Height > 1024)
            {
                image.Mutate(x =>
                    x.Resize(
                        new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(1024, 1024) }
                    )
                );
            }

            var format =
                image.Metadata.DecodedImageFormat
                ?? throw new QlInvalidOperationException(ExceptionMessages.UnknownFormat);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.SaveAsync(stream, format);
        }

        private async Task SaveRegularFile(IFormFile file, string filePath)
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        private string GenerateFilePath(Guid userId, int fileId, string fileExtension)
        {
            return $"{userId}/{DateTime.UtcNow.Year}_{DateTime.UtcNow.Month}_{DateTime.UtcNow.Day}/{fileId}{fileExtension}";
        }

        public async Task<PhysicalFileResult> GetImage(string guid, string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", guid, filename);

            var fileExists = await Task.Run(() => System.IO.File.Exists(filePath));
            if (!fileExists)
            {
                throw new QlNotFoundException(ExceptionMessages.ImageNotFound);
            }

            var provider = new FileExtensionContentTypeProvider();
            if (
                !provider.TryGetContentType(filePath, out string? contentType)
                || string.IsNullOrEmpty(contentType)
            )
            {
                contentType = "application/octet-stream"; // Ensure a non-null default value
            }

            return new PhysicalFileResult(filePath, contentType);
        }

        public async Task<(MemoryStream, string)> ReadImageAsStream(
            string guid,
            string fullDate,
            string filename
        )
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "uploads",
                guid,
                fullDate,
                filename
            );

            if (!System.IO.File.Exists(filePath))
                throw new FileNotFoundException("Image not found");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var provider = new FileExtensionContentTypeProvider();
            if (
                !provider.TryGetContentType(filePath, out string? contentType)
                || string.IsNullOrEmpty(contentType)
            )
            {
                contentType = "application/octet-stream"; // Ensure a non-null default value
            }
            return (memory, contentType);
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }

            return $"{size:0.##}{sizes[order]}";
        }

        private bool IsImage(string extension)
        {
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            return imageExtensions.Contains(extension);
        }

        private async Task DeleteImage(string guid, string fullDate, string filename)
        {
            var folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "uploads",
                guid,
                fullDate
            );
            var filePath = Path.Combine(folderPath, filename);

            var fileExists = await Task.Run(() => System.IO.File.Exists(filePath));
            if (!fileExists)
            {
                throw new QlNotFoundException(ExceptionMessages.ImageNotFound);
            }

            try
            {
                await Task.Run(() => System.IO.File.Delete(filePath));
                CleanupEmptyFolders(folderPath);
            }
            catch (Exception ex)
            {
                throw new QlInvalidOperationException($"Error deleting image: {ex.Message}");
            }
        }

        private void CleanupEmptyFolders(string folderPath)
        {
            string mainUploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            while (
                !string.IsNullOrWhiteSpace(folderPath)
                && Directory.Exists(folderPath)
                && folderPath != mainUploadFolder
            )
            {
                if (!Directory.EnumerateFileSystemEntries(folderPath).Any()) // Check if folder is empty
                {
                    Directory.Delete(folderPath);

                    var parent = Directory.GetParent(folderPath); // Get parent directory safely
                    folderPath = parent?.FullName ?? string.Empty; // Ensure folderPath is never null
                }
                else
                {
                    break;
                }
            }
        }
    }
}
