using System.Collections.Generic;
using System.Web.Http;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            return new EmployeeService().GetAllEmployees();
        }

        public Employee Get(int id)
        {
            return new EmployeeService().GetEmployee(id);
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
