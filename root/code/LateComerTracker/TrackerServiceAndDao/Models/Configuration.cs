using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Backend.Models
{
    public class Configuration
    {
        [Required]
        public string Key { get; set; }
        
        public string Value { get; set; }
    }

    public static class Configurations
    {
        private const string PointsPerPenaltyKey = "PointsPerPenalty";
        private static readonly IList<Configuration> _configurations;

        static Configurations()
        {
            _configurations = new ConfigurationService().GetAll().ToList();
        }

        public static int PointsPerPenalty {
            get
            {
                var setting = _configurations.FirstOrDefault(x => x.Key == PointsPerPenaltyKey);
                if (setting == null) return 10;
                return Convert.ToInt32(setting.Value);
            }
        }
    }
}
