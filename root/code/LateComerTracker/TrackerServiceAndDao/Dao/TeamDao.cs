using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LateComerTracker.Backend.Model;

namespace LateComerTracker.Backend.Dao
{
    public class TeamDao : BaseDao
    {
        public TeamDao(string connString) : base(connString) { }

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
    }
}
