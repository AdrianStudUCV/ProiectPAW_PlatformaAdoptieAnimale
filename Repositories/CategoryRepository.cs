namespace modelMVC.Repositories
{
    using AdoptABuddy.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoryRepository : IRepository<Category>
    {
        private readonly AdoptBuddyContext _context;
        public CategoryRepository(AdoptBuddyContext context) { _context = context; }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();
        public async Task<Category> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);
        public async Task AddAsync(Category entity) => await _context.Categories.AddAsync(entity);
        public void Update(Category entity) => _context.Categories.Update(entity);
        public void Delete(Category entity) => _context.Categories.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
