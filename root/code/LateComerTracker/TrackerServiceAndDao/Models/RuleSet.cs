using System.Collections.Generic;

namespace LateComerTracker.Backend.Models
{
    public enum RuleSetType
    {
        Safe,
        NotSafe,
        Notes
    }

    public class RuleSet
    {
        public RuleSetType Type { get; set; }

        public string Title { get; set; }
        
        public IList<string> Rules { get; set; }
    }
}
