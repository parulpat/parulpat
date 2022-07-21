using LibraryManagement.IServices;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory category;
        public CategoryController (ICategory _category)
        {
            category = _category;
        }
        public async Task<IActionResult> ListCategory()
        {
            var model = await category.GetAllCategory();
            return View(model);
        }
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }
        public async Task<IActionResult> SaveCategory(Category model)
        {
            ResponseModel response = await category.AddCategory(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListCategory";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<IActionResult> EditCategory(int Id)
        {
            var model = await category.GetCategoryById(Id);
            return View(model);
        }
        public async Task<IActionResult> UpdateCategory(Category model)
        {
            ResponseModel response = await category.UpdateCategory(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListCategory";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<IActionResult> CategoryDetails(int Id)
        {
            var model = await category.GetCategoryById(Id);
            return View(model);
        }
        public async Task<IActionResult> CategoryDelete(int Id)
        {
            ResponseModel response = await category.DeleteCategory(Id);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListCategory";
            }
            return RedirectToAction(retrunView);
        }
    }
}
