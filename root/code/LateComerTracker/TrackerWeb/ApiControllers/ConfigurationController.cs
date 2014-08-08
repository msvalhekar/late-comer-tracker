using System.Collections.Generic;
using System.Web.Http;
using LateComerTracker.Api.Filters;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    [ValidateModel]
    public class ConfigurationController : ApiController
    {
        public IEnumerable<Configuration> Get()
        {
            return new ConfigurationService().GetAll();
        }

        //public Configuration Post(Configuration configuration)
        //{
        //    return new ConfigurationService().Add(configuration);
        //}

        public void Put(Configuration configuration)
        {
            new ConfigurationService().Update(configuration);
        }

        //public bool Delete(Configuration configuration)
        //{
        //    return new ConfigurationService().Delete(configuration);
        //}
    }
}
