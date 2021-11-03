using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Information
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(3000)]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public bool WasEdited { get; set; }
        public DateTime? LastEditionDate { get; set; }
    }
}