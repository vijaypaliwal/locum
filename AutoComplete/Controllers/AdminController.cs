using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using AutoComplete.Models;

namespace AutoComplete.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        public DataContext db = new DataContext(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);

        //
        // GET: /Admin/Admin/

        public ActionResult Index()
        {
            return View(db.webcontent.ToList());
        }



        public ViewResult Details(int id)
        {
            webcontent webcontent = db.webcontent.Find(id);
            return View(webcontent);
        }

        //
        // GET: /Admin/Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Admin/Create

        [HttpPost]
        public ActionResult Create(webcontent webcontent)
        {
            if (ModelState.IsValid)
            {
                db.webcontent.Add(webcontent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webcontent);
        }

        //
        // GET: /Admin/Admin/Edit/5

        public ActionResult Edit(int id)
        {
            webcontent webcontent = db.webcontent.Find(id);
            return View(webcontent);
        }

        //
        // POST: /Admin/Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(webcontent webcontent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webcontent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webcontent);
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string email, password;
            email = Request["txt_email"];
            password = Request["txt_password"];
            if (email == "Admin" || email == "admin" && password == "Admin" ||  password == "admin")
                return RedirectToAction("index");
            else
                return Content("Please Fill Correct User name and Password");
        }



        //
        // GET: /Admin/Admin/Delete/5

        public ActionResult Delete(int id)
        {
            webcontent webcontent = db.webcontent.Find(id);
            return View(webcontent);
        }

        //
        // POST: /Admin/Admin/Delete/5


        public string DeleteConfirmed(int id)
        {
            webcontent webcontent = db.webcontent.Find(id);
            db.webcontent.Remove(webcontent);
            db.SaveChanges();
            return "done";
        }






    }
}
