using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoComplete.Models
{
    public class InvoiceDetail
    {

        public Guid ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
        public bool IsPensionFormAinclude { get; set; }       
        public DateTime InvoicedDate { get; set; }
        public DateTime PaidDate { get; set; }
        public bool IsPaid { get; set; }
        public Guid InAppointmentID { get; set; }
        public Guid PracticeID { get; set; }
        public Guid? sessionID { get; set; }
        public string Filename { get; set;}
        public string FileExtension { get; set; }
        #region other properties
       
        [ForeignKey("InAppointmentID")]
        public virtual Appointment Appointment { get; set; }

        [ForeignKey("PracticeID")]
        public virtual Practices Practice { get; set; }


        [ForeignKey("sessionID")]
        public virtual Sessions session { get; set; }

        public virtual ICollection<InvoiceFileGenerator> Invoices { get; set;}
        #endregion
        #region display Property
        [NotMapped]
        public string date { get; set; }
        [NotMapped]
        public string SessionName { get; set; }

        [NotMapped]

        public string SessionFees { get; set;  }


        [NotMapped]

        public decimal totalFees { get; set;}


#endregion
    }
    public class InvoiceFileGenerator
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileData { get; set; }
        public DateTime GeneratedDate { get; set; }
        public bool isInvoicePaid { get; set; }
        public string startdate { get; set; }
        public string Enddate { get; set; }
        public bool Ispensionform { get; set; }

        public DateTime? InvoicePaidDate { get; set; }
        public Guid? InvoiceID { get; set; }
        [ForeignKey("InvoiceID")]
        public virtual InvoiceDetail invoice { get; set; }


    }

}