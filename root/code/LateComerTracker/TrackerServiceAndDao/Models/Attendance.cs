using System.Collections.Generic;

namespace LateComerTracker.Backend.Models
{
    public class Attendance
    {
        public int TeamId { get; set; }
        public int MeetingId { get; set; }
        public IList<int> EmployeeIds { get; set; }
        public string Source { get; set; }
    }
}
