using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LateComerTracker.Backend.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public IList<Employee> Employees { get; set; }
    }
}
