using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class BookPageViewModel
    {
        public Book Book { get; set; }
        public List<Tag> Tags { get; set; }
    }
}