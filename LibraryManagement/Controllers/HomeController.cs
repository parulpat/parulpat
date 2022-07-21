using LibraryManagement.IServices;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegisterUser registerUser;

        public HomeController(ILogger<HomeController> logger, IRegisterUser _registerUser)
        {
            _logger = logger;
            registerUser = _registerUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<ActionResult> LoginUser(UserLogin model)
        {
            ResponseModel response = await registerUser.Userlogin(model);
            string retrunView = "";
            string retuenController = "";
            if (response.ResponseStatus == "Ok")
            {
                
                retuenController = "UserRegister";
                retrunView = "ListUser";
            }
            else
            {
                ViewBag.ResponseMessage = "Invalid Emailid or Password";
                retuenController = "Home";
                retrunView = "Index";
            }

            return RedirectToAction(retrunView, retuenController);
        }
    }
}
