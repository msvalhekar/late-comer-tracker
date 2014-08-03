using System.Collections.Generic;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
{
    public class EmployeeService
    {
        public IList<Employee> GetAllEmployees()
        {
            return new EmployeeDao().GetAllEmployees();
        }

        public Employee GetEmployee(int id)
        {
            return new EmployeeDao().GetEmployee(id);
        }
    }
}
