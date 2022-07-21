using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage ="EmailId Is Required")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        public int  UserRole { get; set; }
    }
}
