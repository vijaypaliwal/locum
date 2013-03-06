using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoComplete.Models;
using AutoComplete;
using System.IO;

namespace AutoComplete.Controllers
{ 
    public class AttachmentsController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Attachments/

        public ViewResult Index()
        {
            var User = Session["Person"] as Person;
            var attachments = db.Attachments.Include(a => a.person).Where(a => a.person.ID == User.ID);
            return View(attachments.ToList());
        }

        //
        // GET: /Attachments/Details/5

        public ViewResult Details(Guid id)
        {
            Attachments attachments = db.Attachments.Find(id);
            return View(attachments);
        }

        [HttpPost]
        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachments)
        {
            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                try
                {
                    // Some browsers send file names with full path. This needs to be stripped.
                    var fileName = Path.GetFileName(file.FileName);
                    Attachments attachmentObj = new Attachments();
                    attachmentObj.ID = Guid.NewGuid();
                    attachmentObj.path = fileName;                    
                    attachmentObj.AttachmentpersonID = (Session["person"] as Person).ID;
                    attachmentObj.title = "Uploaded item on " + DateTime.Now.ToShortDateString();
                    db.Attachments.Add(attachmentObj);
                    db.SaveChanges();
                    var physicalPath = Path.Combine(Server.MapPath("~/Attachments"), fileName);

                    file.SaveAs(physicalPath);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_form", ex.Message);
                }
            }
          
            // Return an empty string to signify success
            return Content("");
        }

        [HttpPost]
        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Attachments"), fileName);
                Attachments attachments = db.Attachments.Where(p => p.path == fileName).FirstOrDefault();
                if (attachments != null)
                {
                    db.Attachments.Remove(attachments);
                    db.SaveChanges();
                }
                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        //
        // GET: /Attachments/Create

        public ActionResult Create()
        {
            ViewBag.personID = new SelectList(db.People, "ID", "Title");
            return View();
        } 

        //
        // POST: /Attachments/Create

        [HttpPost]
        public ActionResult Create(Attachments attachments)
        {
            if (ModelState.IsValid)
            {
                attachments.ID = Guid.NewGuid();
                db.Attachments.Add(attachments);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.personID = new SelectList(db.People, "ID", "Title", attachments.AttachmentpersonID);
            return View(attachments);
        }
        
        //
        // GET: /Attachments/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Attachments attachments = db.Attachments.Find(id);
            ViewBag.personID = new SelectList(db.People, "ID", "Title", attachments.AttachmentpersonID);
            return View(attachments);
        }

        //
        // POST: /Attachments/Edit/5

        [HttpPost]
        public ActionResult Edit(Attachments attachments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attachments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.personID = new SelectList(db.People, "ID", "Title", attachments.AttachmentpersonID);
            return View(attachments);
        }

        //
        // GET: /Attachments/Delete/5
 
   

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Attachments attachments = db.Attachments.Find(id);
            db.Attachments.Remove(attachments);
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