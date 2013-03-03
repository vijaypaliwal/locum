using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class ContactUs
    { 
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Please fill right Email Address")]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string website { get; set; }
        public string Company { get; set; }
     
    }
    public class colors 
    {
       [NotMapped]
        public string color { get; set; }
    
    }


}