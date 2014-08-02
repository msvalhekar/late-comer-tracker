using System.Collections.Generic;
using System.Web.Http;
using LateComerTracker.Backend.Model;
using LateComerTracker.Backend.Service;

namespace LateComerTracker.Api.Controllers
{
    public class EmploeeController : ApiController
    {
        // GET api/user
        public IEnumerable<Employee> Get()
        {
            return new EmployeeService().GetEmployees();
        }

        // GET api/user/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
