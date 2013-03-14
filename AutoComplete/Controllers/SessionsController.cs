using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoComplete.Models;
using AutoComplete;

namespace AutoComplete.Controllers
{ 
    public class SessionsController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Sessions/
        public ViewResult Index(Guid? LocumID)
        {

            //var additionalsurgeries = db.AdditionalSurgeries.Include(a => a.Practices);
            ViewBag.locumID = LocumID;
            List<Sessions> obj = new List<Sessions>();
            return View(obj);
        }

        public ActionResult GetAllPractices()
        {
            Person per = Session["person"] as Person;
            var allpractices = db.Practices.Where(p => p.PersonID == per.ID);
            return Json(allpractices.Select(ap => new { ap.ID, ap.Name }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllSessions(Guid locumID)
        {
            var sessions = db.Sessions.Include(s => s.Practices).Where(p=>p.Practices.PersonID==locumID);
            var allsessions = new List<Sessions>();
            if (sessions != null)
                sessions.ToList().ForEach(p =>
                {
                    if (p.sessionColor.Count() > 0 && p.sessionColor != null)
                    {
                        p.color = p.sessionColor.FirstOrDefault().Color;
                    }
                    allsessions.Add(p);
                });

         
            return Json(allsessions.Select(s=>new {s.Name,s.fees,s.ID,s.color,s.PID,s.startTime,s.EndTime}),JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Sessions/Details/5

        public ViewResult Details(Guid id)
        {
            Sessions sessions = db.Sessions.Find(id);
            return View(sessions);
        }
        public ViewResult DetailsForAppointment(Guid id)
        {
            Sessions sessions = db.Sessions.Find(id);
            return View(sessions);
        }
        //
        // GET: /Sessions/Create

        public ActionResult Create()
        {
            Person per = Session["person"] as Person;
            ViewBag.PracticesID = new SelectList(db.Practices.Where(a=>a.PersonID==per.ID), "ID", "Name");
            return View();
        } 

        //
        // POST: /Sessions/Create

        [HttpPost]
        public ActionResult Create(Sessions sessions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    sessions.ID = Guid.NewGuid();
                    sessions.PID = Guid.Parse(Request["PracticesID"]);
                  
                //    sessions.sessionColor.FirstOrDefault().Color = "#fff";
                 
                    db.Sessions.Add(sessions);
                    db.SaveChanges();
                    return Content(Boolean.TrueString);  
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("_Form", ex.Message);
                    ViewBag.PracticesID = new SelectList(db.Practices, "ID", "Name", sessions.PID);
                    return View(sessions);
                }
                
            }

            ViewBag.PracticesID = new SelectList(db.Practices, "ID", "Name", sessions.PID);
            return View(sessions);
        }
        
        //
        // GET: /Sessions/Edit/5
 
        public ActionResult Edit(Guid id)
        {

            Person per = Session["person"] as Person;
            Sessions sessions = db.Sessions.Find(id);
            ViewBag.PracticesID = new SelectList(db.Practices.Where(a=>a.PersonID== per.ID), "ID", "Name", sessions.PID);
            return View(sessions);
        }

        //
        // POST: /Sessions/Edit/5

        [HttpPost]
        public ActionResult Edit(Sessions sessions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessions).State = EntityState.Modified;
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }
            ViewBag.PracticesID = new SelectList(db.Practices, "ID", "Name", sessions.PID);
            return View(sessions);
        }

        //
        // GET: /Sessions/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Sessions sessions = db.Sessions.Find(id);
            return View(sessions);
        }

        //
        // POST: /Sessions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Sessions sessions = db.Sessions.Find(id);
            db.Sessions.Remove(sessions);
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