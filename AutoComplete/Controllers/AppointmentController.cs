using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoComplete.Models;
using AutoComplete;
using Mvc.Mailer;
using AutoComplete.Mailers;
namespace AutoComplete.Controllers
{
    public class AppointmentController : Controller
    {
        private DataContext db = new DataContext();

        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }
        //
        // GET: /Appointment/

        public ViewResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Parctice).Include(a => a.sessionDetail);
            return View(appointments.ToList());
        }




        public ActionResult GetAllPractices()
        {
            Person per = Session["person"] as Person;
            var allpractices = db.Practices.Where(p => p.PersonID == per.ID);
            return Json(allpractices.Select(ap => new { ap.ID, ap.Name }), JsonRequestBehavior.AllowGet);
        }

        //////
        public JsonResult GetCascadeSession(Guid PracID)
        {
            
            var modelList = db.Sessions.ToList();
            if (PracID != null)
                modelList = modelList.Where(a => a.PID == PracID).ToList();

            var modelData = modelList.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.ID.ToString()

            });
                 return Json(modelData, JsonRequestBehavior.AllowGet);
         

        }
        //////
        //
        // GET: /Appointment/Details/5

        public ViewResult Details(Guid id)
        {
            Appointment appointment = db.Appointments.Find(id);
            return View(appointment);
        }

        //
        // GET: /Appointment/Create

        public ActionResult Create(DateTime startDate, DateTime endDate)
        {
            Person per = Session["person"] as Person;
            ViewBag.PracID = new SelectList(db.Practices.Where(a => a.PersonID == per.ID), "ID", "Name");
            ViewBag.SessID = new SelectList(db.Sessions, "ID", "Name");

            var User = Session["Person"] as Person;
            double value = 0.0;
            double asdouble = (double)(float)value;
            Appointment objNew = new Appointment();


            objNew.PersonID = User.ID;
            objNew.Notes = asdouble;
            objNew.expences = asdouble;
            objNew.VisitMileage = asdouble;
            objNew.IsCharged = true;
            objNew.startDate = startDate.ToString();
            objNew.endDate = endDate.ToString();
            return View(objNew);
        }

        //
        // POST: /Appointment/Create

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    appointment.APPID = Guid.NewGuid();
                    var User = Session["Person"] as Person;
                    appointment.PersonID = User.ID;
                    appointment.Invoiced = false;
                    if (appointment.expences == null)
                    {
                        appointment.expences = 0;
                    }
                    if (appointment.VisitMileage == null)
                    {
                        appointment.VisitMileage = 0;
                    }
                    appointment.Total = appointment.Notes + appointment.expences + appointment.VisitMileage;
                    db.Appointments.Add(appointment);
                    try
                    {
                        db.SaveChanges();
                    }

                    catch (Exception ex)
                    {
                        ModelState.AddModelError("_Form", ex.Message);
                    }


                    //_userMailer.Appointment(User.Email, User.UserName, appointment).Send();
                    ////////////////change
                   
                    var sessionDetail = db.Sessions.Where(ses => ses.ID == appointment.SessID).FirstOrDefault();
                    var practDetail = db.Practices.Where(p => p.ID == appointment.PracID).FirstOrDefault();
                    DateTime start = Convert.ToDateTime(appointment.startDate);
                    DateTime stTime = Convert.ToDateTime(sessionDetail.startTime);
                    DateTime end = Convert.ToDateTime(appointment.endDate);
                    DateTime enTime = Convert.ToDateTime(sessionDetail.EndTime);
                    var sesstionStartTime = Convert.ToDateTime(sessionDetail.startTime);
                    var sesstionEndTime = Convert.ToDateTime(sessionDetail.EndTime);
                    var Cal = new FullCalendarEvent();
                    Cal.id = appointment.APPID;
                    Cal.title = practDetail.Name;
                    Cal.Sessiontitle = sessionDetail.Name;
                    Cal.SessionStart = (sesstionStartTime).ToShortTimeString();
                    Cal.Sessionend = (sesstionEndTime).ToShortTimeString();
                    Cal.start = new DateTime(start.Year, start.Month, start.Day, stTime.Hour,stTime.Minute,stTime.Second).ToString("s");
                    Cal.end = new DateTime(end.Year, end.Month, end.Day, enTime.Hour, enTime.Minute, enTime.Second).ToString("s");
                    Cal.color = "#2d89ef";
                    return Json(Cal, JsonRequestBehavior.AllowGet);
                    ////////////////////////////////////////
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_Form", ex.Message);
            }
                ViewBag.PracID = new SelectList(db.Practices, "ID", "Name", appointment.PracID);
                ViewBag.SessID = new SelectList(db.Sessions, "ID", "Name", appointment.SessID);
                return View(appointment);
            

        }

        //
        // GET: /Appointment/Edit/5

        public ActionResult Edit(Guid id)
        {
            try
            {
                Person per = Session["person"] as Person;
                Appointment appointment = db.Appointments.Find(id);
                ViewBag.PracID = new SelectList(db.Practices.Where(a => a.PersonID == per.ID), "ID", "Name", appointment.PracID);
                ViewBag.SessID = new SelectList(db.Sessions, "ID", "Name", appointment.SessID);
                return View(appointment);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_Form", ex.Message);
            }
            return View();
        }

        //
        // POST: /Appointment/Edit/5

        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {


                // appointment.Locumname = db.Practices.Where(a => a.ID == appointment.PracID).FirstOrDefault() != null ? db.Practices.Where(a => a.ID == appointment.PracID).FirstOrDefault().Name : "";




                // appointment.Sessionname = db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault() != null ? db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault().Name : "";




                // appointment.sstarttime = db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault() != null ? db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault().startTime : "";


                // appointment.sendtime = db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault() != null ? db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault().EndTime : "";
                var User = Session["Person"] as Person;
                appointment.PersonID = User.ID;
                appointment.Total = appointment.Notes + appointment.expences + appointment.VisitMileage;
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                // return Json(appointment, JsonRequestBehavior.AllowGet);
                //Update Scheduler
                var sessionDetail = db.Sessions.Where(ses => ses.ID == appointment.SessID).FirstOrDefault();
                var practDetail = db.Practices.Where(p => p.ID == appointment.PracID).FirstOrDefault();
                DateTime start = Convert.ToDateTime(appointment.startDate);
                DateTime stTime = Convert.ToDateTime(sessionDetail.startTime);
                DateTime end = Convert.ToDateTime(appointment.endDate);
                DateTime enTime = Convert.ToDateTime(sessionDetail.EndTime);
                var sesstionStartTime = Convert.ToDateTime(sessionDetail.startTime);
                var sesstionEndTime = Convert.ToDateTime(sessionDetail.EndTime);
                var Cal = new FullCalendarEvent();
                Cal.id = appointment.APPID;
                Cal.title = practDetail.Name;
                Cal.Sessiontitle = sessionDetail.Name;
                Cal.SessionStart = (sesstionStartTime).ToShortTimeString();
                Cal.Sessionend = (sesstionEndTime).ToShortTimeString();
                Cal.start = new DateTime(start.Year, start.Month, start.Day, stTime.Hour, stTime.Minute, stTime.Second).ToString("s");
                Cal.end = new DateTime(end.Year, end.Month, end.Day, enTime.Hour, enTime.Minute, enTime.Second).ToString("s");

                return Json(Cal, JsonRequestBehavior.AllowGet);
                ///////////////
            }
            ViewBag.PracID = new SelectList(db.Practices, "ID", "Name", appointment.PracID);
            ViewBag.SessID = new SelectList(db.Sessions, "ID", "Name", appointment.SessID);
            return View(appointment);
        }

      
        // POST: /Appointment/Delete/5

        //[HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                Appointment appointment = db.Appointments.Find(id);
                db.Appointments.Remove(appointment);
                db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_Form", ex.Message);
            }
            return RedirectToAction("IndexLocum");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}