using LibraryManagement.IServices;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthor author;
        public AuthorController(IAuthor _author)
        {
            author = _author;
        }
        public async Task<IActionResult> ListAuthor()
        {
            var model = await author.GetAllAuthor();
            return View(model);
        }
        public async Task<IActionResult> AddAuthor()
        {
            return View();
        }
        public async Task<IActionResult> SaveAuthor(Author model)
        {
            ResponseModel response = await author.AddAuthor(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListAuthor";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<IActionResult> EditAuthor(int Id)
        {
            var model = await author.GetAuthorById(Id);
            return View(model);
        }
        public async Task<IActionResult> UpdateAuthor(Author model)
        {
            ResponseModel response = await author.UpdateAuthor(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListAuthor";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<IActionResult> DetailsAuthor(int Id)
        {
            var model = await author.GetAuthorById(Id);
            return View(model);
        }
        public async Task<IActionResult> DeleteAuthor(int Id)
        {
            ResponseModel response = await author.DeleteAuthor(Id);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListAuthor";
            }
            return RedirectToAction(retrunView);
        }
    }
}
