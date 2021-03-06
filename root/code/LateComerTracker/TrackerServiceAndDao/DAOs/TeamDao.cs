﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.DAOs
{
    public class TeamDao : BaseDao
    {
        public IList<Team> GetAll()
        {
            var table = GetDataTable("select team_id, team_name, team_description from Team");

            return (from DataRow row in table.Rows
                    select new Team
                    {
                        Id = Convert.ToInt32(row["team_id"]),
                        Name = row["team_name"].ToString(),
                        Description = row["team_description"].ToString()
                    }).ToList();
        }

        public Team Get(int id)
        {
            return GetTeamWhere(new KeyValuePair<string, string>("team_id", id.ToString(CultureInfo.InvariantCulture)));
        }

        public Team Get(string name)
        {
            return GetTeamWhere(new KeyValuePair<string, string>("team_name", "'" + name + "'"));
        }

        private Team GetTeamWhere(KeyValuePair<string, string> wherePair)
        {
            var commandText = "select t.team_id, t.team_name, t.team_description from Team t"
                              + " where t." + wherePair.Key + " = " + wherePair.Value;

            var dataTable = GetDataTable(commandText);
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            var team = new Team
            {
                Id = Convert.ToInt32(row["team_id"]),
                Name = row["team_name"].ToString(),
                Description = row["team_description"].ToString(),
                Employees = new List<Employee>()
            };

            // join and fetch associated Empolyees
            var empIdsCommandText = "select te.emp_id from TeamEmployee te"
                              + " where te.team_id = " + team.Id;

            var empIdsDataTable = GetDataTable(empIdsCommandText);
            if (0 < empIdsDataTable.Rows.Count)
            {
                var employeeDao = new EmployeeDao();
                foreach (DataRow dataRow in empIdsDataTable.Rows)
                {
                    team.Employees.Add(employeeDao.Get(Convert.ToInt32(dataRow["emp_id"]), team.Id));
                }
            }

            return team;
        }

        public Team Add(Team team)
        {
            if (team == null) return null;

            var commandText = string.Format("INSERT INTO Team (team_name, team_description) OUTPUT inserted.team_id VALUES ('{0}', '{1}')",
                team.Name, team.Description);

            team.Id = ExecuteScalar(commandText);

            if (team.Employees == null) return team;

            // add new employees to this team
            const string insertFormat = "INSERT INTO TeamEmployee (team_id, emp_id) VALUES ({0}, {1});";
            var insertCommandText = new StringBuilder();
            foreach (var employee in team.Employees)
            {
                insertCommandText.AppendLine(string.Format(insertFormat, team.Id, employee.Id));
            }
            ExecuteNonQuery(insertCommandText.ToString());

            return team;
        }

        public bool Delete(int id)
        {
            var commandText = string.Format("DELETE TeamEmployee WHERE team_id = {0};"
                + "DELETE Team WHERE team_id = {0}", id);

            return -1 < ExecuteNonQuery(commandText);
        }

        public void Update(Team team)
        {
            var commandText = string.Format("UPDATE Team SET team_name='{0}', team_description='{1}' WHERE team_id = {2}", 
                team.Name, team.Description, team.Id);
            ExecuteNonQuery(commandText);

            // remove all team-employees associations for team
            ExecuteNonQuery("DELETE TeamEmployee WHERE team_id = " + team.Id);

            // add new employees to this team
            const string insertFormat = "INSERT INTO TeamEmployee (team_id, emp_id) VALUES ({0}, {1});";
            var insertCommandText = new StringBuilder();
            foreach (var employee in team.Employees)
            {
                insertCommandText.AppendLine(string.Format(insertFormat, team.Id, employee.Id));
            }
            ExecuteNonQuery(insertCommandText.ToString());
        }

        public void MarkLate(int teamId, int meetingId, int employeeId, string reason, string source)
        {
            var commandText = string.Format("INSERT INTO LateEmployee (le_teamId, le_empId, le_mtgId, le_reason, le_source) VALUES ({0}, {1}, {2}, '{3}', '{4}');",
                teamId, employeeId, meetingId, reason, source);

            ExecuteNonQuery(commandText);
        }

        public void LogPenalty(Penalty penalty)
        {
            var commandText = string.Format("INSERT INTO Penalty (pn_teamId, pn_empId, pn_how, pn_source, pn_servedOn) VALUES ({0}, {1}, '{2}', '{3}', '{4}')",
                penalty.TeamId, penalty.EmpId, penalty.How, penalty.Source, penalty.When.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            ExecuteNonQuery(commandText);
        }

    }
}
