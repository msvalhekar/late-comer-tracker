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

        public Employee Get(int id, int teamId = 0)
        {
            return GetWhere(new KeyValuePair<string, string>("emp_id", id.ToString(CultureInfo.InvariantCulture)), teamId);
        }

        public Employee Get(string name)
        {
            return GetWhere(new KeyValuePair<string, string>("emp_name", "'" + name + "'"));
        }

        private Employee GetWhere(KeyValuePair<string, string> wherePair, int teamId = 0)
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

            if (teamId != 0)
            {
                var fineCommandText = string.Format("select unsettled_points from EmployeeFine where team_id = {0} and emp_id = {1}", teamId, employee.Id);
                employee.UnsettledPoints = ExecuteScalar(fineCommandText);
            }
            employee.PenaltyList = GetPenalties(employee.Id);
            employee.AttendanceList = GetAttendance(employee.Id);
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

        public IList<Attendance> GetAttendance(int empId)
        {
            var commandText = string.Format(
                    "SELECT t.team_name, e.emp_name, m.mtg_name, le.le_lateOn, le.le_reason FROM LateEmployee le"
                    + " JOIN Team t ON t.team_id = le.le_teamId"
                    + " JOIN Employee e ON e.emp_id = le.le_empId"
                    + " JOIN Meeting m ON m.mtg_id = le.le_mtgId"
                    + " WHERE le.le_empid = {0}"
                    + " ORDER BY le.le_lateOn DESC", empId);
            var table = GetDataTable(commandText);

            return (from DataRow row in table.Rows
                    select new Attendance
                    {
                        TeamName = row["team_name"].ToString(),
                        EmployeeName = row["emp_name"].ToString(),
                        MeetingName = row["mtg_name"].ToString(),
                        LateDateTime = Convert.ToDateTime(row["le_lateOn"]),
                        Reason = row["le_reason"].ToString(),
                    }).ToList();
        }

        public IList<Penalty> GetPenalties(int empId)
        {
            var commandText = string.Format(
                    "SELECT t.team_name, e.emp_name, p.pn_how, p.pn_servedOn, p.pn_source FROM Penalty p"
                    + " JOIN Team t ON t.team_id = p.pn_teamId"
                    + " JOIN Employee e ON e.emp_id = p.pn_empId"
                    + " WHERE p.pn_empid = {0}"
                    + " ORDER BY p.pn_servedOn DESC", empId);
            var table = GetDataTable(commandText);

            return (from DataRow row in table.Rows
                    select new Penalty
                    {
                        TeamName = row["team_name"].ToString(),
                        EmployeeName = row["emp_name"].ToString(),
                        How = row["pn_how"].ToString(),
                        When = Convert.ToDateTime(row["pn_servedOn"]),
                        Source = row["pn_source"].ToString()
                    }).ToList();
        }
    }
}
