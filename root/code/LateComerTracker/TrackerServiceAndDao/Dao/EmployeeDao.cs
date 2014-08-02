using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LateComerTracker.Backend.Model;

namespace LateComerTracker.Backend.Dao
{
    public class EmployeeDao : BaseDao
    {
        public IList<Employee> GetAllEmployees()
        {
            var table = GetDataTable("select emp_id, emp_name, emp_emailId from Employee");

            return (from DataRow row in table.Rows
                select new Employee
                {
                    Id = Convert.ToInt32(row["emp_id"]), 
                    Name = row["emp_name"].ToString(), 
                    EmailId = row["emp_emailId"].ToString()
                }).ToList();
        }
    }
}
