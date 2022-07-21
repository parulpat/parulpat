using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModel
{
    public class StudentViewModel
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string RollNo { get; set; }
        public string FatherName { get; set; }
        public string Class { get; set; }
        public string Address { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ProfileImagePath { get; set; }
        public bool Active { get; set; }
    }
}
