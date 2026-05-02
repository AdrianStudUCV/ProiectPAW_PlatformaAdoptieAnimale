using AdoptABuddy.Models;
using Microsoft.EntityFrameworkCore;
using modelMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace modelMVC.Repositories
{
    public class AnimalRepository : Repository<Animal>
    {
        public AnimalRepository(AdoptBuddyContext context) : base(context)
        {
        }

        // Suprascriem DOAR metoda de get pentru a adauga Includes
        public override async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _dbSet
                .Include(a => a.Category)
                .Include(a => a.Shelter)
                .ToListAsync();
        }
    }
}