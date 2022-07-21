using LibraryManagement.IServices;
using LibraryManagement.Models;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent student;
        public StudentController(IStudent _student)
        {
            student = _student;
        }
        [HttpGet]
        public async Task<ActionResult> ListStudent()
        {
            var model = await student.GetAllStudent();
            return View(model);
        }
        public async Task<ActionResult> AddStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SaveStudent(StudentViewModel model)
        {
            ResponseModel response = await student.AddStudent(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListStudent";
            }
            return RedirectToAction(retrunView);
        }
        public async Task<ActionResult> DeleteStudent(int Id)
        {
            ResponseModel response;
            try
            {
                response = await student.DeleteStudnet(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListStudent";
            }
            return RedirectToAction(retrunView);
        }

        public async Task<ActionResult> DetailsStudent(int Id)
        {
            var model = await student.GetStudentById(Id);
            return View(model);
        }
        public async Task<ActionResult> EditStudent(int Id)
        {
            var user = await student.GetStudentById(Id);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateStudent(StudentViewModel model)
        {
            ResponseModel response = await student.UpdateStudent(model);
            string retrunView = "";
            if (response.ResponseStatus == "Ok")
            {
                ViewBag.ResponseMessage = response.ResponseMessage;
                retrunView = "ListStudent";
            }

            return RedirectToAction(retrunView);

        }
    }
}
