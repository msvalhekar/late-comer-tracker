using System.Collections.Generic;

namespace LateComerTracker.Backend.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Employee> Employees { get; set; }
    }
}
