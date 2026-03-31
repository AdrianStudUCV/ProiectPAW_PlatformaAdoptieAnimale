using System;

namespace AdoptABuddy.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string TreatmentName { get; set; } // Ex: Vaccin Antirabic
        public DateTime DateAdministered { get; set; }

        // Relatia cu Animalul
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}