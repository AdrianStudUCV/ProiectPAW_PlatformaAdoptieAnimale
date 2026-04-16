using AdoptABuddy.Models;
using modelMVC.Repositories;

namespace modelMVC.Services
{
    public interface IShelterService
    {
        Task<IEnumerable<Shelter>> GetAllShelters();
        Task CreateShelter(Shelter shelter);
        Task<Shelter> GetShelterById(int id);
        Task UpdateShelter(Shelter shelter);
        Task DeleteShelter(int id);
    }

    public class ShelterService : IShelterService
    {
        private readonly IRepository<Shelter> _repository;
        public ShelterService(IRepository<Shelter> repository) { _repository = repository; }

        public async Task<IEnumerable<Shelter>> GetAllShelters() => await _repository.GetAllAsync();
        public async Task CreateShelter(Shelter shelter)
        {
            await _repository.AddAsync(shelter);
            await _repository.SaveAsync();
        }
        public async Task<Shelter> GetShelterById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateShelter(Shelter shelter)
        {
            _repository.Update(shelter);
            await _repository.SaveAsync();
        }

        public async Task DeleteShelter(int id)
        {
            var shelter = await _repository.GetByIdAsync(id);
            if (shelter != null)
            {
                _repository.Delete(shelter);
                await _repository.SaveAsync();
            }
        }
    }
}
