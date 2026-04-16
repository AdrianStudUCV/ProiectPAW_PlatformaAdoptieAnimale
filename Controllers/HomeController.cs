using Microsoft.AspNetCore.Mvc;
using modelMVC.Models;
using modelMVC.Services;
using System.Diagnostics;

namespace modelMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAnimalService _animalService;

        // Injectam serviciul prin constructor [cite: 215]
        public HomeController(IAnimalService animalService)
        {
            _animalService = animalService;
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

        public IActionResult Contact()
        {
            return View();
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
