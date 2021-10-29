using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(500)]
        public string Describtion { get; set; }
        public int ISPNNumber { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
    }
}