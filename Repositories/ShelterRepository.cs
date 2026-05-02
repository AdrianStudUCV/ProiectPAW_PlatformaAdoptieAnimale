using AdoptABuddy.Models;
using modelMVC.Models;

namespace modelMVC.Repositories
{
    public class ShelterRepository : Repository<Shelter>
    {
        public ShelterRepository(AdoptBuddyContext context) : base(context)
        {
        }
    }
}