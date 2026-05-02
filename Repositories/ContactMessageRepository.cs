using AdoptABuddy.Models;
using modelMVC.Models;

namespace modelMVC.Repositories
{
    // Mostenim tot codul de Add, Delete, Save din clasa de baza!
    public class ContactMessageRepository : Repository<ContactMessage>
    {
        public ContactMessageRepository(AdoptBuddyContext context) : base(context)
        {
        }
    }
}