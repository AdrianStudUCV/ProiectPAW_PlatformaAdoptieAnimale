namespace modelMVC.Repositories
{
    using AdoptABuddy.Models;
    using Microsoft.EntityFrameworkCore;

    public class ShelterRepository : IRepository<Shelter>
    {
        private readonly AdoptBuddyContext _context;
        public ShelterRepository(AdoptBuddyContext context) { _context = context; }

        public async Task<IEnumerable<Shelter>> GetAllAsync() => await _context.Shelters.ToListAsync();
        public async Task<Shelter> GetByIdAsync(int id) => await _context.Shelters.FindAsync(id);
        public async Task AddAsync(Shelter entity) => await _context.Shelters.AddAsync(entity);
        public void Update(Shelter entity) => _context.Shelters.Update(entity);
        public void Delete(Shelter entity) => _context.Shelters.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
