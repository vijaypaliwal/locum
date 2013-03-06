using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class bookmarks
    {
        public int ID { get; set; }

        [Required]
        public string Btitle { get; set; }
        public string Btype { get; set; }

        [Required]
        public string url { get; set; }
        public string Description { get; set; }

        public Guid personID { get; set; }
        
    }
}