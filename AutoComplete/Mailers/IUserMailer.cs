using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.Net.Mail;

using AutoComplete.Models;

namespace AutoComplete.Mailers
{
    public interface IUserMailer
    {

        MailMessage Welcome(Person Person);
        MailMessage ForgotPass(Person person);
        MailMessage Appointment(string UserName, string DoctorName, Appointment Appointment);
        MailMessage ContactUs(ContactUs contactus, string useremail);
        MailMessage sendreport(string pathToCreate,string toaddress,InvoiceDetail invoicedetail);






    }
}