using System.Collections.Generic;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
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

        public Team GetTeam(string name)
        {
            return new TeamDao().GetTeam(name);
        }

        public Team AddTeam(Team team)
        {
            return new TeamDao().AddTeam(team);
        }

        public bool Delete(int id)
        {
            return new TeamDao().Delete(id);
        }
    }
}
