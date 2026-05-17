using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace modelMVC.Models
{
    public class RegisterViewModel
    {
        [Required][EmailAddress] public string Email { get; set; }
        [Required][DataType(DataType.Password)] public string Password { get; set; }
        [Required] public string Role { get; set; } // Pentru a alege Admin sau User
        public IFormFile ProfilePicture { get; set; } // Fisierul incarcat
    }
}