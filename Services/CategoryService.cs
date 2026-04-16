using AdoptABuddy.Models;
using modelMVC.Repositories;

namespace modelMVC.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
       
        Task<Category> GetCategoryById(int id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        public CategoryService(IRepository<Category> repository) { _repository = repository; }

        public async Task<IEnumerable<Category>> GetAllCategories() => await _repository.GetAllAsync();
        public async Task CreateCategory(Category category)
        {
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateCategory(Category category)
        {
            _repository.Update(category);
            await _repository.SaveAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null)
            {
                _repository.Delete(category);
                await _repository.SaveAsync();
            }
        }
    }
}
