using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class Practices
    {
        public Guid ID { get; set; }
        [Required]
        [Display(Name = "Name", Description = "Name of practice")]
        public string Name { get; set; }

        [Display(Name = "Contact at Practice", Description = "Contact at Practice")]
        public string CAtPractice { get; set; }

        [Display(Name = "Contact Position", Description = "Contact Position")]
        public string CPosition { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Phone")]
        public string cphone { get; set; }

        [Required]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Please fill right Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Contact Email")]
        public string cEmail { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "Practice Website")]
        public string cWebsite { get; set; }

         
        [Display(Name = "Current Practice")]
        public bool isshowCurrentPractice { get; set; }

        #region Addresses


        [Display(Name = "Street Address")]
        public string streetAdd { get; set; }


      


        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "Post Code")]
        public string postCode { get; set; }


        [Display(Name = "Village/Area")]
        public string VArea { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Practice Phone")]
        public string Pphone { get; set; }
        #endregion


        public Guid PersonID { get; set; }
        #region other properties
        [ForeignKey("PersonID")]
        public virtual Person person { get; set; }
        public virtual ICollection<Sessions> sessions { get; set; }
        
        #endregion
    }
}