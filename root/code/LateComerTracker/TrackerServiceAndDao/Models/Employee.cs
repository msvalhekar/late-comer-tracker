using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LateComerTracker.Backend.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        
        public int UnsettledPoints { get; set; }

        public IList<Attendance> AttendanceList { get; set; }
        
        public IList<Penalty> PenaltyList { get; set; }
       
        public string FormattedDuePenalties {
            get
            {
                // 0 0 - 0
                // 0 N - 0 (+nnn pnts)
                // N 0 - NNN
                // N N - NNN (+nnn pnts)
                if (UnsettledPoints == 0) return "0";

                var dueRoundPenalties = (uint)(UnsettledPoints/Configurations.PointsPerPenalty);
                var duePoints = UnsettledPoints % Configurations.PointsPerPenalty;

                var duePointsStr = (0 < duePoints) ? string.Format("(+{0} pnts)", duePoints) : string.Empty;
                return string.Format("{0} {1}", GetDuePenalties(dueRoundPenalties), duePointsStr);
            } 
        }

        public bool HasPenalties {
            get
            {
                var dueRoundPenalties = (uint)UnsettledPoints / Configurations.PointsPerPenalty;
                return (dueRoundPenalties > 0);                
            }
        }

        private static readonly Dictionary<uint, string> NumberMap = new Dictionary<uint, string>();

        static Employee()
        {
            NumberMap.Add(0, "0");
            NumberMap.Add(1, "One");
            NumberMap.Add(2, "Two");
            NumberMap.Add(3, "Three");
            NumberMap.Add(4, "Four");
            NumberMap.Add(5, "Five");
            NumberMap.Add(6, "Six");
            NumberMap.Add(7, "Seven");
            NumberMap.Add(8, "Eight");
            NumberMap.Add(9, "Nine");
            NumberMap.Add(10, "Ten");
        }

        private string GetDuePenalties(uint penalties)
        {
            return NumberMap.ContainsKey(penalties)
                ? NumberMap[penalties]
                : penalties.ToString();
        }
    }
}
