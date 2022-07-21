using LibraryManagement.IServices;
using LibraryManagement.Models;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook book;
        private readonly LibraryManagementDbContext dbContext;
        public BookController(IBook _book,LibraryManagementDbContext _dbContext)
        {
            book = _book;
            dbContext = _dbContext;
        }
        public async Task<IActionResult> ListBook()
        {
            var model = await book.GetAllBook();
            return View(model);
        }
        public async Task<IActionResult> CreateBook()
        {
            ViewBag.Category = (from cat in dbContext.Category
                                where cat.ActiveStatus == true
                                select new Category()
                                {
                                    CategoryId = cat.CategoryId,
                                    CategoryName = cat.CategoryName
                                }).ToList();
            ViewBag.Author = (from auth in dbContext.Author
                                where auth.ActiveStatus == true
                                select new Author()
                                {
                                    AuthorId = auth.AuthorId,
                                    AuthorName = auth.AuthorName
                                }).ToList();
            return View();
        }
        public async Task<ActionResult> SaveBook(BookViewModel model)
        {
            ResponseModel response = await book.AddBook(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListBook";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<ActionResult> DetailsBook(int Id)
        {
            BookViewModel model = await book.GetBookById(Id);
            return View(model);
        }
        public async Task<ActionResult> DeleteBook(int Id)
        {
            ResponseModel response = await book.DeletBook(Id);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListBook";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<ActionResult> EditBook(int Id)
        {
            BookViewModel model = await book.GetBookById(Id);
            ViewBag.Category = (from cat in dbContext.Category
                                where cat.ActiveStatus == true
                                select new Category()
                                {
                                    CategoryId = cat.CategoryId,
                                    CategoryName = cat.CategoryName
                                }).ToList();
            ViewBag.Author = (from auth in dbContext.Author
                              where auth.ActiveStatus == true
                              select new Author()
                              {
                                  AuthorId = auth.AuthorId,
                                  AuthorName = auth.AuthorName
                              }).ToList();
            
            return View(model);
        }
        public async Task<ActionResult> UpdateBook(BookViewModel model)
        {
            ResponseModel response = await book.UpdateBook(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListBook";
            }
            return RedirectToAction(retrunView);
        }
    }
}
