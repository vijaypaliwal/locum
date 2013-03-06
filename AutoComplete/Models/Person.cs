using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AutoComplete.Models
{
    public enum PersonGender : byte
    {
        Male,
        Female
    }

    public class Person
    {
        public Person()
        {
            ID = Guid.NewGuid();
        }


        public Guid ID { get; set; }

        [Display(Name = "Title", Description = "Title of the person (Mr,Dr,Mrs,Ms etc")]
        [StringLength(15)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name", Description = "First Name of the person")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Gender", Description = "Gender of the person")]
        public PersonGender personGender { get; set; }

        [Required]
        [Display(Name = "Last Name", Description = "Last Name of the person")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "GMC # ", Description = "GMC of the person")]
        public string GMC { get; set; }

        [Required]
        [Display(Name = "Qualification ", Description = "Qualification of the person")]
        public string qualification { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string streetAdd { get; set; }

        //removed town from here

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        public string postCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Required]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Please fill right Email Address")]
        [Display(Name = "Email address")]
        public string Email { get; set; }



        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        #region other properties
        public virtual ICollection<Practices> practices { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        #endregion

    }

    public class AboutPerson
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Html)]
        [AllowHtml]      
        public string AboutDetail { get; set; }
        public Guid PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person person { get; set; }
    }
}