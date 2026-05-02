using modelMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public interface IAdoptionStoryService
    {
        Task<IEnumerable<AdoptionStory>> GetAllStoriesAsync();
        Task CreateStoryAsync(AdoptionStory story);
    }
}