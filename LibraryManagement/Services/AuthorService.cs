
using LibraryManagement.IServices;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class AuthorService : IAuthor
    {
        private readonly LibraryManagementDbContext dbContext;
        public AuthorService(LibraryManagementDbContext _dbcontext)
        {
            dbContext = _dbcontext;
        }
        public async Task<ResponseModel> AddAuthor(Author model)
        {
            ResponseModel response = new ResponseModel();
            int updaterow = 0;
            dbContext.Author.Add(model);
            updaterow = await dbContext.SaveChangesAsync();
            if (updaterow > 0)
            {
                response.ResponseMessage = "Author added Successfully";
                response.ResponseStatus = "Ok";
            }
            return response;
        }

        public async Task<ResponseModel> DeleteAuthor(int Id)
        {
            int updaterow = 0;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                var auth = (from author in dbContext.Author
                            where author.AuthorId == Id
                            select new Author()
                            {
                                AuthorId = author.AuthorId,
                                AuthorName = author.AuthorName
                            }).FirstOrDefault();

                if (auth != null)
                {
                    auth.AuthorId = Id;
                    auth.ActiveStatus = false;

                    dbContext.Author.Update(auth);
                    updaterow = dbContext.SaveChanges();
                    if (updaterow > 0)
                    {
                        responseModel.ResponseMessage = "Author Deleted Successfully";
                        responseModel.ResponseStatus = "Ok";
                    }
                }
                else
                {
                    responseModel.ResponseMessage = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseModel;
        }

        public async Task<List<Author>> GetAllAuthor()
        {
            var auth = (from author in dbContext.Author
                        where author.ActiveStatus == true
                        select new Author()
                        {
                            AuthorName = author.AuthorName,
                            AuthorId = author.AuthorId
                        }).ToList();
            return auth;
        }

        public async Task<Author> GetAuthorById(int Id)
        {
            var auth = dbContext.Author.Find(Id);
            return auth;
        }

        public async Task<ResponseModel> UpdateAuthor(Author model)
        {

            ResponseModel response = new ResponseModel();
            int updaterow = 0;
            dbContext.Author.Update(model);
            updaterow = await dbContext.SaveChangesAsync();
            if (updaterow > 0)
            {
                response.ResponseMessage = "Author Updated Successfully";
                response.ResponseStatus = "Ok";
            }
            return response;
        }
    }
}
