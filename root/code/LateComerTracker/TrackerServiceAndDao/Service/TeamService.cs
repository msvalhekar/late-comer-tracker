using System.Collections.Generic;
using LateComerTracker.Backend.Dao;
using LateComerTracker.Backend.Model;

namespace LateComerTracker.Backend.Service
{
    public class TeamService
    {
        public IList<Team> GetAllTeams()
        {
            return new TeamDao().GetAllTeams();
        }

        public Team GetTeam(int id)
        {
            return new TeamDao().GetTeam(id);
        }
    }
}
