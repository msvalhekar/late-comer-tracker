using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LateComerTracker.Backend.Model;

namespace LateComerTracker.Backend.Dao
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
            var commandText = "select t.team_id, t.team_name, t.team_description, e.emp_id, e.emp_name, e.emp_emailId from Team t"
                + " left join TeamEmployee te on te.team_id = t.team_id"
                + " left join Employee e on e.emp_id = te.emp_id"
                + " where t.team_id = " + id;
            var table = GetDataTable(commandText);

            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            var team = new Team
            {
                Id = Convert.ToInt32(row["team_id"]),
                Name = row["team_name"].ToString(),
                Description = row["team_description"].ToString(),
                Employees = new List<Employee>()
            };

            // join and fetch associated Empolyees
            foreach (DataRow dataRow in table.Rows)
            {
                if (dataRow["emp_id"] == null) break;

                team.Employees.Add(new Employee
                {
                    Id = Convert.ToInt32(dataRow["emp_id"]),
                    Name = dataRow["emp_name"].ToString(),
                    EmailId = dataRow["emp_emailId"].ToString()
                });
            }
            return team;
        }
    }
}
