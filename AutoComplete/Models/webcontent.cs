using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AutoComplete.Models
{
    public class webcontent
    {
        [Key]
        public int PageID { get; set; }

        public string title { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string content{ get; set; }
        
       
   
    }
}