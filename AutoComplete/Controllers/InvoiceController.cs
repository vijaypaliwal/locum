using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoComplete.Models;
using AutoComplete;
using DoddleReport.Web;
using DoddleReport.AbcPdf;
using DoddleReport;
using System.Text;
using System.IO;
using ReportManagement;
using AutoComplete.Reports;
using System.Web.Mail;
using Mvc.Mailer;
using System.Net.Mail;
using AutoComplete.Mailers;
using System.Data.Entity.Validation;

namespace AutoComplete.Controllers
{
    public class InvoiceController : PdfViewController
    {
        private DataContext db = new DataContext();


        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }

        //
        // GET: /Invoice/

        public ViewResult Index()
        {
            var invoicedetails = db.InvoiceDetails.Include(i => i.Appointment);
            return View(invoicedetails.ToList());
        }
        public ActionResult PensionB()
        {
            var PensionB = db.Invoices;
            return View(PensionB.ToList().Where(a => a.isInvoicePaid == true));
        }


        public JsonResult GetAllunpaidInvoice()
        {

            return Json(db.Invoices.Select(d => new { d.Id, d.FileData, d.FileExtension, d.FileName, d.GeneratedDate, d.isInvoicePaid, d.InvoicePaidDate }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult unpaidinvoice()
        {
            var unpaid = db.Invoices;
            return View(unpaid.ToList().Where(a => a.isInvoicePaid == false));
        }

        public ActionResult unpaidinvoice1()
        {
            Person sperson = Session["person"] as Person;
            var invoices = db.InvoiceDetails.Where(a => a.PersonID == sperson.ID);
            return View(invoices.ToList());
        }
        public ActionResult GetAllInvoice()
        {
            Person per = Session["person"] as Person;
            var inv = db.InvoiceDetails.Where(i => i.PersonID == per.ID).ToList();
            foreach (var itemm in inv)
            {
                var pracid = db.Practices.Where(pd => pd.ID == itemm.PracticeID);
                itemm.invoice_date = itemm.InvoicedDate.ToString("M/d/yyyy");
                itemm.paid_date = itemm.PaidDate.ToString("M/d/yyyy");
                foreach (var q in pracid)
                {
                    itemm.practice_name = q.Name;
                }
                if (itemm.IsPaid == true)
                {
                    itemm.Paid_Status = "Paid";
                    itemm.enable = true;
                }
                else
                {
                    itemm.Paid_Status = "Unpaid";
                    itemm.enable = false;

                }

            }

            return Json(inv.Select(s => new { s.ID, s.practice_name, s.InvoicedDate, s.invoice_date, s.paid_date, s.Total, s.PaidDate, s.Paid_Status,s.enable }), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public ActionResult Paid(Guid id)
        {
            var allinvoice = db.InvoiceDetails.Where(invo => invo.ID == id);
            foreach (var itm in allinvoice)
            {
                itm.IsPaid = true;
                var stdate = Convert.ToDateTime(itm.startDate);
                var eddate = Convert.ToDateTime(itm.endDate);
                var allapt = db.Appointments.Where(ap => ap.PracID == itm.PracticeID);
                if (allapt != null)
                {
                    try
                    {
                        foreach (var appt in allapt)
                        {
                            var origStartDate = Convert.ToDateTime(appt.startDate);
                            var origEndDate = Convert.ToDateTime(appt.endDate);
                            if (origStartDate >= stdate && origEndDate <= eddate)
                            {
                                appt.Paid = true;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        ModelState.AddModelError("_Form", ex.Message);
                    }
                }

            }
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult makepaid(Generateinvoice gi)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        db.SaveChanges();
        //        return Content(Boolean.TrueString);
        //    }

        //    return View(gi);
        //}




        public ActionResult DetailPensionB(int id, InvoiceDetail vm)
        {
            var abresults = new List<InvoiceDetail>();

            var results = new List<Appointment>();
            var allAppt = db.Appointments.ToList();
            vm.session = new Sessions();
            vm.session = vm.session;

            Session["Practice"] = db.Practices.Where(p => p.ID == vm.PracticeID).FirstOrDefault();
            var practice = Session["Practice"] as AutoComplete.Models.Practices;
            var name = Session["Person"] as AutoComplete.Models.Person;

            return View();
        }


        //
        // GET: /Invoice/Details/5

        public ViewResult Details(Guid id)
        {
            InvoiceDetail invoicedetail = db.InvoiceDetails.Find(id);
            return View(invoicedetail);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create()
        {
            //ViewBag.InAppointmentID = new SelectList(db.Appointments, "APPID", "Notes");
            //ViewBag.PracticeID = new SelectList(db.Practices, "ID", "Name");
            return View();
        }


        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(InvoiceDetail InvoiceDetails)
        {
            if (ModelState.IsValid)
            {
                InvoiceDetails.ID = Guid.NewGuid();
                InvoiceDetails.PaidDate = DateTime.Now;
                db.InvoiceDetails.Add(InvoiceDetails);
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }

            return View();
        }









        [HttpPost]
        public ActionResult Saveinvoice(InvoiceDetail InvoiceDetails)
        {
            if (ModelState.IsValid)
            {
                var AppID = Request["Appointment.APPID"];
                InvoiceDetails.ID = Guid.NewGuid();
                InvoiceDetails.PaidDate = DateTime.Now;

                InvoiceDetails.startDate = DateTime.Now;
                InvoiceDetails.endDate = DateTime.Now;
                InvoiceDetails.InvoicedDate = DateTime.Now;
                db.InvoiceDetails.Add(InvoiceDetails);
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }

            return View();
        }






        //
        // GET: /Invoice/Edit/5

        public ActionResult Edit(Guid id)
        {
            InvoiceDetail invoicedetail = db.InvoiceDetails.Find(id);
            ViewBag.InAppointmentID = new SelectList(db.Appointments, "APPID", "Notes", invoicedetail.InAppointmentID);
            return View(invoicedetail);
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        public ActionResult Edit(InvoiceDetail invoicedetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoicedetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InAppointmentID = new SelectList(db.Appointments, "APPID", "Notes", invoicedetail.InAppointmentID);
            return View(invoicedetail);
        }



        public ActionResult InvoiceProfile(InvoiceDetail vm)
        {
            var abresults = new List<InvoiceDetail>();

            var results = new List<Appointment>();
            var allAppt = db.Appointments.ToList();
            Session["Practice"] = db.Practices.Where(p => p.ID == vm.PracticeID).FirstOrDefault();
            foreach (var appt in allAppt)
            {
                var origStartDate = Convert.ToDateTime(appt.startDate);
                var origEndDate = Convert.ToDateTime(appt.endDate);
                if (DateTime.Compare(vm.startDate, origStartDate) <= 0 || DateTime.Compare(vm.endDate, origEndDate) >= 0)
                {
                    if (vm.PracticeID == appt.PracID)
                    {
                        appt.sessionDetail = new Sessions();
                        appt.sessionDetail = db.Sessions.Where(a => a.PID == appt.PracID).FirstOrDefault() != null ? db.Sessions.Where(a => a.PID == appt.PracID).FirstOrDefault() : null;
                        results.Add(appt);
                    }
                }
            }
            if (results != null)
            {
                foreach (var r in results)
                    abresults.Add(new InvoiceDetail()
                    {

                        InvoicedDate = DateTime.Now,
                        ID = Guid.NewGuid(),
                        Appointment = r

                    });

            }
            //   invoicedetail = A
            return this.ViewPdf("Order Details", "InvoicingreportTable", abresults);
        }

        public ActionResult CreateReport(string writer)
        {
            if (Session["Report"] == null) return View();
            var report = Session["Report"] as Report;
            // Return the ReportResult
            // the type of report that is rendered will be determined by the extension in the URL (.pdf, .xls, .html, etc)
            if (writer == "Excel") return new ReportResult(report, new DoddleReport.OpenXml.ExcelReportWriter());
            else if (writer == "html") return new ReportResult(report, new DoddleReport.Writers.HtmlReportWriter());
            //create new MemoryStream object and add PDF file’s content to outStream.
            return new ReportResult(report, new PdfReportWriter());
        }

        //
        // GET: /Invoice/Delete/5

        public ActionResult Delete(Guid id)
        {
            InvoiceDetail invoicedetail = db.InvoiceDetails.Find(id);
            return View(invoicedetail);
        }

        public ActionResult detailpage()
        {
            return View();
        }


        public ActionResult GetAllPractices()
        {
            Person per = Session["person"] as Person;
            var allpractices = db.Practices.Where(p => p.PersonID == per.ID);
            return Json(allpractices.Select(ap => new { ap.ID, ap.Name }), JsonRequestBehavior.AllowGet);
        }



        //
        // POST: /Invoice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            InvoiceDetail invoicedetail = db.InvoiceDetails.Find(id);
            db.InvoiceDetails.Remove(invoicedetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        #region InvoicingreportRegion
        public ActionResult Invoicingreport()
        {
            var vm = new InvoiceDetail() { startDate = DateTime.Today.Date, endDate = DateTime.Today.Date, IsPaid = false, InvoicedDate = DateTime.Today.Date, PracticeID = Guid.NewGuid() };
            var name = Session["Person"] as AutoComplete.Models.Person;
            ViewBag.PracticeID = new SelectList(db.Practices.Where(a => a.PersonID == name.ID), "ID", "Name");
            return View(vm);
        }

        [HttpPost]
        public ActionResult InvoicingreportTable(InvoiceDetail vm, string writer)
        {
            var abresults = new List<InvoiceDetail>();

            var results = new List<Appointment>();
            var allAppt = db.Appointments.Where(a => a.Invoiced == false).ToList();
            vm.session = new Sessions();
            vm.session = vm.session;
            decimal total = 0;
            Session["Practice"] = db.Practices.Where(p => p.ID == vm.PracticeID).FirstOrDefault();
            var practice = Session["Practice"] as AutoComplete.Models.Practices;
            var name = Session["Person"] as AutoComplete.Models.Person;

            foreach (var appt in allAppt)
            {
                var origStartDate = Convert.ToDateTime(appt.startDate);
                var origEndDate = Convert.ToDateTime(appt.endDate);
                var day = (origEndDate - origStartDate).TotalDays;

                int dy = Convert.ToInt32(day) + 1;
                if (vm.startDate <= origStartDate && vm.endDate >= origEndDate)
                {
                    if (vm.PracticeID == appt.PracID)
                    {
                        appt.sessionDetail = new Sessions();
                        appt.days = dy;
                        appt.sessionDetail = db.Sessions.Where(a => a.ID == appt.SessID).FirstOrDefault() != null ? db.Sessions.Where(a => a.ID == appt.SessID).FirstOrDefault() : null;
                        results.Add(appt);


                        /*  var query =
                              from c in db.Appointments
                              where c.APPID == appt.APPID
                              select c;
                          foreach (var query1 in query)
                          {
                              query1.Invoiced = true;
                          }
                          db.SaveChanges();
                          */
                    }
                }
            }


            if (results != null)
            {
                foreach (var r in results)
                    abresults.Add(new InvoiceDetail()
                    {

                        InvoicedDate = DateTime.Now,
                        ID = Guid.NewGuid(),
                        Appointment = r,
                        SessionFees = Convert.ToDecimal(r.sessionDetail.fees) * r.days,
                        SessionName = r.sessionDetail.Name,
                        totalFees = total += Convert.ToDecimal(r.sessionDetail.fees)


                    });

                //  Generateinvoice docreport = new Generateinvoice();
                //  docreport.CreatePackage(Server.MapPath("\\Reports\\abc.doc"), abresults, practice);
                Session["Allinvoice"] = abresults;
                var report = new Report(abresults.Select(ap => new { ap.InvoicedDate, ap.SessionName, ap.SessionFees }).ToList().ToReportSource());
                // Customize the Text Fields
                report.TextFields.Header = string.Format(@"<table  width='100%'><tr><td align='left'>
                &nbsp To : {0}
                 Website :  {1}
                 City :  {2}
                 Street Address :  {3}
                 Contact no. :  {4}
                <span style='color:red'>Email</span> :  {5}</td>

<td align=''>
 From Dr. : {6}
 City :    {7}
  Country :    {8}
   Contact no.:  {9}
 Email : {10} </td></tr></table>", practice.Name, practice.cWebsite, practice.city, practice.streetAdd, practice.cphone, practice.cEmail, name.FirstName, name.city, name.country, name.phone, name.Email);
                report.TextFields.Title = "Invoicing For " + db.Practices.Where(p => p.ID == vm.PracticeID).FirstOrDefault() == null ? "Not Found" : db.Practices.Where(p => p.ID == vm.PracticeID).FirstOrDefault().Name;
                report.TextFields.SubTitle = " From : " + vm.startDate + " End : " + vm.endDate;
                report.TextFields.Footer = string.Format(@"<table width='80%'><tr><td align='right'> Total Fees {0}</td></tr></table>", Convert.ToString(total));
                // Render hints allow you to pass additional hints to the reports as they are being rendered
                report.RenderHints.BooleanCheckboxes = true;
                report.RenderHints.BooleansAsYesNo = true;
                report.RenderHints.FreezeRows = 20;
                report.RenderHints.FreezeColumns = 10;
                Session["Report"] = report;

                if (writer == "excel") return new ReportResult(report, new DoddleReport.Writers.ExcelReportWriter());

            }
            return View(abresults);
            //return Json(abresults.Select(ap => new { ap.PatientId, ap.TestDateTime,ap.Age, ap.Gender,  ap.TestName, ap.Technician, ap.Result, ap.NormalRange, ap.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }




        public ActionResult mailInvoice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult mailInvoice(InvoiceDetail invoice)
        {
            var mailmessage = new System.Net.Mail.MailMessage { Subject = "Reports" };
            var person = (Session["Person"] as Person);
            var Prac = Session["Practice"] as AutoComplete.Models.Practices;
            string pathToCreate = "~/Reports/" + person.FirstName + "/Invoice" + "_" + person.FirstName + "_" + person.ID + ".doc";
            string toaddress = Prac.cEmail;
            var to = toaddress;
            mailmessage.Attachments.Add(new Attachment(Server.MapPath(pathToCreate)));
            var path = Server.MapPath(pathToCreate);

            _userMailer.sendreport(path, to, invoice).Send();

            return Content(Boolean.TrueString);
        }


        public ActionResult SaveGeneratedInvoice()
        {
            var vm = new InvoiceDetail() { startDate = DateTime.Today.Date, endDate = DateTime.Today.Date, IsPaid = false, InvoicedDate = DateTime.Today.Date, PracticeID = Guid.NewGuid() };
            return View();
        }



        [HttpPost]
        public ContentResult SaveGeneratedInvoice(InvoiceDetail vm, decimal total)
        {

           
            var startdate = Request.Form["startDate"];
            vm.Total = total;
            if (ModelState.IsValid)
            {
                vm.FileExtension = ".doc";
                vm.IsPaid = false;
                vm.PaidDate = DateTime.Now;
                vm.ID = Guid.NewGuid();
                var person = (Session["Person"] as Person);
                vm.PersonID = person.ID;
                string pathToCreate = "~/Reports/" + person.FirstName;
                if (!Directory.Exists(Server.MapPath(pathToCreate)))
                {
                    Directory.CreateDirectory(Server.MapPath(pathToCreate));
                }

                vm.Filename = "Invoice" + "_" + person.FirstName + "_" + person.ID + ".doc";
                Generateinvoice docreport = new Generateinvoice();
                docreport.CreatePackage(Server.MapPath(pathToCreate + "\\" + vm.Filename), Session["Allinvoice"] as List<InvoiceDetail>, Session["Practice"] as Practices);
                var allappointment = db.Appointments.Where(app => app.PracID == vm.PracticeID);
                foreach (var iteam in allappointment)
                {
                    iteam.Invoiced = true;
                }

                db.InvoiceDetails.Add(vm);
                db.SaveChanges();



            }
            return Content("True");
            //return Json(abresults.Select(ap => new { ap.PatientId, ap.TestDateTime,ap.Age, ap.Gender,  ap.TestName, ap.Technician, ap.Result, ap.NormalRange, ap.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }

        private DateTime ToDateTime(string p)
        {
            return Convert.ToDateTime(p);
        }

        //public ActionResult AbnormalLabResultRepo(InvoiceDetail vm, string writer)
        //{
        //    var abresults = new List<InvoiceDetail>();
        //    var results = db.Appointments.Include(p => p.Parctice).Select(p => p);
        //    foreach (var r in results)
        //        abresults.Add(new InvoiceDetail()
        //        {

        //            InvoicedDate = DateTime.Now,
        //            ID = Guid.NewGuid(),
        //            Appointment = r
        //        });
        //    // Create the report and turn our query into a ReportSource
        //    var report = new Report(abresults.ToReportSource());


        //    // Customize the Text Fields
        //    report.TextFields.Header = "INVOICE";
        //    report.TextFields.Title = "Invoicing For " + vm.Appointment.Parctice.Name;
        //    report.TextFields.SubTitle = " From : " + vm.startDate + " End : " + vm.endDate;
        //    report.TextFields.Footer = "Copyright 2012 (c) Locum Assistant";



        //    // Render hints allow you to pass additional hints to the reports as they are being rendered
        //    report.RenderHints.BooleanCheckboxes = true;
        //    report.RenderHints.BooleansAsYesNo = true;
        //    report.RenderHints.FreezeRows = 9;
        //    report.RenderHints.FreezeColumns = 2;

        //    // Return the ReportResult
        //    // the type of report that is rendered will be determined by the extension in the URL (.pdf, .xls, .html, etc)
        //    if (writer == "excel") return new ReportResult(report, new DoddleReport.Writers.ExcelReportWriter());
        //    else if (writer == "html") return new ReportResult(report, new DoddleReport.Writers.HtmlReportWriter());
        //    return new ReportResult(report, new PdfReportWriter());
        //}
        #endregion
    }
}