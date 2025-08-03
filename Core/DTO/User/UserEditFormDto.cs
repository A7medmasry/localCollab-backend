using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TiktokLocalAPI.Core.DTO.User
{
    public class UserEditFormDto
    {
        public string Dto { get; set; }

        public IFormFile? AvatarFile { get; set; }

        public IFormFile? BusinessLogoFile { get; set; }

        public IFormFile? VerificationDocumentsFile { get; set; }
    }
}
