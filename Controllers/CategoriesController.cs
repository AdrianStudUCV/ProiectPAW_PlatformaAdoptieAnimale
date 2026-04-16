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
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService; // Folosim interfata serviciului

        // Injectam serviciul in constructor 
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategories());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // --- DETALII (READ) ---
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // --- EDITARE (GET) ---
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // --- EDITARE (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // --- STERGERE (GET) ---
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // --- STERGERE (POST) ---
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
