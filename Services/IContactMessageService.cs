using modelMVC.Models;
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public interface IContactMessageService
    {
        Task SendMessageAsync(ContactMessage message);
    }
}