using System.Collections.Generic;
using System.Web.Http;
using LateComerTracker.Api.Filters;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    [ValidateModel]
    public class TeamsController : ApiController
    {
        public IEnumerable<Team> Get()
        {
            return new TeamService().GetAll();
        }

        public Team Get(int id)
        {
            return new TeamService().Get(id);
        }

        public Team GetByName(string name)
        {
            return new TeamService().Get(name);
        }

        public Team Post(Team team)
        {
            team = new TeamService().Add(team);
            if (team.Id == 0) return null;
            return team;
        }

        public void Put(int id, Team team)
        {
            new TeamService().Edit(team);
        }

        public bool Delete(int id)
        {
            return new TeamService().Delete(id);
        }
    }
}
