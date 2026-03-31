using System.Collections.Generic;

namespace AdoptABuddy.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Ex: Caine, Pisica

        // Proprietate de navigare
        public ICollection<Animal>? Animals { get; set; }
    }
}