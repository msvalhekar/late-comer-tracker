using System.Collections.Generic;
using System.Web.Http;
using LateComerTracker.Api.Filters;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    [ValidateModel]
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            return new EmployeeService().GetAll();
        }

        public Employee Get(int id)
        {
            return new EmployeeService().Get(id);
        }

        public Employee GetByName(string name)
        {
            return new EmployeeService().Get(name);
        }

        public Employee Post(Employee employee)
        {
            employee = new EmployeeService().Add(employee);
            if (employee.Id == 0) return null;
            return employee;
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public bool Delete(int id)
        {
            return new EmployeeService().Delete(id);
        }


        public void MarkLate(int meetingId, IList<int> employeeIds, string source)
        {
            new EmployeeService().MarkLate(meetingId, employeeIds, source);
        }
    }
}
