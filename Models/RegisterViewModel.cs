using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace modelMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Numele complet este obligatoriu.")]
        [Display(Name = "Nume Complet")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Adresa de email este obligatorie.")]
        [EmailAddress(ErrorMessage = "Introduceți o adresă de email validă.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Parola trebuie să aibă cel puțin {2} caractere.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Selectarea unui rol este obligatorie.")]
        public string Role { get; set; } // "Admin" sau "User"

        [Display(Name = "Poză de Profil")]
        public IFormFile? ProfilePicture { get; set; } // Pentru incarcarea fisierului imagine
    }
}