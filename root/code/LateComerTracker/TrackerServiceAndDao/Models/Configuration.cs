using System.ComponentModel.DataAnnotations;

namespace LateComerTracker.Backend.Models
{
    public class Configuration
    {
        [Required]
        public string Key { get; set; }
        
        public string Value { get; set; }
    }
}
