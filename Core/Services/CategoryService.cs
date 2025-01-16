using Core.Repositories;
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
        private readonly IRepository<Category> _repository;
        public CategoryService(ApplicationDBContext db, IRepository<Category> repository)
        {
            _repository = repository;
            _db = db;
        }
        public async Task<Category> FindCategoryById(int id)
        {//await _db.Categories.FindAsync(id)
            return  _repository.GetById(id)
            ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories =  _repository.GetAll();
            return categories;
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var category = _repository.GetById(id);
            return category;
        }
        public async Task AddCategory(Category category)
        {
            //await _db.Categories.AddAsync(category);
            //await _db.SaveChangesAsync();

            _repository.Add(category);
        }
        public async Task EditCategory(Category category)
        {
            //_db.Categories.Update(category);
            //await _db.SaveChangesAsync();
            _repository.Update(category);
        }
        public async Task DeleteCategory(int id)
        {
            //var category = await FindCategoryById(id);
            //_db.Categories.Remove(category);
            //await _db.SaveChangesAsync();

            _repository.Delete(id);
        }

        public  bool CategoryExists(int id)
        {
            //_db.Categories.Any(e => e.Id == id);
            return  _db.Categories.Any(e => e.Id == id);
        } 
    }
}
