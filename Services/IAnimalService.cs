using AdoptABuddy.Models;
using modelMVC.Repositories;

namespace modelMVC.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAnimalsForAdoption();
        Task<Animal> GetAnimalById(int id);
        Task CreateAnimal(Animal animal);
        Task UpdateAnimal(Animal animal);
        Task DeleteAnimal(int id);
    }

    public class AnimalService : IAnimalService
    {
        private readonly IRepository<Animal> _repository;
        public AnimalService(IRepository<Animal> repository) { _repository = repository; }

        public async Task<IEnumerable<Animal>> GetAnimalsForAdoption() => await _repository.GetAllAsync();
        public async Task CreateAnimal(Animal animal)
        {
            
            await _repository.AddAsync(animal);
            await _repository.SaveAsync();
        }
        public async Task<Animal> GetAnimalById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAnimal(Animal animal)
        {
            _repository.Update(animal);
            await _repository.SaveAsync();
        }

        public async Task DeleteAnimal(int id)
        {
            var animal = await _repository.GetByIdAsync(id);
            if (animal != null)
            {
                _repository.Delete(animal);
                await _repository.SaveAsync();
            }
        }
    }
}
