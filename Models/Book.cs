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
        public int Id { get; set; }
        [Required]       
        [Column(TypeName = "nvarchar")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar"), Required]
        [MaxLength(1100)]
        [DataType(DataType.MultilineText)]
        public string Describtion { get; set; }
        [Display(Name = "ISPN Number")]
        [Required, MaxLength(13)]
        [RegularExpression("^[0-9]{13}$", ErrorMessage = "ispn number must be a 13 digit number")]
        public string ISPNNumber { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(200)]
        public string Author { get; set; }
        [Display(Name = "Added To Library")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Publication Date")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers are allowed")]
        public int Quantity { get; set; }
        //Relations
        public virtual ICollection<AwaitedBook> AwaitedBooks { get; set; }
        public virtual ICollection<BookTag> BookTags { get; set; }
    }
}