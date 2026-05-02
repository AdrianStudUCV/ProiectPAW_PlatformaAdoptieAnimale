using System;
using System.ComponentModel.DataAnnotations;

namespace modelMVC.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        // Validare: Numele este obligatoriu si trebuie sa aiba intre 3 si 50 caractere
        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 50 de caractere.")]
        public string Name { get; set; }

        // Validare: Trebuie sa fie un format corect de email (ex: nume@domeniu.com)
        [Required(ErrorMessage = "Adresa de email este obligatorie.")]
        [EmailAddress(ErrorMessage = "Te rugăm să introduci o adresă de email validă.")]
        public string Email { get; set; }

        // Validare: Mesajul trebuie sa aiba continut
        [Required(ErrorMessage = "Mesajul nu poate fi gol.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Mesajul trebuie să conțină cel puțin 10 caractere.")]
        public string Message { get; set; }

        // Salvam automat data si ora cand a fost trimis mesajul
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}