using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoComplete.Models;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;

namespace AutoComplete.Controllers
{
    public class ExpencesController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Expences/

        public ViewResult Index(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            ViewBag.Count = db.Expences.Count();
            ViewBag.Start = start;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.OrderBy = orderBy;
            ViewBag.Desc = desc;

            return View();
        }

        //
        // GET: /Expences/GridData/?start=0&itemsPerPage=20&orderBy=ID&desc=true

        public ActionResult GridData(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            var User = Session["Person"] as Person;

            Response.AppendHeader("X-Total-Row-Count", db.Expences.Count().ToString());
            ObjectQuery<Expences> expences = (db as IObjectContextAdapter).ObjectContext.CreateObjectSet<Expences>();
            expences =(ObjectQuery<Expences>) expences.Include(e => e.person);
            expences = expences.OrderBy("it." + orderBy + (desc ? " desc" : ""));

            return PartialView(expences.Skip(start).Take(itemsPerPage).Where(a=>a.personID == User.ID));
        }

        //
        // GET: /Default5/RowData/5

        public ActionResult RowData(Guid id)
        {
            Expences expences = db.Expences.Find(id);
            return PartialView("GridData", new Expences[] { expences });
        }

        //
        // GET: /Expences/Create

        public ActionResult Create()
        {
            ViewBag.personID = new SelectList(db.People, "ID", "FirstName");
            return PartialView("Edit");
        }

        //
        // POST: /Expences/Create

        [HttpPost]
        public ActionResult Create(Expences expences)
        {
            var User = Session["Person"] as Person;
            expences.personID = User.ID;


            if (ModelState.IsValid)
            {
                expences.ID = Guid.NewGuid();
                db.Expences.Add(expences);
                db.SaveChanges();
                return PartialView("GridData", new Expences[] { expences });
            }

            ViewBag.personID = new SelectList(db.People, "ID", "Title", expences.personID);
            return PartialView("Edit", expences);
        }


        public ActionResult ExportDataFromExcel()
        {
            var User = Session["Person"] as Person;
            OleDbConnection oconn = new OleDbConnection(ConfigurationManager.ConnectionStrings["ExcelCon"].ConnectionString);
            // connectionstring to connect to the Excel Sheet
            try
            {
                //After connecting to the Excel sheet here we are selecting the data 
                //using select statement from the Excel sheet
                OleDbCommand ocmd = new OleDbCommand("select * from [IplansInfo$]", oconn);
                oconn.Open();  //Here [Sheet1$] is the name of the sheet 
                //in the Excel file where the data is present
                OleDbDataReader odr = ocmd.ExecuteReader();
                Guid Id = new Guid();
                DateTime EDate = DateTime.Now;
                Guid PersonID = User.ID;
                string Desc = "";
                string Amount = "";
                //Guid CategoryID = new Guid();

                while (odr.Read())
                {
                    Id = Guid.NewGuid();
                    EDate = DateTime.Now;//Here we are calling the valid method
                    PersonID = User.ID;
                    Desc = valid(odr, 2);
                    Amount = valid(odr, 3);
                   // CategoryID = Guid.NewGuid();

                    //Here using this method we are inserting the data into the database
                    insertdataintosql(Id, EDate, PersonID, Desc, Amount );
                }
                oconn.Close();
            }
            catch (DataException ee)
            {

            }
            finally
            {

            }

            return Content(Boolean.TrueString);
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
                   
                 
                    var physicalPath =Server.MapPath("~/Attachments");

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



        protected string valid(OleDbDataReader myreader, int stval)//if any columns are 
        //found null then they are replaced by zero
        {
            object val = myreader[stval];
            if (val != DBNull.Value)
                return val.ToString();
            else
                return Convert.ToString(0);
        }
        private void insertdataintosql(Guid Id, DateTime EDate, Guid PersonID, string Desc, string Amount)
        {


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Expences(ID,EDate,personID,Description,Amount)values(@ID,@EDate,@personID,@Description,@Amount)";
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Id;
            cmd.Parameters.Add("@EDate", SqlDbType.DateTime).Value = EDate;
            cmd.Parameters.Add("@personID", SqlDbType.UniqueIdentifier).Value = PersonID;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Desc;
            cmd.Parameters.Add("@Amount", SqlDbType.NVarChar).Value = Amount;
            //cmd.Parameters.Add("@Category_ID", SqlDbType.UniqueIdentifier).Value = CategoryID;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }






        //
        // GET: /Expences/Edit/5

        public ActionResult Edit(Guid id)
        {
            Expences expences = db.Expences.Find(id);
            ViewBag.personID = new SelectList(db.People, "ID", "FirstName", expences.personID);
            return PartialView(expences);
        }

        //
        // POST: /Expences/Edit/5

        [HttpPost]
        public ActionResult Edit(Expences expences)
        {
            var user = Session["Person"] as Person;
            expences.personID = user.ID;


            if (ModelState.IsValid)
            {
                db.Entry(expences).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("GridData", new Expences[] { expences });
            }

            ViewBag.personID = new SelectList(db.People, "ID", "FirstName", expences.personID);
            return PartialView(expences);
        }

        //
        // POST: /Expences/Delete/5

        [HttpPost]
        public void Delete(Guid id)
        {
            Expences expences = db.Expences.Find(id);
            db.Expences.Remove(expences);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
