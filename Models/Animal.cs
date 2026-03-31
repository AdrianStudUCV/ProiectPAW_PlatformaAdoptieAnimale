using System.Collections.Generic;

namespace AdoptABuddy.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }

        // Relatia cu Categoria
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Relatia cu Adapostul
        public int ShelterId { get; set; }
        public Shelter? Shelter { get; set; }

        // Colectii catre tabelele copil
        public ICollection<MedicalRecord>? MedicalRecords { get; set; }
        public ICollection<AdoptionApplication>? AdoptionApplications { get; set; }
    }
}