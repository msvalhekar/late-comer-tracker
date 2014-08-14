using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
{
    public class EmployeeService
    {
        public IList<Employee> GetAll()
        {
            return new EmployeeDao().GetAll();
        }

        public Employee Get(int id)
        {
            return new EmployeeDao().Get(id);
        }

        public Employee Get(string name)
        {
            return new EmployeeDao().Get(name);
        }

        public Employee Add(Employee employee)
        {
            return new EmployeeDao().Add(employee);
        }

        public bool Delete(int id)
        {
            return new EmployeeDao().Delete(id);
        }

        public void ServedPenalty(int empId, DateTime servedOn, string how, string source)
        {
            new EmployeeDao().ServedPenalty(empId, servedOn, how, source);
        }
    }
}
