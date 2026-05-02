using Microsoft.AspNetCore.Mvc;
using modelMVC.Models;
using modelMVC.Services;
using System.Diagnostics;

namespace modelMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAnimalService _animalService;
        private readonly IContactMessageService _contactService;


        // Injectam serviciul prin constructor 
        public HomeController(IAnimalService animalService, IContactMessageService contactService)
        {
            _animalService = animalService;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            // Luam toate animalele si le trimitem pe cele mai recente 3
            var allAnimals = await _animalService.GetAnimalsForAdoption();
            var featuredAnimals = allAnimals.OrderByDescending(a => a.Id).Take(3);

            return View(featuredAnimals);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CumAdopt()
        {
            return View();
        }

        public IActionResult Povesti()
        {
            return View();
        }

        // --- AFISARE FORMULAR GOL (GET) ---
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // --- PRIMIRE SI SALVARE DATE (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken] // Protectie impotriva atacurilor CSRF
        public async Task<IActionResult> Contact(ContactMessage model)
        {
            // 1. Verificam daca validatile din clasa Model sunt respectate
            if (!ModelState.IsValid)
            {
                // Daca nu sunt respectate (ex: email gresit), ii trimitem inapoi formularul cu erorile marcate
                return View(model);
            }

            // 2. Daca totul e corect, trimitem mesajul catre serviciu pentru salvare
            await _contactService.SendMessageAsync(model);

            // 3. Afisam un mesaj de succes temporar (TempData)
            TempData["SuccessMessage"] = "Mesajul tău a fost trimis cu succes! Vom reveni în curând.";

            // 4. Resetam formularul
            return RedirectToAction(nameof(Contact));
        }

        public IActionResult ProfilAnimal()
        {
            return View();
        }

        public IActionResult ProfilUtilizator()
        {
            return View();
        }

        public IActionResult Adoptie()
        {
            return View();
        }
        // Repetă și pentru ProfilAnimal, ProfilUtilizator, etc.

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
