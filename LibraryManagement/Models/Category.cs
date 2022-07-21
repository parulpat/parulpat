using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Category Is Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name Is Required")]
        public string CategoryName { get; set; }
        public bool ActiveStatus { get; set; }
    }

    
}
