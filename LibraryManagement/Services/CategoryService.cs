using LibraryManagement.IServices;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class CategoryService : ICategory
    {
        private readonly LibraryManagementDbContext dbContext;
        public CategoryService(LibraryManagementDbContext _dbcontext)
        {
            dbContext = _dbcontext;
        }
        public async Task<ResponseModel> AddCategory(Category category)
        {
            ResponseModel response = new ResponseModel();
            int updaterow = 0;
            dbContext.Category.Add(category);
            updaterow = await dbContext.SaveChangesAsync();
            if(updaterow > 0)
            {
                response.ResponseMessage = "Category added Successfully";
                response.ResponseStatus = "Ok";
            }
            return response;
        }

        public async Task<ResponseModel> DeleteCategory(int Id)
        {
            int updaterow = 0;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                var cate = (from cat in dbContext.Category
                            where cat.CategoryId == Id
                            select new Category()
                            {
                                CategoryId = cat.CategoryId,
                                CategoryName = cat.CategoryName
                            }).FirstOrDefault();

                if (cate != null)
                {
                    cate.CategoryId = Id;
                    cate.ActiveStatus = false;

                    dbContext.Category.Update(cate);
                    updaterow = dbContext.SaveChanges();
                    if (updaterow > 0)
                    {
                        responseModel.ResponseMessage = "Category Deleted Successfully";
                        responseModel.ResponseStatus = "Ok";
                    }
                }
                else
                {
                    responseModel.ResponseMessage = "User Not Found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseModel;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var cate = (from cates in dbContext.Category
                        where cates.ActiveStatus== true
                        select new Category()
                        {
                            CategoryName = cates.CategoryName,
                            CategoryId = cates.CategoryId
                        }).ToList() ;
            return cate;
        }

        public async Task<Category> GetCategoryById(int Id)
        {
            var cate = dbContext.Category.Find(Id);
            return cate;
        }

        public async Task<ResponseModel> UpdateCategory(Category category)
        {
            ResponseModel response = new ResponseModel();
            int updaterow = 0;
            dbContext.Category.Update(category);
            updaterow = await dbContext.SaveChangesAsync();
            if (updaterow > 0)
            {
                response.ResponseMessage = "Category Updated Successfully";
                response.ResponseStatus = "Ok";
            }
            return response;
        }
    }
}
