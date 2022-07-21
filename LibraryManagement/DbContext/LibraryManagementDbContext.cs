using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.ViewModel;

namespace LibraryManagement
{
    public class LibraryManagementDbContext : DbContext
    {
        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : base(options)
        {

        }
        public DbSet<RegisterUser> RegisterUser { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<LibraryManagement.ViewModel.BookViewModel> BookViewModel { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}
