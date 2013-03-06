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
    public class bookmarkController : Controller
    {

        private DataContext db = new DataContext();
        //
        // GET: /bookmark/

        public ViewResult Index()
        {
            return View();
        }


        public ActionResult GetAllbookmarks()
        {
            var bookmarks = db.bookmarks;
            return Json(bookmarks.Select(bk => new { bk.ID, bk.Btitle, bk.url, bk.Btype, bk.Description  }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
          
            return View();
        }

        //
        // POST: /Sessions/Create

        [HttpPost]
        public ActionResult Create(bookmarks bookmarks)
        {
            if (ModelState.IsValid)
            {
              
                    db.bookmarks.Add(bookmarks);
                    db.SaveChanges();
                    return Content(Boolean.TrueString);
              
            }
            return View(bookmarks);
        }



        public ActionResult Edit(int id)
        {
            bookmarks bookmark = db.bookmarks.Find(id);

            return View(bookmark);
        }



        //
        // POST: /Sessions/Edit/5

        [HttpPost]
        public ActionResult Edit(bookmarks Bookmarks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Bookmarks).State = EntityState.Modified;
                db.SaveChanges();
                return Content(Boolean.TrueString);
            }
            return View(Bookmarks);
        }


        public ViewResult Details(int id)
        {
            bookmarks Bookmarks = db.bookmarks.Find(id);
            return View(Bookmarks);
        }




        //
        // GET: /Sessions/Delete/5

        public ActionResult Delete(int id)
        {
            bookmarks biookmarks = db.bookmarks.Find(id);
            return View(biookmarks);
        }



        //
        // POST: /Sessions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            bookmarks bookmarks = db.bookmarks.Find(id);
            db.bookmarks.Remove(bookmarks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
