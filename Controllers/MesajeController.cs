using Microsoft.AspNetCore.Mvc;
using modelMVC.Services;
using System.Threading.Tasks;

namespace modelMVC.Controllers
{
    public class MesajeController : Controller
    {
        private readonly IContactMessageService _messageService;

        public MesajeController(IContactMessageService messageService)
        {
            _messageService = messageService;
        }

        // Aceasta metoda va raspunde la link-ul /Mesaje
        public async Task<IActionResult> Index()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return View(messages);
        }
        // Metoda care primeste ID-ul mesajului si il sterge
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.DeleteMessageAsync(id);

            // Dupa ce l-a sters, dam un refresh la pagina (ne intoarcem la Index)
            return RedirectToAction(nameof(Index));
        }
    }
}