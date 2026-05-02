using AdoptABuddy.Models;
using modelMVC.Models;

namespace modelMVC.Repositories
{
    public class AdoptionStoryRepository : Repository<AdoptionStory>
    {
        public AdoptionStoryRepository(AdoptBuddyContext context) : base(context)
        {
        }
    }
}