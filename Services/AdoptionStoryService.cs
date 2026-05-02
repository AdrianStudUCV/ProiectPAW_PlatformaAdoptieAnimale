using modelMVC.Models;
using modelMVC.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public class AdoptionStoryService : IAdoptionStoryService
    {
        private readonly IRepository<AdoptionStory> _repository;

        public AdoptionStoryService(IRepository<AdoptionStory> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdoptionStory>> GetAllStoriesAsync()
        {
            // Preluam toate povestile din baza de date
            return await _repository.GetAllAsync();
        }
    }
}