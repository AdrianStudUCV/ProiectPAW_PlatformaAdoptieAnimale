using System.Collections.Generic;

namespace AdoptABuddy.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}