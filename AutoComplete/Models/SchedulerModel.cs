using System;

namespace AutoComplete.Models
{
    public class CalendarEvent
    {
        //id, text, start_date and end_date properties are mandatory
        public int id { get; set; }
        public string text { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }


    }

    public class FullCalendarEvent
    {
        //id, text, start_date and end_date properties are mandatory
        public Guid id { get; set; }
        public Guid Aid { get; set; }
        public string title { get; set; }
        public string Sessiontitle { get; set; }
        public string SessionStart { get; set; }

        
        public string Sessionend { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string color { get; set; }
    }
}