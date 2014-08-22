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
        
        public int TotalPoints { get; set; }
        
        public int UnsettledPoints { get; set; }
       
        public string DuePenalties {
            get
            {
                if (UnsettledPoints == 0) return "0 penalties";

                var dueRoundPenalties = (uint)UnsettledPoints/Configurations.PointsPerPenalty;
                var duePoints = UnsettledPoints % Configurations.PointsPerPenalty;

                if (dueRoundPenalties == 0) return string.Format("{0} points", duePoints);
                if (dueRoundPenalties == 1)
                {
                    if (duePoints == 0) return string.Format("1 penalty");
                    return string.Format("1 penalty {0} points", duePoints);
                }
                if (duePoints == 0) return string.Format("{0} penalties", dueRoundPenalties);
                return string.Format("{0} penalties {1} points", dueRoundPenalties, duePoints);
            } 
        }

        public bool HasPenalties {
            get
            {
                var dueRoundPenalties = (uint)UnsettledPoints / Configurations.PointsPerPenalty;
                return (dueRoundPenalties > 0);                
            }
        }
    }
}
