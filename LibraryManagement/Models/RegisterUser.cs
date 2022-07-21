using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class RegisterUser
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "UserRole is Required")]
        public int UserRole { get; set; }
        [Required(ErrorMessage = "EmailId is Required")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        public string ContactNo { get; set; }
        
        public string ProfileImagePath { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
