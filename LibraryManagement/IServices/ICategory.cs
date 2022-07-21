using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.IServices
{
   public interface ICategory
    {
        public Task<List<Category>> GetAllCategory();
        public Task<ResponseModel> AddCategory(Category category);
        public Task<ResponseModel> UpdateCategory(Category category);
        public Task<ResponseModel> DeleteCategory(int Id);
        public Task<Category> GetCategoryById(int Id);

    }
}
