using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.DAOs
{
    public class EmployeeDao : BaseDao
    {
        public IList<Employee> GetAll()
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

        public Employee Get(int id)
        {
            return GetWhere(new KeyValuePair<string, string>("emp_id", id.ToString(CultureInfo.InvariantCulture)));
        }

        public Employee Get(string name)
        {
            return GetWhere(new KeyValuePair<string, string>("emp_name", "'" + name + "'"));
        }

        private Employee GetWhere(KeyValuePair<string, string> wherePair)
        {
            var commandText = "select e.emp_id, e.emp_name, e.emp_emailId from Employee e"
                              + " where e." + wherePair.Key + " = " + wherePair.Value;

            var dataTable = GetDataTable(commandText);
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            var employee = new Employee
            {
                Id = Convert.ToInt32(row["emp_id"]),
                Name = row["emp_name"].ToString(),
                EmailId = row["emp_emailId"].ToString(),
                UnsettledPoints = 0
            };

            var empFineCommandText = "select unsettled_points from EmployeeFine"
                             + " where emp_id = " + employee.Id;

            employee.UnsettledPoints = ExecuteScalar(empFineCommandText);
            return employee;
        }

        public Employee Add(Employee employee)
        {
            if (employee == null) return null;

            var commandText = string.Format("INSERT INTO Employee (emp_name, emp_emailId) OUTPUT inserted.emp_id VALUES ('{0}', '{1}')",
                employee.Name, employee.EmailId);

            employee.Id = ExecuteScalar(commandText);
            return employee;
        }

        public bool Delete(int id)
        {
            var commandText = string.Format("DELETE EmployeeFine WHERE emp_id = {0};"
                                            + "DELETE TeamEmployee WHERE emp_id = {0};"
                                            + "DELETE Employee WHERE emp_id = {0}", id);

            return -1 < ExecuteNonQuery(commandText);
        }

        public void MarkLate(int meetingId, IList<int> employeeIds, string source)
        {
            const string insertFormat = "INSERT INTO LateEmployee (le_empId, le_mtgId, le_source) VALUES ({0}, {1}, '{2}');";
            var commandText = new StringBuilder();
            foreach (var employeeId in employeeIds)
            {
                commandText.AppendLine(string.Format(insertFormat, employeeId, meetingId, source));
            }

            ExecuteNonQuery(commandText.ToString());
        }

        public void ServedPenalty(int empId, DateTime servedOn, string how, string source)
        {
            var commandText = string.Format("INSERT INTO Penalty (pn_empId, pn_how, pn_source, pn_servedOn) VALUES ({0}, '{1}', '{2}', '{3}')",
                empId, how, source, servedOn);

            ExecuteNonQuery(commandText);
        }
    }
}
