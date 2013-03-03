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
    public class AdditionalSurgeriesController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /AdditionalSurgeries/

        public ViewResult Index(Guid? LocumID)
        {

            //var additionalsurgeries = db.AdditionalSurgeries.Include(a => a.Practices);
            ViewBag.locumID = LocumID;
            List<AdditionalSurgeries> obj = new List<AdditionalSurgeries>();
            return View(obj);
        }

        public JsonResult getAllAdditional(Guid locumID)
        {
            var User = Session["Person"] as Person;
            var additionalsurgeries = db.AdditionalSurgeries.Include(a => a.Practices).Where(A=>A.Practices.PersonID == User.ID);
            
            return Json(additionalsurgeries.Select(asu=>new {asu.ID, asu.Name,asu.Rcdm,asu.SurgeryPhone,asu.VArea,asu.city}) ,JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /AdditionalSurgeries/Details/5

        public ViewResult Details(Guid id)
        {
            AdditionalSurgeries additionalsurgeries = db.AdditionalSurgeries.Find(id);
            return View(additionalsurgeries);
        }

        //
        // GET: /AdditionalSurgeries/Create

        public ActionResult Create()
        {
              var User = Session["Person"] as Person;
            ViewBag.PracticesID = new SelectList(db.Practices.Where(a=>a.PersonID== User.ID), "ID", "Name").ToArray();
            return View();
        }

        public ActionResult GetAllPractices()
        {
            Person per=Session["person"] as Person;
            var allpractices = db.Practices.Where(p => p.PersonID == per.ID);
            return Json(allpractices.Select(ap => new { ap.ID, ap.Name }), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /AdditionalSurgeries/Create

        [HttpPost]
        public ActionResult Create(AdditionalSurgeries additionalsurgeries)
        {
            if (ModelState.IsValid)
            {
                additionalsurgeries.ID = Guid.NewGuid();
                db.AdditionalSurgeries.Add(additionalsurgeries);
                db.SaveChanges();
                return Content(Boolean.TrueString);  
            }

            ViewBag.PracticesID = new SelectList(db.Practices, "ID", "Name", additionalsurgeries.PracticesID);
            return View(additionalsurgeries);
        }
        
        //
        // GET: /AdditionalSurgeries/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var User = Session["Person"] as Person;
            AdditionalSurgeries additionalsurgeries = db.AdditionalSurgeries.Find(id);
            ViewBag.PracticesID = new SelectList(db.Practices.Where(a=>a.PersonID==User.ID), "ID", "Name", additionalsurgeries.PracticesID);
            return View(additionalsurgeries);
        }

        //
        // POST: /AdditionalSurgeries/Edit/5

        [HttpPost]
        public ActionResult Edit(AdditionalSurgeries additionalsurgeries)
        {
          
            if (ModelState.IsValid)
            {
                db.Entry(additionalsurgeries).State = EntityState.Modified;
                db.SaveChanges();
                return Content(Boolean.TrueString); 
            }
            ViewBag.PracticesID = new SelectList(db.Practices, "ID", "Name", additionalsurgeries.PracticesID);
            return View(additionalsurgeries);
        }

        //
        // GET: /AdditionalSurgeries/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            AdditionalSurgeries additionalsurgeries = db.AdditionalSurgeries.Find(id);
            return View(additionalsurgeries);
        }

        //
        // POST: /AdditionalSurgeries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            AdditionalSurgeries additionalsurgeries = db.AdditionalSurgeries.Find(id);
            db.AdditionalSurgeries.Remove(additionalsurgeries);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

    public class TempPractices
    {
        public Guid ID { get; set; }
        public string PracName { get; set; }

    }
}