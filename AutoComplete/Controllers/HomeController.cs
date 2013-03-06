using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler.Data;
using AutoComplete.Models;
using System.Data;
using System.Data.Entity;
using Mvc.Mailer;
using AutoComplete.Mailers;
// Author: Brother Bill, brotherbill@mail.com, May freely make changes and copies.
namespace AutoComplete.Controllers
{

    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }
        // GET: /Home/
        public ActionResult Index()
        {

            var user = Session["user"] as User;
            // Session["Person"] = 1;
            // var model = new Country() { Id = 1, Name = "Bulgaria" };
            return View();
        }
        [Authorize]
        public ActionResult IndexLocum()
        {

            // var model = new Country() { Id = 1, Name = "Bulgaria" };

            return View();
        }


        // GET: /Home/Ajax
        public ActionResult Ajax()
        {
            return View();
        }

        public ActionResult news()
        {
            var x = db.webcontent.Where(a => a.title == "News&Events").FirstOrDefault();
            return View(x);
        }

        [HttpPost]

        public ActionResult news(webcontent news)
        {

            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            return View();
        }
        // GET: /Home/Events/
        public ActionResult Events()
        {
            return View();
        }

        // GET: /Home/Api/
        public ActionResult Api(int? id)
        {
            ViewBag.Message = "About us";
            var PageContent = db.webcontent.Where(p => p.PageID == id).FirstOrDefault();
            return View(PageContent);
            // return View();
        }

        public ActionResult CheckSession()
        {
            if (Session["person"] == null)
                return Content(Boolean.FalseString);
            else
                return Content(Boolean.TrueString);
        }

        public ActionResult SessionTimeOut()
        {
            Session.Abandon();
            Session.Clear();
            return View();
        }

        public ActionResult Receipt()
        {
            return View();
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgotPassword(Person person)
        {
            var senddetail = db.People.Where(c => c.Email == person.Email).FirstOrDefault();
            if (senddetail != null)
            {
                _userMailer.ForgotPass(senddetail).Send();

                return View("forgotpasswordSuccess");
            }


            return View("_EmailNotExist");
        }





        [HttpPost]
        public JsonResult GetAllAppointments(IEnumerable<Appointment> AP, Guid? Empid)
        {
            var data = new List<FullCalendarEvent>();
            var count = 0;
            var User = Session["Person"] as Person;
            var Appointment = new Appointment();
            Appointment.PersonID = User.ID;
            foreach (var item in db.Appointments.Include(p => p.sessionDetail).Include(p=>p.Parctice).Where(A=>A.PersonID==User.ID))
            {
                DateTime start = Convert.ToDateTime(item.startDate);
                DateTime stTime = Convert.ToDateTime(item.sessionDetail.startTime);
                DateTime end = Convert.ToDateTime(item.endDate);
                DateTime enTime = Convert.ToDateTime(item.sessionDetail.EndTime);
                var color1="";
                if (item.Invoiced == true)
                {
                    if (item.Paid == true)
                    {
                        color1 = "#00a300";
                    }
                    else
                    {
                        color1 = "#EE4646";
                    }
                }
                else
                {
                    color1 = "#2d89ef";
                }


                var sesstionStartTime = Convert.ToDateTime(item.sessionDetail.startTime);
                var sesstionEndTime = Convert.ToDateTime(item.sessionDetail.EndTime);
                var Cal = new FullCalendarEvent()
                {
                   
                    id = item.APPID,
                 //   Aid = item.APPID,
                    title = item.Parctice.Name,
                    Sessiontitle = item.sessionDetail.Name,
                    //SessionStart =item.sessionDetail.startTime,
                    //Sessionend = item.sessionDetail.EndTime,
                    SessionStart=sesstionStartTime.ToShortTimeString(),
                    Sessionend=sesstionEndTime.ToString("s"),
                    color=color1,
                    start =new DateTime(start.Year, start.Month,start.Day,sesstionStartTime.Hour,sesstionStartTime.Minute,sesstionStartTime.Second).ToString("s"),
                    end = new DateTime(end.Year, end.Month, end.Day, sesstionEndTime.Hour, sesstionEndTime.Minute, sesstionEndTime.Second).ToString("s")
                };
                data.Add(Cal);
            }
            var returnData = data.ToArray();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

    }
}


