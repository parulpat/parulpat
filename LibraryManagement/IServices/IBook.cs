using LibraryManagement.Models;
using LibraryManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.IServices
{
   public interface IBook
    {
        public  Task<List<BookViewModel>> GetAllBook();
        public Task<ResponseModel> AddBook(BookViewModel model);
        public Task<ResponseModel> UpdateBook(BookViewModel model);
        public Task<BookViewModel> GetBookById(int Id);
        public Task<ResponseModel> DeletBook(int Id);
    }
}
