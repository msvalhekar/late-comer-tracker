using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.DAOs
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

        public Employee GetEmployee(int id)
        {
            var commandText = "select e.emp_id, e.emp_name, e.emp_emailId from Employee e"
                + " where e.emp_id = " + id;

            var dataTable = GetDataTable(commandText);
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            return new Employee
            {
                Id = Convert.ToInt32(row["emp_id"]),
                Name = row["emp_name"].ToString(),
                EmailId = row["emp_emailId"].ToString()
            };
        }
    }
}
