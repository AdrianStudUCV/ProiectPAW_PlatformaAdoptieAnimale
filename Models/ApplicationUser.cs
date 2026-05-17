using Microsoft.AspNetCore.Identity;

namespace modelMVC.Models
{
    // Mostenim clasa de baza Identity pentru a-i adauga proprietati noi
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePictureUrl { get; set; } // Aici vom salva calea catre poza
    }
}