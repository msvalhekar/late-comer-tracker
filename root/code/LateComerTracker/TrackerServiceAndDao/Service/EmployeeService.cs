using System.Collections.Generic;
using LateComerTracker.Backend.Dao;
using LateComerTracker.Backend.Model;

namespace LateComerTracker.Backend.Service
{
    public class EmployeeService
    {
        public IList<Employee> GetEmployees()
        {
            return new EmployeeDao().GetEmployees();
        }
    }
}
