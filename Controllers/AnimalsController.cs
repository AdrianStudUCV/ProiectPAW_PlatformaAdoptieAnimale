using AdoptABuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modelMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace modelMVC.Controllers
{
    public class AnimalsController : Controller
    {
        // Aducem toate cele 3 servicii pentru a indeplini cerinta profesorului (Fara Context direct!)
        private readonly IAnimalService _animalService;
        private readonly ICategoryService _categoryService;
        private readonly IShelterService _shelterService;

        public AnimalsController(IAnimalService animalService, ICategoryService categoryService, IShelterService shelterService)
        {
            _animalService = animalService;
            _categoryService = categoryService;
            _shelterService = shelterService;
        }

        // --- CITIRE (READ) - Afiseaza tabelul ---
        public async Task<IActionResult> Index()
        {
            var animals = await _animalService.GetAnimalsForAdoption();
            return View(animals);
        }

        // --- CREARE (CREATE) - Afiseaza formularul ---
        // GET: Animals/Create
        public async Task<IActionResult> Create()
        {
            // Luam categoriile si adaposturile din baza de date
            var categories = await _categoryService.GetAllCategories(); 
            var shelters = await _shelterService.GetAllShelters();      

            // Le punem intr-un ViewBag sub forma de SelectList (pentru dropdown)
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.ShelterId = new SelectList(shelters, "Id", "Name");

            return View();
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _animalService.GetAnimalById(id.Value);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }
        // --- EDITARE (GET) - Afiseaza formularul cu datele incarcate ---
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _animalService.GetAnimalById(id);
            if (animal == null) return NotFound();

            // Trebuie sa populam meniurile dropdown, exact ca la Create, 
            // dar de data asta le spunem sa preselecteze valoarea pe care o are deja animalul
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name", animal.CategoryId);
            ViewData["ShelterId"] = new SelectList(await _shelterService.GetAllShelters(), "Id", "Name", animal.ShelterId);

            return View(animal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Animal animal)
        {
            if (id != animal.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _animalService.UpdateAnimal(animal); // Serviciul se ocupa de salvare 
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }
        // --- STERGERE (GET) - Afiseaza pagina unde te intreaba "Esti sigur?" ---
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _animalService.GetAnimalById(id);
            if (animal == null) return NotFound();

            return View(animal);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _animalService.DeleteAnimal(id); // Verbul DELETE 
            return RedirectToAction(nameof(Index));
        }

        // --- CREARE (CREATE) - Salveaza datele in baza de date ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                await _animalService.CreateAnimal(animal);
                return RedirectToAction(nameof(Index)); // Ne intoarce la lista dupa salvare
            }

            // Daca ceva a mers prost, reincarcam meniurile
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name", animal.CategoryId);
            ViewData["ShelterId"] = new SelectList(await _shelterService.GetAllShelters(), "Id", "Name", animal.ShelterId);

            return View(animal);
        }
    }
}