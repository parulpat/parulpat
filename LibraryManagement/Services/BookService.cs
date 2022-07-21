using LibraryManagement.IServices;
using LibraryManagement.Models;
using LibraryManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class BookService : IBook
    {
        private readonly LibraryManagementDbContext dbContext;
        public BookService(LibraryManagementDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<ResponseModel> AddBook(BookViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                
                int updaterow = 0;

                Book books = new Book()
                {
                    BookId = model.BookId,
                    BookTitle = model.BookTitle,
                    BookPublication = model.BookPublication,
                    Copies = model.Copies,
                    PublisherName = model.PublisherName,
                    ISBNNo = model.ISBNNo,
                    CategoryId = model.CategoryId,
                    AuthorId = model.AuthorId,
                    CopyRightYear = model.CopyRightYear,
                    ActiveStatus = true
                };
                dbContext.Book.Add(books);
                updaterow = dbContext.SaveChanges();
                if (updaterow > 0)
                {
                    responseModel.ResponseMessage = "Book Added Successfully";
                    responseModel.ResponseStatus = "Ok";
                }
            }
            catch (Exception ex)
            {
                responseModel.ResponseMessage = "Something went wrong";
            }
            return responseModel;
        }

        public async Task<ResponseModel> DeletBook(int Id)
        {
            int updaterow = 0;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                var books = (from book in dbContext.Book
                            where book.BookId == Id
                            select new Book() {
                                BookId = book.BookId,
                                BookTitle = book.BookTitle,
                                BookPublication = book.BookPublication,
                                Copies = book.Copies,
                                PublisherName = book.PublisherName,
                                ISBNNo = book.ISBNNo,
                                CategoryId = book.CategoryId,
                                AuthorId = book.AuthorId,
                                CopyRightYear = book.CopyRightYear,
                                ActiveStatus = book.ActiveStatus
                            }).FirstOrDefault();

                if (books != null)
                {
                    books.BookId = Id;
                    books.ActiveStatus = false;
                    dbContext.Book.Update(books);
                    updaterow = dbContext.SaveChanges();
                    if (updaterow > 0)
                    {
                        responseModel.ResponseMessage = "Book Deleted Successfully";
                        responseModel.ResponseStatus = "Ok";
                    }
                }
                else
                {
                    responseModel.ResponseMessage = "Book Not Found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseModel;
        }

        public async Task<List<BookViewModel>> GetAllBook()
        {
            List<BookViewModel> bookViewModels = new List<BookViewModel>();
            try
            {
                bookViewModels = (from book in dbContext.Book
                                     join cate in dbContext.Category
                                     on book.CategoryId equals cate.CategoryId
                                  join auth in dbContext.Author
                                 on book.AuthorId equals auth.AuthorId
                                  where book.ActiveStatus == true
                                     select new BookViewModel()
                                     {
                                         BookId = book.BookId,
                                         BookTitle = book.BookTitle,
                                         BookPublication = book.BookPublication,
                                         Copies = book.Copies,
                                         PublisherName = book.PublisherName,
                                         ISBNNo = book.ISBNNo,
                                         CategoryName = cate.CategoryName,
                                         AuthorName = auth.AuthorName,
                                         CopyRightYear = book.CopyRightYear,
                                         ActiveStatus = book.ActiveStatus
                                     }
                                   ).OrderByDescending(mod => mod.BookId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(bookViewModels).ConfigureAwait(false);
        }

        public async Task<BookViewModel> GetBookById(int Id)
        {
            BookViewModel bookViewModels = new BookViewModel();
            try
            {
                bookViewModels = (from book in dbContext.Book
                                     join cate in dbContext.Category
                                     on book.CategoryId equals cate.CategoryId
                                     join auth in dbContext.Author
                                    on book.AuthorId equals auth.AuthorId
                                     where book.ActiveStatus == true && book.BookId == Id
                                     select new BookViewModel()
                                     {
                                         BookId = book.BookId,
                                         BookTitle = book.BookTitle,
                                         BookPublication = book.BookPublication,
                                         Copies = book.Copies,
                                         PublisherName = book.PublisherName,
                                         ISBNNo = book.ISBNNo,
                                         CategoryName = cate.CategoryName,
                                         AuthorName = auth.AuthorName,
                                         CopyRightYear = book.CopyRightYear,
                                         ActiveStatus = book.ActiveStatus
                                     }
                                   ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(bookViewModels).ConfigureAwait(false);
        }

        public async Task<ResponseModel> UpdateBook(BookViewModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {

                int updaterow = 0;

                Book books = new Book()
                {
                    BookId = model.BookId,
                    BookTitle = model.BookTitle,
                    BookPublication = model.BookPublication,
                    Copies = model.Copies,
                    PublisherName = model.PublisherName,
                    ISBNNo = model.ISBNNo,
                    CategoryId = model.CategoryId,
                    AuthorId = model.AuthorId,
                    CopyRightYear = model.CopyRightYear,
                    ActiveStatus = true
                };
                dbContext.Book.Update(books);
                updaterow = dbContext.SaveChanges();
                if (updaterow > 0)
                {
                    responseModel.ResponseMessage = "Book Update Successfully";
                    responseModel.ResponseStatus = "Ok";
                }
            }
            catch (Exception ex)
            {
                responseModel.ResponseMessage = "Something went wrong";
            }
            return responseModel;
        }
       
    }
}
