using AdoptABuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using modelMVC.Models;
using modelMVC.Services;
using System.Threading.Tasks;

namespace modelMVC.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordService _medicalService;
        private readonly IAnimalService _animalService;

        // Injectam ambele servicii
        public MedicalRecordsController(IMedicalRecordService medicalService, IAnimalService animalService)
        {
            _medicalService = medicalService;
            _animalService = animalService;
        }

        // Afiseaza formularul gol
        public async Task<IActionResult> Create()
        {
            // Luam lista cu toate animalele
            var animals = await _animalService.GetAnimalsForAdoption(); // NOTA: daca metoda ta se numeste doar GetAllAsync(), modifica aici!

            // Le punem intr-un ViewBag ca sa construim dropdown-ul in HTML
            ViewBag.AnimalId = new SelectList(animals, "Id", "Name");
            return View();
        }

        // Primeste datele completate si le salveaza
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalRecord record)
        {
            if (ModelState.IsValid)
            {
                await _medicalService.CreateRecordAsync(record);

                // Magie: Dupa ce salvam, te trimitem direct pe pagina de Profil a acelui animal!
                return RedirectToAction("Details", "Animals", new { id = record.AnimalId });
            }

            // Daca a fost o eroare (ex: n-ai pus data), refacem dropdown-ul si ramanem pe pagina
            var animals = await _animalService.GetAnimalsForAdoption();
            ViewBag.AnimalId = new SelectList(animals, "Id", "Name", record.AnimalId);
            return View(record);
        }
    }
}