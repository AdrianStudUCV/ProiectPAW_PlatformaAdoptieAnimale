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

        // Suprascriem metoda de GetById pentru a aduce si datele din celelalte tabele
        public override async Task<Animal> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Category)
                .Include(a => a.Shelter)
                .Include(a => a.MedicalRecords)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}