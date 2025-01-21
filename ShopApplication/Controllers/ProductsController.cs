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
using Microsoft.IdentityModel.Tokens;
using ShopApplication.Models;

namespace ShopApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IService<Product> _service;
        private readonly IService<Category> _categoryService;
        public ProductsController(IService<Product> service,IService<Category> categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        // GET: Products
        public async Task<IActionResult> Index(ProductViewModelFilter? filter)
        {

            var query = _service.GetAll().AsQueryable();

            Console.WriteLine(filter);

            if(filter.CategoryId!= null)
            {
                query = query.Where(p=>p.CategoryId==filter.CategoryId.Value);
            }
            if (filter.MinPrice != null)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }
            if (filter.MaxPrice != null)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }
            if (filter.Aroma != null)
            {
                query = query.Where(p => p.Aroma.Contains(filter.Aroma));
            }


            var model = new ProductViewModelFilter
            {
                CategoryId = filter.CategoryId,
                MinPrice = filter.MinPrice,
                MaxPrice = filter.MaxPrice,
                Aroma = filter.Aroma,
                Categories = new SelectList(_categoryService.GetAll(), "Id", "Name"),
                Products = query.Include(p => p.Category).ToList(),
            };

            ViewData["message from controller"] = "Hello from the controller : )";

            return View(model);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _service.GetById(id.Value);

            var categories = _categoryService.GetAll().ToArray();
            Category category = categories[product.CategoryId]; 
            product.Category = category;
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Aroma,CategoryId")] Product product)
        {
            Category category= _categoryService.GetById(product.CategoryId);
            product.Category = category;
            if (!product.Name.IsNullOrEmpty()&&product.Price>0&&!product.Aroma.IsNullOrEmpty()&&_categoryService.EntityExists(product.CategoryId))
            {
               _service.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _service.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Aroma,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (!product.Name.IsNullOrEmpty() && product.Price > 0 && !product.Aroma.IsNullOrEmpty() && _categoryService.EntityExists(product.CategoryId))
            {
                try
                {
                    _service.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_service.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _service.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _service.GetById(id);
            if (product != null)
            {
                _service.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _service.EntityExists(id);
        }
    }
}
