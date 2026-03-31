using System;

namespace AdoptABuddy.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicationMessage { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        // Relatia cu Animalul dorit
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}