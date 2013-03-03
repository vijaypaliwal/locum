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
    public class SessionColorController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /SessionColor/

        public ViewResult Index()
        {
            var sessioncolors = db.SessionColors.Include(s => s.Sessions);
            return View(sessioncolors.ToList());
        }

        //
        // GET: /SessionColor/Details/5

        public ViewResult Details(int id)
        {
            SessionColor sessioncolor = db.SessionColors.Find(id);
            return View(sessioncolor);
        }

        //
        // GET: /SessionColor/Create

        public ActionResult Create()
        {
            ViewBag.SID = new SelectList(db.Sessions, "ID", "Name");
            return View();
        } 

        //
        // POST: /SessionColor/Create

        [HttpPost]
        public ActionResult Create(SessionColor sessioncolor)
        {

            
            if (ModelState.IsValid)
            {
                db.SessionColors.Add(sessioncolor);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.SID = new SelectList(db.Sessions, "ID", "Name", sessioncolor.SID);
            return View(sessioncolor);
        }
        
        //
        // GET: /SessionColor/Edit/5
 
        public ActionResult Edit(int id)
        {
            SessionColor sessioncolor = db.SessionColors.Find(id);
            ViewBag.SID = new SelectList(db.Sessions, "ID", "Name", sessioncolor.SID);
            return View(sessioncolor);
        }

        //
        // POST: /SessionColor/Edit/5

        [HttpPost]
        public ActionResult Edit(SessionColor sessioncolor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessioncolor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SID = new SelectList(db.Sessions, "ID", "Name", sessioncolor.SID);
            return View(sessioncolor);
        }

        //
        // GET: /SessionColor/Delete/5
 
        public ActionResult Delete(int id)
        {
            SessionColor sessioncolor = db.SessionColors.Find(id);
            return View(sessioncolor);
        }

        //
        // POST: /SessionColor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SessionColor sessioncolor = db.SessionColors.Find(id);
            db.SessionColors.Remove(sessioncolor);
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