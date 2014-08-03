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
            var commandText = "select t.team_id, t.team_name, t.team_description, e.emp_id, e.emp_name, e.emp_emailId from Team t"
                              + " left join TeamEmployee te on te.team_id = t.team_id"
                              + " left join Employee e on e.emp_id = te.emp_id"
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
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (Convert.IsDBNull(dataRow["emp_id"])) break;

                team.Employees.Add(new Employee
                {
                    Id = Convert.ToInt32(dataRow["emp_id"]),
                    Name = dataRow["emp_name"].ToString(),
                    EmailId = dataRow["emp_emailId"].ToString()
                });
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
    }
}
