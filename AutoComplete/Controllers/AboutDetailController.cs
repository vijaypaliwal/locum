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
    public class AboutDetailController : Controller
    {

        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }
        private DataContext db = new DataContext();

        //
        // GET: /AboutDetail/

        public ViewResult Index()
        {
            var aboutperson = db.AboutPerson.Include(a => a.person);
            return View(aboutperson.ToList());
        }

        //
        // GET: /AboutDetail/Details/5

        public ViewResult Details(Guid id)
        {
            AboutPerson aboutperson = db.AboutPerson.Find(id);

            return View(aboutperson);
        }

        //
        // GET: /AboutDetail/Create

        public ActionResult Create(Guid id)
        {
            //var aboutperson = db.AboutPerson.Include(a => a.person);
            var aboutperson = db.AboutPerson.Include(m => m.person).Where(p => p.PersonId == id).FirstOrDefault();
          //  var per = Session["Person"] as Person;

            if (aboutperson == null)
            {
                aboutperson = new AboutPerson();
                aboutperson.Id = Guid.NewGuid();
                aboutperson.PersonId = id;
            }

            return View(aboutperson);
        }

        public PartialViewResult _schedule()
        {
            return PartialView();
        }


        public PartialViewResult contactus()
        {
            return PartialView();
        }


        public ActionResult Contactinformation()
        {
            var x = db.webcontent.Where(a => a.title == "contactus").FirstOrDefault();
            return View(x);
         
        }

        [HttpPost]
            public ActionResult Contactinformation(webcontent contactInformation)
        {

            db.Entry(contactInformation).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }










        [HttpPost]
        public ActionResult ContactUs(ContactUs ContactUs)
        {
            var user = Session["Person"] as Person;
            string email = user != null ? user.Email : "gautam.paliwal88@gmail.com";
            _userMailer.ContactUs(ContactUs, email).Send();
            return Content(Boolean.TrueString);
        }


        public ActionResult OuterContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OuterContactUs(ContactUs ContactUs)
        {
            var user = Session["Person"] as Person;
            string email = user != null ? user.Email : "gautam.paliwal88@gmail.com";
            _userMailer.ContactUs(ContactUs, email).Send();
            return Content(Boolean.TrueString);
        }



        //
        // POST: /AboutDetail/Create

        [HttpPost]
        public ActionResult Create(AboutPerson aboutperson)
        {
            if (ModelState.IsValid)
            {
                aboutperson.person = db.People.Where(p => p.ID == aboutperson.PersonId).FirstOrDefault();
                if (aboutperson.person != null)
                {
                    aboutperson.Id = Guid.NewGuid();
                    db.AboutPerson.Add(aboutperson);
                    db.SaveChanges();
                }
                else
                {
                    var aboutperso = new AboutPerson();
                    aboutperso.AboutDetail = aboutperson.AboutDetail;
                    aboutperson = db.AboutPerson.Include(p => p.person).Where(m => m.PersonId == aboutperson.PersonId).FirstOrDefault();
                    aboutperson.AboutDetail = aboutperso.AboutDetail;
                    db.Entry(aboutperson).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return View(aboutperson);
            }

            ViewBag.PersonId = new SelectList(db.People, "ID", "Title", aboutperson.PersonId);
            return View(aboutperson);
        }

        //
        // GET: /AboutDetail/Edit/5

        public ActionResult Edit(Guid id)
        {
            AboutPerson aboutperson = db.AboutPerson.Find(id);
            ViewBag.PersonId = new SelectList(db.People, "ID", "Title", aboutperson.PersonId);
            return View(aboutperson);
        }

        //
        // POST: /AboutDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(AboutPerson aboutperson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aboutperson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.People, "ID", "Title", aboutperson.PersonId);
            return View(aboutperson);
        }

        //
        // GET: /AboutDetail/Delete/5

        public ActionResult Delete(Guid id)
        {
            AboutPerson aboutperson = db.AboutPerson.Find(id);
            return View(aboutperson);
        }

        //
        // POST: /AboutDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AboutPerson aboutperson = db.AboutPerson.Find(id);
            db.AboutPerson.Remove(aboutperson);
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