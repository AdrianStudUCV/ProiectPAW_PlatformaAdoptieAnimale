using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace modelMVC.Models
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Numele complet este obligatoriu.")]
        [Display(Name = "Nume Complet")]
        public string FullName { get; set; }

        public string? CurrentProfilePictureUrl { get; set; }

        [Display(Name = "Schimbă Poza de Profil")]
        public IFormFile? NewProfilePicture { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parola Curentă")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parola Nouă")]
        [StringLength(100, ErrorMessage = "Parola trebuie să aibă cel puțin {2} caractere.", MinimumLength = 6)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmă Parola Nouă")]
        [Compare("NewPassword", ErrorMessage = "Parolele noi nu se potrivesc.")]
        public string? ConfirmNewPassword { get; set; }
    }
}