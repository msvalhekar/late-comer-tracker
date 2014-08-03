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
        
        public int SettledPenalties { get; set; }
    }
}
