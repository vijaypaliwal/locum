using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.Net.Mail;

using AutoComplete.Models;

namespace AutoComplete.Mailers
{
    public class UserMailer : MailerBase, IUserMailer
    {
        public UserMailer() :
            base()
        {
            MasterName = "_Layout";
        }


        public virtual MailMessage Welcome(Person Person)
        {
            var mailMessage = new MailMessage { Subject = "New Loccum Registrations" };

            mailMessage.To.Add(Person.Email);
            //Admin Email
            //  mailMessage.CC.Add("ceo@shivamitconsultancy.com");
            ViewBag.ID = Person.ID;

            ViewBag.FirstName = Person.FirstName;
            ViewBag.LastName = Person.LastName;
            ViewBag.UserName = Person.UserName;
            ViewBag.GMC = Person.GMC;
            ViewBag.Password = Person.Password;
            ViewBag.Email = Person.Email;

            PopulateBody(mailMessage, viewName: "Welcome");

            return mailMessage;
        }



        public virtual MailMessage Appointment(string UserEmail, string DoctorName, Appointment Appointment)
        {
            var AppointmentmailMessage = new MailMessage { Subject = "Remind for Appointment" };

            AppointmentmailMessage.To.Add(UserEmail);
            //Admin Email
            //   AppointmentmailMessage.CC.Add("ceo@shivamitconsultancy.com");
            ViewBag.ID = Appointment.APPID;

            ViewBag.StartDate = Appointment.startDate;
            ViewBag.EndDate = Appointment.endDate;
            ViewBag.VisitMileage = Appointment.VisitMileage;
            ViewBag.Expences = Appointment.expences;
            ViewBag.DoctorName = DoctorName;


            PopulateBody(AppointmentmailMessage, viewName: "Appointment");

            return AppointmentmailMessage;
        }




        public virtual MailMessage sendreport(string pathToCreate,string toaddress, InvoiceDetail invoicedetail)
        {
            var report = new MailMessage { Subject = "Invoice report" };
            report.To.Add(toaddress);
            report.Attachments.Add(new Attachment(pathToCreate));
            report.Sender = new MailAddress("dashrath.k@shivamitconsultancy.com");
            PopulateBody(report, viewName: "report");
            return report;
        }





        public virtual MailMessage ContactUs(ContactUs ContactUs, string useremail)
        {
            var mailMessage = new MailMessage { Subject = "Loccum Contact" };

            mailMessage.To.Add(ContactUs.Email);
            //Admin Email
            mailMessage.CC.Add(useremail);
            //  mailMessage.Bcc.Add("ceo@shivamitconsultancy.com");



            ViewBag.Name = ContactUs.Name;
            ViewBag.Email = ContactUs.Email;
            ViewBag.Subject = ContactUs.Subject;
            ViewBag.Message = ContactUs.Message;
            PopulateBody(mailMessage, viewName: "ContactUs");

            return mailMessage;
        }







        public virtual MailMessage ForgotPass(Person Person)
        {
            var mailMessage = new MailMessage { Subject = "ForgotPassword" };

            mailMessage.To.Add(Person.Email);
            mailMessage.Bcc.Add("ceo@shivamitconsultancy.com");

            ViewBag.fullname = Person.LastName;
            ViewBag.Data = Person.Password;
            ViewBag.Data1 = Person.UserName;

            PopulateBody(mailMessage, viewName: "ForgotPassword");

            return mailMessage;
        }

        //public virtual MailMessage ContactUs(ContactUs contactus)
        //{
        //    var mailMessage = new MailMessage { Subject = "Thanks For Contacting Us" };

        //    mailMessage.To.Add(contactus.Email);
        //    ViewBag.fullname = contactus.Name;
        //    ViewBag.Data = contactus.Email;
        //    ViewBag.Data1 = contactus.Message;



        //    PopulateBody(mailMessage, viewName: "ContactUS");

        //    return mailMessage;
        //}





        public virtual MailMessage GoodBye()
        {
            var mailMessage = new MailMessage { Subject = "GoodBye" };


            mailMessage.To.Add("some-email@example.com");
            ViewBag.Data = "";
            PopulateBody(mailMessage, viewName: "GoodBye");

            return mailMessage;
        }


    }
}