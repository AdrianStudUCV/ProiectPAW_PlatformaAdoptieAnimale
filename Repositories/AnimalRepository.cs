using AdoptABuddy.Models;
using Microsoft.EntityFrameworkCore;
namespace modelMVC.Repositories
{
    public class AnimalRepository : IRepository<Animal>
    {
        private readonly AdoptBuddyContext _context;
        public AnimalRepository(AdoptBuddyContext context) { _context = context; }

        public async Task<IEnumerable<Animal>> GetAllAsync() => await _context.Animals.Include(a => a.Category).Include(a => a.Shelter).ToListAsync();
        public async Task<Animal> GetByIdAsync(int id) => await _context.Animals.FindAsync(id);
        public async Task AddAsync(Animal entity) => await _context.Animals.AddAsync(entity);
        public void Update(Animal entity) => _context.Animals.Update(entity);
        public void Delete(Animal entity) => _context.Animals.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
