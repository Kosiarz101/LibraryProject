using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BorrowedBook
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public DateTime CreationDate { get; set; }
    }
}