using Microsoft.AspNetCore.Mvc;
using modelMVC.Models;
using modelMVC.Services;
using System.Threading.Tasks;

namespace modelMVC.Controllers
{
    public class AdoptionStoriesController : Controller
    {
        private readonly IAdoptionStoryService _storyService;

        public AdoptionStoriesController(IAdoptionStoryService storyService)
        {
            _storyService = storyService;
        }

        // Afiseaza lista cu povesti (pentru admin)
        public async Task<IActionResult> Index()
        {
            var stories = await _storyService.GetAllStoriesAsync();
            return View(stories);
        }

        // Afiseaza formularul GOL de creare
        public IActionResult Create()
        {
            return View();
        }

        // Primeste datele din formular si le salveaza
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionStory story)
        {
            if (ModelState.IsValid)
            {
                await _storyService.CreateStoryAsync(story);
                return RedirectToAction(nameof(Index)); // Ne intoarce la lista
            }
            return View(story); // Daca sunt erori, ramane pe pagina
        }
    }
}