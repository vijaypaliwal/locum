using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AutoComplete.Models
{
    
    public class Appointment
    {
        [Key]
        public Guid APPID { get; set; }



       
        
        public Guid? PersonID { get; set; }

        [ForeignKey("PersonID")]
        public Person Person  { get; set; }





        [Required(ErrorMessage = "Please Select the Product from the List")]
        
        [Display(Name = "Parctice ", Description = "Parctice of the person")]
        public Guid PracID { get; set; }

        [ForeignKey("PracID")]
        public Practices Parctice { get; set; }

        [Required]
        [Display(Name = "Session Detail")]
        public Guid SessID { get; set; }
        [ForeignKey("SessID")]
        public Sessions sessionDetail { get; set; }

        
        [DataType(DataType.Currency)]
        [Display(Name = "Expences")]
        public double expences { get; set; }

        
        [DataType(DataType.Currency)]
        [Display(Name = "Visit Mileage")]
        public double VisitMileage { get; set; }

               
        [Display(Name = "Charge ?")]
        public bool IsCharged { get; set; }

        
        [DataType(DataType.Currency)]
        [Display(Name = "Visit Mileage")]
        public string Notes { get; set; }
      
        [Display(Name = "Invoice Notes")]
        public string InvoiceNotes { get; set; }

        [Display(Name = "Start Date")]
        public string startDate { get; set; }

        [Display(Name = "End Date")]
        public string endDate { get; set; }

        public bool Invoiced  { get; set; }
        public bool Paid  { get; set; }



        [NotMapped]
        public string Locumname { get; set; }
         [NotMapped]
        public string Sessionname { get; set; }
         [NotMapped]
        public string sstarttime { get; set; }
         [NotMapped]
        public string sendtime { get; set; } 
        [NotMapped]
         public int days { get; set; }
       
        

    }


}