using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using LateComerTracker.Api.Filters;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    [ValidateModel]
    public class AttendanceController : ApiController
    {
        public IEnumerable<Attendance> Get(int id)
        {
            return new EmployeeService().GetAttendance(id);
        }

        public void Post()
        {
            var contentType = Request.Content.Headers.ContentType.MediaType;
            var requestParams = Request.Content.ReadAsStringAsync().Result;

            if (contentType != "application/json")
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var formData = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, object>>(requestParams);
            var teamId = Convert.ToInt32(formData["teamId"]);
            var meetingId = Convert.ToInt32(formData["meetingId"]);
            var employeeIds = ((Newtonsoft.Json.Linq.JArray) formData["employeeIds"]).ToObject<List<int>>();
            var source = formData["source"].ToString();

            new TeamService().MarkLate(teamId, meetingId, employeeIds, source);
        }
    }
}
