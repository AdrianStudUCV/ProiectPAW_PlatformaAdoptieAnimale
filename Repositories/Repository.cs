using AdoptABuddy.Models;
using Microsoft.EntityFrameworkCore;
using modelMVC.Models; // Ajusteaza namespace-ul la proiectul tau
using System.Collections.Generic;
using System.Threading.Tasks;

namespace modelMVC.Repositories
{
    // Implementeaza metodele pentru ORICE tip de clasa (T)
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AdoptBuddyContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AdoptBuddyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>(); // Aici alege tabelul corect
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public virtual void Update(T entity) => _dbSet.Update(entity);

        public virtual void Delete(T entity) => _dbSet.Remove(entity);

        public virtual async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}