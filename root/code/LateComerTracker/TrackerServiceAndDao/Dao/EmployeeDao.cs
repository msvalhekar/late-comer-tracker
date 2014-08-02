using System.Collections.Generic;
using LateComerTracker.Backend.Model;

namespace LateComerTracker.Backend.Dao
{
    public class EmployeeDao
    {
        public IList<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Meenalkumar",
                    TotalPoints = 0,
                    UnsettledPoints = 0,
                    SettledPenalties = 0
                },
                new Employee
                {
                    Id = 1,
                    Name = "Mohan",
                    TotalPoints = 15,
                    UnsettledPoints = 4,
                    SettledPenalties = 2
                }
            };
        }
    }
}
