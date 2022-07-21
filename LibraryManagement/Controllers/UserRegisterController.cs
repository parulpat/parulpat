using LibraryManagement.IServices;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class UserRegisterController : Controller
    {
        // GET: UserRegisterController
        private readonly IRegisterUser registerUser;
        public UserRegisterController(IRegisterUser _registerUser)
        {
            registerUser = _registerUser;
        }
        [HttpGet]
        public async Task<ActionResult> ListUser()
        {
            var model = await registerUser.GetAllRegisterUser();
            return View(model);
        }
        public async Task<ActionResult> AddRegisteruser()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SaveRegisteruser(RegisterViewModel model)
        {
            ResponseModel response = await registerUser.AddRegisteruser(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListUser";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<ActionResult> DeleteRegisterUser(int Id)
        {
            ResponseModel response;
            try
            {
                response = await registerUser.DeleteRegisterUser(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListUser";
            }
            return RedirectToAction(retrunView);
        }

        public async Task<ActionResult> DetailsUser(int Id)
        {
            var model = await registerUser.GetRegisterById(Id);
            return View(model);
        }
        public async Task<ActionResult> EditRegisterUser(int Id)
        {
            var user = await registerUser.GetRegisterById(Id);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateRegisteruser(RegisterViewModel model)
        {
            ResponseModel response = await registerUser.EditRegisterUserDetail(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListUser";
            }
            
            return RedirectToAction(retrunView);

        }
       
        
    }
}
