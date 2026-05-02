using AdoptABuddy.Models;
using modelMVC.Models;

namespace modelMVC.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(AdoptBuddyContext context) : base(context)
        {
        }
    }
}