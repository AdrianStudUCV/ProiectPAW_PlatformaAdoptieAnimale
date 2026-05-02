using modelMVC.Models;
using modelMVC.Repositories;
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IRepository<ContactMessage> _repository;

        // Injectam Repository-ul generic pentru mesaje
        public ContactMessageService(IRepository<ContactMessage> repository)
        {
            _repository = repository;
        }

        public async Task SendMessageAsync(ContactMessage message)
        {
            await _repository.AddAsync(message);
            await _repository.SaveAsync();
        }
    }
}