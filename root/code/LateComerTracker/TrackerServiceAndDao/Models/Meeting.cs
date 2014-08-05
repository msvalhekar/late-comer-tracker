using System.ComponentModel.DataAnnotations;

namespace LateComerTracker.Backend.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int Severity { get; set; }
    }
}
