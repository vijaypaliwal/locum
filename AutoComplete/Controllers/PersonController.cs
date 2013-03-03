using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoComplete.Models;
using AutoComplete;
using System.Web.Security;
using Mvc.Mailer;
using AutoComplete.Mailers;


namespace AutoComplete.Controllers
{
   
    public class PersonController : Controller
    {
        private DataContext db = new DataContext();

        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }

        //
        // GET: /Person/

        public ViewResult Index()
        {
            List<Person> obj = new List<Person>();
            return View(obj);
        }


        public JsonResult GetAllPerson()
        {
            return Json(db.People.Select(p=>new {p.ID,p.FirstName,p.LastName,p.UserName,p.phone,p.Title,p.Email}),JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Person/Details/5

        public ViewResult Details(Guid id)
        {
            Person person = db.People.Find(id);
            return View(person);
        }

        //
        // GET: /Person/Create

        public ActionResult Create()
        {
           var title=new Person(){ Title="Dr."};
            return View(title);
        }

        //
        // POST: /Person/Create
        [Recaptcha.RecaptchaControlMvc.CaptchaValidator]
        [HttpPost]
        public ActionResult Create(Person person, bool captchaValid)
        {
            //edited some text here
            //check for username existance
           var Check = from s in db.People
                        where s.UserName == person.UserName
                        select s;
            int count = Check.Count();
            
            if (count > 0)
            {
                ModelState.AddModelError("_FORM", "This Username is already Exist. Try with another Username.");
            }

            //chect for email id existance
            var Check1 = from s in db.People
                        where s.Email == person.Email
                        select s;
            int count1 = Check1.Count();
            if (count1 > 0)
            {
                ModelState.AddModelError("_FORM", "Email Id is already exist.You can't register with same Email Id.");
            }
            

            if (ModelState.IsValid)
            {
                if (captchaValid)
                {
                    
                    person.ID = Guid.NewGuid();
                    db.People.Add(person);
                    db.SaveChanges();                    
                    var obj = db.Roles.Where(r => r.RoleName == "Locum").FirstOrDefault();
                    if (obj != null)
                    {
                        obj.Persons.Add(person);
                    }
                    db.SaveChanges();
                    _userMailer.Welcome(person).Send();
                    return RedirectToAction("Index","Home");
                    
                }
                else
                {
                    ModelState.AddModelError("_FORM", "You did not type the verification word correctly. Please try again.");
                }

            }

            return View(person);
        }

        //
        // GET: /Person/Edit/5

        public ActionResult Edit(Guid id)
        {
            Person person = db.People.Find(id);
            return View(person);
        }

        //
        // POST: /Person/Edit/5

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }
            return View(person);
        }




        public ActionResult Edit1(Guid id)
        {
            Person person = db.People.Find(id);
            return View(person);
        }

        //
        // POST: /Person/Edit/5

        [HttpPost]
        public ActionResult Edit1(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;

                Session["Person"] = person;
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }
            return View(person);
        }









        //
        // GET: /Person/Delete/5

        public ActionResult Delete(Guid id)
        {
            Person person = db.People.Find(id);
            return View(person);
        }

        //
        // POST: /Person/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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