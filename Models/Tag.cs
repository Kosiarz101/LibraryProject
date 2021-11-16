using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Tag
    {
        [Key]
        [Column(TypeName = "nvarchar"), MaxLength(200)]
        public string Name { get; set; }
    }
}