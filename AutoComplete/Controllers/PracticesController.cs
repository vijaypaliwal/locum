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
    public class PracticesController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Practices/

        public ViewResult Index()
        {
            var practices = db.Practices.Include(p => p.person);
            Person sperson = Session["person"] as Person;
            ViewBag.LocumID = sperson.ID;
            return View(practices.ToList());
        }

        public JsonResult GetAllPractices(Guid LocumID)
        {
            Person sperson = Session["person"] as Person;
            return Json(db.Practices.Where(a => a.PersonID == sperson.ID).Select(d => new { d.ID, d.Name, d.CAtPractice, d.cEmail, d.city, d.cphone, d.CPosition, d.Pphone }), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Practices/Details/5

        public ViewResult Details(Guid id)
        {
            Practices practices = db.Practices.Find(id);
            return View(practices);
        }

        //
        // GET: /Practices/Create

        public ActionResult Create()
        {
            Person sperson = Session["person"] as Person;
            ViewBag.PersonID = new SelectList(db.People, "ID", "Title", sperson.ID);
            var person = new Practices { PersonID = sperson.ID };
            return View(person);
        }

        //
        // POST: /Practices/Create

        [HttpPost]
        public ActionResult Create(Practices practices)
        {
            if (ModelState.IsValid)
            {
                practices.ID = Guid.NewGuid();
                db.Practices.Add(practices);
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }

            ViewBag.PersonID = new SelectList(db.People, "ID", "Title", practices.PersonID);
            return View(practices);
        }

        //
        // GET: /Practices/Edit/5

        public ActionResult Edit(Guid id)
        {
            Practices practices = db.Practices.Find(id);
            ViewBag.PersonID = new SelectList(db.People, "ID", "Title", practices.PersonID);
            return View(practices);
        }

        //
        // POST: /Practices/Edit/5

        [HttpPost]
        public ActionResult Edit(Practices practices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(practices).State = EntityState.Modified;
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }
            ViewBag.PersonID = new SelectList(db.People, "ID", "Title", practices.PersonID);
            return View(practices);
        }

        //
        // GET: /Practices/Delete/5

        public ActionResult Delete(Guid id)
        {
            Practices practices = db.Practices.Find(id);
            return View(practices);
        }

        //
        // POST: /Practices/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Practices practices = db.Practices.Find(id);
            db.Practices.Remove(practices);
            db.SaveChanges();
            return Content(Boolean.TrueString);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}