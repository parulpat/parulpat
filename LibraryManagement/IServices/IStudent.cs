
using LibraryManagement.Models;
using LibraryManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.IServices
{
   public interface IStudent
    {
        public Task<List<Student>> GetAllStudent();
        public Task<StudentViewModel> GetStudentById(int id);
        public Task<ResponseModel> AddStudent(StudentViewModel model);
        public Task<ResponseModel> UpdateStudent(StudentViewModel model);
        public Task<ResponseModel> DeleteStudnet(int id);
    }
}
