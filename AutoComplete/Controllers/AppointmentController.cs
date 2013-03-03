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



            Appointment objNew = new Appointment();


            objNew.PersonID = User.ID;
            objNew.startDate = startDate.ToString();
            objNew.endDate = endDate.ToString();
            return View(objNew);
        }

        //
        // POST: /Appointment/Create

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.APPID = Guid.NewGuid();
                var User = Session["Person"] as Person;
                appointment.PersonID = User.ID;
                db.Appointments.Add(appointment);
                db.SaveChanges();

                _userMailer.Appointment(User.Email, User.UserName, appointment).Send();
                return Content(Boolean.TrueString);
            }

            ViewBag.PracID = new SelectList(db.Practices, "ID", "Name", appointment.PracID);
            ViewBag.SessID = new SelectList(db.Sessions, "ID", "Name", appointment.SessID);
            return View(appointment);
        }

        //
        // GET: /Appointment/Edit/5

        public ActionResult Edit(Guid id)
        {

            Person per = Session["person"] as Person;
            Appointment appointment = db.Appointments.Find(id);
            ViewBag.PracID = new SelectList(db.Practices.Where(a=>a.PersonID==per.ID), "ID", "Name", appointment.PracID);
            ViewBag.SessID = new SelectList(db.Sessions, "ID", "Name", appointment.SessID);
            return View(appointment);
        }

        //
        // POST: /Appointment/Edit/5

        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {


                appointment.Locumname = db.Practices.Where(a => a.ID == appointment.PracID).FirstOrDefault() != null ? db.Practices.Where(a => a.ID == appointment.PracID).FirstOrDefault().Name : "";




                appointment.Sessionname = db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault() != null ? db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault().Name : "";




                appointment.sstarttime = db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault() != null ? db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault().startTime : "";


                appointment.sendtime = db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault() != null ? db.Sessions.Where(a => a.ID == appointment.SessID).FirstOrDefault().EndTime : "";

                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return Json(appointment, JsonRequestBehavior.AllowGet);
            }
            ViewBag.PracID = new SelectList(db.Practices, "ID", "Name", appointment.PracID);
            ViewBag.SessID = new SelectList(db.Sessions, "ID", "Name", appointment.SessID);
            return View(appointment);
        }

        //
        // GET: /Appointment/Delete/5

        public ActionResult Delete(Guid id)
        {
            Appointment appointment = db.Appointments.Find(id);
            return View(appointment);
        }

        //
        // POST: /Appointment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}