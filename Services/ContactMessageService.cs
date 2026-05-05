using modelMVC.Models;
using modelMVC.Repositories;
using System.Collections.Generic;
using System.Linq; //sort
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IRepository<ContactMessage> _repository;

        public ContactMessageService(IRepository<ContactMessage> repository)
        {
            _repository = repository;
        }

        public async Task SendMessageAsync(ContactMessage message)
        {
            await _repository.AddAsync(message);
            await _repository.SaveAsync();
        }

        // Metoda noua care preia mesajele si le sorteaza
        public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
        {
            var messages = await _repository.GetAllAsync();
            return messages.OrderByDescending(m => m.CreatedAt);
        }

        // Cauta mesajul dupa ID si il sterge
        public async Task DeleteMessageAsync(int id)
        {
            var message = await _repository.GetByIdAsync(id);
            if (message != null)
            {
                _repository.Delete(message);
                await _repository.SaveAsync(); // Salvam modificarea in baza de date
            }
        }
    }
}