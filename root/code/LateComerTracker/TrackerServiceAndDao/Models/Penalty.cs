using System;

namespace LateComerTracker.Backend.Models
{
    public class Penalty
    {
        public int TeamId { get; set; }
        public int EmpId { get; set; }
        public string TeamName { get; set; }
        public string EmployeeName { get; set; }
        public string How { get; set; }
        public DateTime When { get; set; }
        public string Source { get; set; }

        public string WhenString
        {
            get { return When.ToString("dd-MMM-yyyy dddd"); }
        }
    }
}
