using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICategoryService
    {
        public Task<Category> FindCategoryById(int id);
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task AddCategory(Category carInputModel);
        public Task EditCategory(Category carInputModel);
        public Task DeleteCategory(int id);

        public bool CategoryExists(int id);
    }
}
