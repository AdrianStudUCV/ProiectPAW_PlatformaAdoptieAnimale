using System;
using System.ComponentModel.DataAnnotations;

namespace modelMVC.Models
{
    public class AdoptionStory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele animalului este obligatoriu.")]
        [StringLength(50)]
        public string AnimalName { get; set; }

        [Required(ErrorMessage = "Numele familiei/adoptatorului este obligatoriu.")]
        [StringLength(100)]
        public string AdopterName { get; set; }

        [Required(ErrorMessage = "Te rugăm să scrii povestea.")]
        public string StoryText { get; set; }

        public string ImageUrl { get; set; } // Calea catre poza (ex: /images/poveste1.jpg)

        // Data la care a avut loc adoptia
        [Required]
        public DateTime AdoptionDate { get; set; }
    }
}