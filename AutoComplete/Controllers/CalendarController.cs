using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using AutoComplete.Models;
namespace AutoComplete.Controllers
{
    public class CalendarController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Terrace;
            scheduler.InitialDate = new DateTime(2012, 09, 03);            
            scheduler.Config.multi_day = true;//render multiday events
            
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = false;
            return View(scheduler);
        }

        public ContentResult Data()
        {
            var data = new List<CalendarEvent>();
         
            var count = 0;
           
            foreach (var item in db.Appointments.Include(p => p.sessionDetail))
            {
                DateTime start = Convert.ToDateTime(item.startDate);
                DateTime stTime = Convert.ToDateTime(item.sessionDetail.startTime);
                DateTime end = Convert.ToDateTime(item.endDate);
                DateTime enTime = Convert.ToDateTime(item.sessionDetail.EndTime);
                var Cal = new CalendarEvent()
                {
                    id = count++,
                    text = "Meeting Schedule",
                    start_date = new DateTime(start.Year, start.Month,start.Day, stTime.Hour,stTime.Month,stTime.Second),
                    end_date = new DateTime(end.Year, end.Month, end.Day, enTime.Hour, enTime.Month, enTime.Second)
                };
                data.Add(Cal);
            }
            var returnData = new SchedulerAjaxData(data);
            return (ContentResult)returnData;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (CalendarEvent)DHXEventsHelper.Bind(typeof(CalendarEvent), actionValues);



                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        //do insert
                        action.TargetId = changedEvent.id;//assign postoperational id
                        break;
                    case DataActionTypes.Delete:
                        //do delete
                        break;
                    default:// "update"                          
                        //do update
                        break;
                }
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

