using LibraryManagement.IServices;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class RegisterUserService : IRegisterUser
    {
        private readonly LibraryManagementDbContext libraryManagementdb;
        private readonly CommonServices commonServices;
         public RegisterUserService(LibraryManagementDbContext _libraryManagement,CommonServices _commonServices) {
            libraryManagementdb = _libraryManagement;
            commonServices = _commonServices;
        }

        public async Task<ResponseModel> AddRegisteruser(RegisterViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var exitUser = (from user in libraryManagementdb.RegisterUser
                                where user.EmailId == model.EmailId
                                select user
                                ).FirstOrDefault();
                if(exitUser != null)
                {
                    var fileName = "";
                    if (model.ProfileImage != null)
                    {
                        fileName = await commonServices.UploadFile(model.ProfileImage);
                    }

                    int updaterow = 0;

                    RegisterUser registerUser = new RegisterUser()
                    {
                        UserName = model.UserName,
                        UserRole = model.UserRole,
                        EmailId = model.EmailId,
                        Password = model.Password,
                        ContactNo = model.ContactNo,
                        ProfileImagePath = fileName,
                        Active = true,
                        CreatedDate = DateTime.UtcNow,

                    };
                    libraryManagementdb.RegisterUser.Add(registerUser);
                    updaterow = libraryManagementdb.SaveChanges();
                    if (updaterow > 0)
                    {
                        responseModel.ResponseMessage = "User Added Successfully";
                        responseModel.ResponseStatus = "Ok";
                    }
                }
                else
                {
                    responseModel.ResponseMessage = "User Is Already Exists";
                    responseModel.ResponseStatus = "not Ok";
                }
                
            }
            catch (Exception ex)
            {
                responseModel.ResponseMessage = "Something went wrong";
            }
            return responseModel;

        }

        public async Task<ResponseModel> DeleteRegisterUser(int Id)
        {
            int updaterow = 0;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                var user = (from users in libraryManagementdb.RegisterUser
                            where users.UserId == Id
                            select new RegisterUser() {
                            UserId=users.UserId,
                            UserName = users.UserName,
                                UserRole = users.UserRole,
                                EmailId = users.EmailId,
                                Password = users.Password,
                                ContactNo = users.ContactNo,
                                ProfileImagePath = users.ProfileImagePath,
                                Active = false,
                                UpdateDate = DateTime.UtcNow,
                            }).FirstOrDefault();
                
                if (user != null)
                {
                    user.UserId = Id;
                    user.Active = false;
                    user.UpdateDate = DateTime.UtcNow;

                    libraryManagementdb.RegisterUser.Update(user);
                    updaterow = libraryManagementdb.SaveChanges();
                    if (updaterow > 0)
                    {
                        responseModel.ResponseMessage = "User Deleted Successfully";
                        responseModel.ResponseStatus = "Ok";
                    }
                }
                else
                {
                    responseModel.ResponseMessage = "User Not Found";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
            return responseModel;
        }

        public async Task<List<RegisterUser>> GetAllRegisterUser()
        {
            List<RegisterUser> model = new List<RegisterUser>();
            model =  (from registerUser in libraryManagementdb.RegisterUser 
                      join userRole in libraryManagementdb.UserRole on
                       registerUser.UserRole equals userRole.UserRoleId
                      where registerUser.Active == true 
                      select new RegisterUser()
                      {
                          UserName = registerUser.UserName,
                          EmailId = registerUser.EmailId,
                          ContactNo = registerUser.ContactNo,
                          ProfileImagePath = registerUser.ProfileImagePath,
                          Password = registerUser.Password,
                          UserId = registerUser.UserId,
                          Active = registerUser.Active
                      }
                      ).OrderByDescending(x=>x.UserId).ToList();
            return model;
        }

        public async Task<RegisterViewModel> GetRegisterById(int userId)
        {
            RegisterViewModel registerUser = new RegisterViewModel();
            registerUser = (from user in libraryManagementdb.RegisterUser
                            where user.UserId == userId && user.Active == true
                            select new RegisterViewModel()
                            {
                                UserName = user.UserName,
                                EmailId = user.EmailId,
                                ContactNo = user.ContactNo,
                                ProfileImagePath = user.ProfileImagePath,
                                UserRole = user.UserRole,
                                Password = user.Password,
                                UserId = user.UserId,
                                Active = user.Active
                            }).OrderByDescending(mod => mod.UserId).FirstOrDefault();
            return  registerUser;
        }
        public async Task<ResponseModel> EditRegisterUserDetail(RegisterViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            var fileName = "";
            int updaterow = 0;
            if (model.ProfileImage != null)
            {
                fileName = await commonServices.UploadFile(model.ProfileImage);
            }
            else
            {
                fileName = model.ProfileImagePath;
            }
            RegisterUser reguser = new RegisterUser();
            reguser = (from user in libraryManagementdb.RegisterUser
                                   where user.UserId == model.UserId && user.Active == true
                                   select new RegisterUser()
                                   {
                                       UserName = user.UserName,
                                       EmailId = user.EmailId,
                                       ContactNo = user.ContactNo,
                                       ProfileImagePath = user.ProfileImagePath,
                                       UserRole = user.UserRole,
                                       Password = user.Password,
                                       UserId = user.UserId,
                                       Active = user.Active
                                   }).OrderByDescending(mod => mod.UserId).FirstOrDefault();
            if(reguser != null)
            {
                reguser.UserName = model.UserName;
                reguser.UserRole = model.UserRole;
                reguser.EmailId = model.EmailId;
                reguser.Password = model.Password;
                reguser.ContactNo = model.ContactNo;
                reguser.ProfileImagePath = fileName;
                reguser.UpdateDate = DateTime.UtcNow;
                libraryManagementdb.RegisterUser.Update(reguser);
                updaterow = libraryManagementdb.SaveChanges();
                if(updaterow > 0)
                {
                    responseModel.ResponseMessage = "User Updated successfully";
                    responseModel.ResponseStatus = "Ok";
                }
            }
            else
            {
                responseModel.ResponseMessage = "Somethiong Went Wrong";
                responseModel.ResponseStatus = "";
            }
            return responseModel;
        }
        public async Task<ResponseModel> Userlogin(UserLogin model)
        {
            ResponseModel responseModel = new ResponseModel();
            var user = (from users in libraryManagementdb.RegisterUser
                        where users.EmailId == model.EmailId && users.Password == model.Password && users.Active==true
                        select new UserLogin { 
                            EmailId = users.EmailId,
                            Password = users.Password,
                            UserRole = users.UserRole
                        }).FirstOrDefault();
            if(user != null)
            {
                
                responseModel.ResponseStatus = "Ok";
             
            }
            return responseModel;
        }
    }
}
