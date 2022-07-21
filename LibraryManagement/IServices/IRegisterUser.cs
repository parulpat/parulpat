using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.IServices
{
    public interface IRegisterUser
    {
      public  Task<List<RegisterUser>> GetAllRegisterUser();
      public  Task<RegisterViewModel> GetRegisterById(int userId);
        public Task<ResponseModel> AddRegisteruser(RegisterViewModel model);
        public Task<ResponseModel> EditRegisterUserDetail(RegisterViewModel model);
        public Task<ResponseModel> DeleteRegisterUser(int Id);
        public Task<ResponseModel> Userlogin(UserLogin model);
    }
}
