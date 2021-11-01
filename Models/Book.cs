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
        [MaxLength(550)]
        [DataType(DataType.MultilineText)]
        public string Describtion { get; set; }
        [Display(Name = "ISPN Number")]
        public int ISPNNumber { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        [Display(Name = "Publication Date")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }
    }
}