using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.IServices
{
    public interface IAuthor
    {
        public Task<List<Author>> GetAllAuthor();
        public Task<ResponseModel> AddAuthor(Author category);
        public Task<ResponseModel> UpdateAuthor(Author category);
        public Task<ResponseModel> DeleteAuthor(int Id);
        public Task<Author> GetAuthorById(int Id);
    }
}
