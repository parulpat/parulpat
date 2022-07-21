using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        [Required(ErrorMessage = "BookId Is Required")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "BookTitle Is Required")]
        public string BookTitle { get; set; }
        [Required(ErrorMessage = "Category Is Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Author Is Required")]
        public int AuthorId { get; set; }
        public string Copies { get; set; }
        public string BookPublication { get; set; }
        public string PublisherName { get; set; }
        public string ISBNNo { get; set; }
        public string CopyRightYear { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
