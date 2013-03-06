using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class AdditionalSurgeries
    {
        public Guid ID { get; set; }
        [Required]
        [Display(Name = "Surgery Name", Description = "Surgery Name")]
        public string Name { get; set; }

        [Display(Name = "Street Address")]
        public string streetAdd { get; set; }


       


        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "Post Code")]
        public string postCode { get; set; }

        [Required]
        [Display(Name = "Village/Area")]
        public string VArea { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Surgery Phone")]
        public string SurgeryPhone { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Return commuting distance in miles")]
        public string Rcdm { get; set; }

        public Guid PracticesID { get; set; }
        #region other properties
        [ForeignKey("PracticesID")]
        public virtual Practices Practices { get; set; }
        #endregion
     
    }
}