using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LateComerTracker.Api.Filters;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    [ValidateModel]
    public class AttendanceController : ApiController
    {
        public void Post(Attendance attendance)
        {
            new TeamService().MarkLate(attendance.TeamId, attendance.MeetingId, attendance.EmployeeIds.ToList(), attendance.Source);
        }
    }
}
