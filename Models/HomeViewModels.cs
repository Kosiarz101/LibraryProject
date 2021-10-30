using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class HomeIndexViewModel
    {
        public List<Information> Information { get; set; }
        public List<Search> Searches { get; set; }
    }
}