using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Author
    {
        [Key]
        [Required(ErrorMessage = "Author Is Required")]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Author Name Is Required")]
        public string AuthorName { get; set; }
        public bool ActiveStatus { get; set; }
    }
    
}
