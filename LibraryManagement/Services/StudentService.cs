using LibraryManagement.IServices;
using LibraryManagement.Models;
using LibraryManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class StudentService : IStudent
    {
        private readonly CommonServices commonServices;
        private readonly LibraryManagementDbContext dbContext;
        public StudentService(LibraryManagementDbContext _dbContext, CommonServices _commonServices)
        {
            dbContext = _dbContext;
            commonServices = _commonServices;
        }
        public async Task<List<Student>> GetAllStudent()
        {
            List<Student> students = new List<Student>();
            students = (from std in dbContext.Student
                        where std.Active == true
                        select new Student() {
                            studentId=std.studentId,
                            studentName = std.studentName,
                            FatherName = std.FatherName,
                            Class = std.Class,
                            RollNo = std.RollNo,
                            Address = std.Address,
                            ProfileImagePath = std.ProfileImagePath
                        }
                        ).OrderByDescending(x => x.studentId).ToList();

            return students;
        }

        public async Task<StudentViewModel> GetStudentById(int id)
        {
            StudentViewModel students = new StudentViewModel();
            students = (from std in dbContext.Student
                        where std.Active == true && std.studentId == id
                        select new StudentViewModel()
                        {
                            studentId = std.studentId,
                            studentName = std.studentName,
                            FatherName = std.FatherName,
                            Class = std.Class,
                            RollNo = std.RollNo,
                            Address = std.Address,
                            ProfileImagePath = std.ProfileImagePath
                        }
                        ).FirstOrDefault();

            return students;
        }
        public async Task<ResponseModel> AddStudent(StudentViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var exitstudent = (from std in dbContext.Student
                                where std.RollNo == model.RollNo
                                select std
                                ).FirstOrDefault();
                if (exitstudent == null)
                {
                    var fileName = "";
                    if (model.ProfileImage != null)
                    {
                        fileName = await commonServices.UploadFile(model.ProfileImage);
                    }

                    int updaterow = 0;

                    Student student = new Student()
                    {
                        studentId = model.studentId,
                        studentName = model.studentName,
                        FatherName = model.FatherName,
                        Class = model.Class,
                        RollNo = model.RollNo,
                        Address = model.Address,
                        ProfileImagePath = fileName,
                        Active = true,

                    };
                    dbContext.Student.Add(student);
                    updaterow = dbContext.SaveChanges();
                    if (updaterow > 0)
                    {
                        responseModel.ResponseMessage = "Student Added Successfully";
                        responseModel.ResponseStatus = "Ok";
                    }
                }
                else
                {
                    responseModel.ResponseMessage = "Student Is Already Exists";
                    responseModel.ResponseStatus = "not Ok";
                }

            }
            catch (Exception ex)
            {
                responseModel.ResponseMessage = "Something went wrong";
            }
            return responseModel;
        }
        public async Task<ResponseModel> UpdateStudent(StudentViewModel model)
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
            Student std = new Student();
            std = (from student in dbContext.Student
                       where student.studentId == model.studentId && student.Active == true
                       select new Student()
                       {
                           studentId = model.studentId,
                           studentName = model.studentName,
                           FatherName = model.FatherName,
                           Class = model.Class,
                           RollNo = model.RollNo,
                           Address = model.Address,
                           ProfileImagePath = fileName,
                           Active = model.Active
                       }).FirstOrDefault();
            if (std != null)
            {
                std.studentId = model.studentId;
                std.studentName = model.studentName;
                std.FatherName = model.FatherName;
                std.Class = model.Class;
                std.RollNo = model.RollNo;
                std.Address = model.Address;
                std.ProfileImagePath = fileName;
                std.Active = true;
                dbContext.Student.Update(std);
                updaterow = dbContext.SaveChanges();
                if (updaterow > 0)
                {
                    responseModel.ResponseMessage = "Student Updated successfully";
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
        public async Task<ResponseModel> DeleteStudnet(int id)
        {
            int updaterow = 0;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                var student = (from std in dbContext.Student
                            where std.studentId == id
                            select new Student()
                            {
                                studentId = std.studentId,
                                studentName = std.studentName,
                                RollNo = std.RollNo,
                                Class = std.Class,
                                Address = std.Address,
                                FatherName = std.FatherName,
                                ProfileImagePath = std.ProfileImagePath,
                            }).FirstOrDefault();

                if (student != null)
                {
                    student.studentId = id;
                    student.Active = false;

                    dbContext.Student.Update(student);
                    updaterow = dbContext.SaveChanges();
                    if (updaterow > 0)
                    {
                        responseModel.ResponseMessage = "Student Deleted Successfully";
                        responseModel.ResponseStatus = "Ok";
                    }
                }
                else
                {
                    responseModel.ResponseMessage = "Student Not Found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseModel;
        }
    }
}
