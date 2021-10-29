using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Information
    {
        //public int InformationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}