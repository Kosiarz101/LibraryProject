using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Search
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        public string Content { get; set; }
        [Display(Name = "Search Date")]
        public DateTime CreationDate { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}