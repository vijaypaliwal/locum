using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class Sessions : colors
    {
        public Guid ID { get; set; }


        [Required]
        [Display(Name = "Session Name", Description = "Session Name")]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:hh\:mm}")]
        [DataType(DataType.Time)]
        public string startTime { get; set; }

        [Required]
        [Display(Name = "End Time", Description = "End Time")]
        [DataType(DataType.Time)]
        public string EndTime { get; set; }

     
        [Display(Name = "Fee/Rate", Description = "Fee/Rate")]
        [DataType(DataType.Currency)]
        public string fees { get; set; }

        [Required]
        public Guid PID { get; set; }
        #region other properties
        [ForeignKey("PID")]
        public virtual Practices Practices { get; set; }

    
        #endregion
        public virtual ICollection<SessionColor> sessionColor { get; set; }
     
    }
}