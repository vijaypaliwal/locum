using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class Attachments
    {
        public Guid ID { get; set; }        
      
        public Guid AttachmentpersonID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required]        
        [Display(Name = "Doc Path")]
        public string path { get; set; }

        [ForeignKey("AttachmentpersonID")]
        public virtual Person person { get; set; }
       
    }
}