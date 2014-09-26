using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LateComerTracker.Api.Filters;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    [ValidateModel]
    public class PenaltyController : ApiController
    {
        public IEnumerable<Penalty> Get(int id)
        {
            return new EmployeeService().GetPenalties(id);
        }

        public void Post(Penalty penalty)
        {
            var teamService = new TeamService();
            teamService.LogPenalty(penalty);

            teamService.NotifyServedPenalty(penalty);
        }
    }
}
