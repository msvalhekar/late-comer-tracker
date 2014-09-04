using System;
using System.Collections.Generic;

namespace LateComerTracker.Backend.Models
{
    public class Attendance
    {
        public DateTime LateDateTime { get; set; }
        public string Source { get; set; }

        public string TeamName { get; set; }
        public string EmployeeName { get; set; }
        public string MeetingName { get; set; }
        public string Reason { get; set; }

        public string LateDateTimeString {
            get { return LateDateTime.ToString("dd-MMM-yyyy dddd"); }
        }
    }
}
