using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.DAOs
{
    public class TeamDao : BaseDao
    {
        public IList<Team> GetAllTeams()
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

        public Team GetTeam(int id)
        {
            return GetTeamWhere(new KeyValuePair<string, string>("team_id", id.ToString(CultureInfo.InvariantCulture)));
        }

        public Team GetTeam(string name)
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
                    team.Employees.Add(employeeDao.GetEmployee(Convert.ToInt32(dataRow["emp_id"])));
                }
            }
            
            return team;
        }

        public Team AddTeam(Team team)
        {
            if (team == null) return null;

            var commandText = string.Format("INSERT INTO Team (team_name, team_description) VALUES ('{0}', '{1}')",
                team.Name, team.Description);

            if (-1 < ExecuteNonQuery(commandText))
            {
                return GetTeam(team.Name);
            }
            return team;
        }

        public bool Delete(int id)
        {
            var commandText = string.Format("DELETE team where team_id="+id);

            return -1 < ExecuteNonQuery(commandText);
        }
    }
}
