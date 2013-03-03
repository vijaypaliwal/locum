using AutoComplete.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoComplete.Controllers
{
    public class InvoicingController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult SaveFile(InvoiceDetail vm)
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
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Invoice.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource("Invoice", abresults);
            localReport.DataSources.Add(reportDataSource);

            string reportType = "pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
                "" +
                "  PDF" +
                "  8.5in" +
                "  11in" +
                "  0.5in" +
                "  1in" +
                "  1in" +
                "  0.5in" +
                "";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            // Save to Database
            InvoiceFileGenerator dataFile = new InvoiceFileGenerator
            {
                FileName = "Customer",
                FileData = renderedBytes,
                FileExtension = ".pdf",
                isInvoicePaid=false,
                GeneratedDate=DateTime.Now
                
            };

            bool Status;
            try
            {
                db.Invoices.Add(dataFile);
                db.SaveChanges();
                ViewBag.Status = true;
                return View();
            }
            catch
            {
                ViewBag.Status = false;
                return View();
            }
        }
        public ActionResult Download()
        {
            InvoiceFileGenerator dataFile = db.Invoices.SingleOrDefault(p => p.Id == 1);
            byte[] fileData = dataFile.FileData;

            // Generate PDF file
            Warning[] warnings = null;
            String[] streamids = null;
            String mimeType = null;
            String encoding = null;
            String extension = null;

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.ContentType = mimeType;
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=Customer.pdf"));
            Response.BinaryWrite(fileData);
            Response.End();

            return RedirectToAction("Index");
        }

    }
}
