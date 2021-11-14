using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BookTag
    {
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}