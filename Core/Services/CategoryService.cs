using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDBContext _db;
        public CategoryService(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<Category> FindCategoryById(int id)
        {
            return await _db.Categories.FindAsync(id)
            ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await _db.Categories.ToListAsync();
            return categories;
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await FindCategoryById(id);
            return category;
        }
        public async Task AddCategory(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
        }
        public async Task EditCategory(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteCategory(int id)
        {
            var category = await FindCategoryById(id);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

        public  bool CategoryExists(int id)
        {
            //_db.Categories.Any(e => e.Id == id);
            return  _db.Categories.Any(e => e.Id == id);
        } 
    }
}
