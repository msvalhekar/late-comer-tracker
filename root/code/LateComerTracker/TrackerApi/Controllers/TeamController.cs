using System.Collections.Generic;
using System.Web.Http;
using LateComerTracker.Backend.Model;
using LateComerTracker.Backend.Service;

namespace LateComerTracker.Api.Controllers
{
    public class TeamController : ApiController
    {
        // GET api/user
        public IEnumerable<Team> Get()
        {
            return new TeamService().GetAllTeams();
        }

        // GET api/user/5
        public Team Get(int id)
        {
            return new TeamService().GetTeam(id);
        }

        // POST api/user
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
