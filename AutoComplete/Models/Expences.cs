using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoComplete.Models
{
    public class Expences
    {
        public Guid ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expences Date")]
        public DateTime EDate { get; set; }
        public Categrory Category { get; set; }
        public Guid personID { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]        
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public double Amount { get; set; }


        public virtual ICollection<Categrory> Categories { get; set; }

        [ForeignKey("personID")]
        public virtual Person person { get; set; }
    }

    public class Categrory
    {

        public Guid ID { get; set; }

        public String Name { get; set; }

        public Guid ExpencesID { get; set; }

        [ForeignKey("ExpencesID")]
        public virtual Expences Expences { get; set; }
    }
}