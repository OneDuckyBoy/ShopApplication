using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using Models;
using Core.Services;

namespace ShopApplication.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ApplicationDBContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {

            var categories = await _categoryService.GetAllCategories();

            ViewBag.Categories = new SelectList(categories, "Id", "Name"/*,"select category", null*/);
            return View(await _categoryService.GetAllCategories());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var category = await _categoryService.FindCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create( Category category)
        {
            if (category!=null)
            {
                _categoryService.AddCategory(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var category = await _categoryService.FindCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,  Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.EditCategory(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var category = await _categoryService.FindCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = _categoryService.FindCategoryById(id).Result;
            if (category != null)
            {
                _categoryService.DeleteCategory(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
           return _categoryService.CategoryExists(id);
        }
    }
}
