using System.Collections.Generic;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
{
    public class ConfigurationService
    {
        public IEnumerable<Configuration> GetAll()
        {
            return new ConfigurationDao().GetAll();
        }

        //public Configuration Add(Configuration configuration)
        //{
        //    return new ConfigurationDao().Add(configuration);
        //}

        //public bool Delete(Configuration configuration)
        //{
        //    return new ConfigurationDao().Delete(configuration);
        //}

        public void Update(Configuration configuration)
        {
            new ConfigurationDao().Update(configuration);
            Configurations.Reset();
        }
    }
}
