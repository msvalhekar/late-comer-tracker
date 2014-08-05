using System.Collections.Generic;
using System.Web.Http;
using LateComerTracker.Api.Filters;
using LateComerTracker.Backend.Models;
using LateComerTracker.Backend.Services;

namespace LateComerTracker.Web.ApiControllers
{
    [ValidateModel]
    public class MeetingsController : ApiController
    {
        public IEnumerable<Meeting> Get()
        {
            return new MeetingService().GetAllMeetings();
        }

        public Meeting Get(int id)
        {
            return new MeetingService().GetMeeting(id);
        }

        //public Team GetByName(string name)
        //{
        //    return new MeetingService().GetMeeting(name);
        //}

        public Meeting Post(Meeting meeting)
        {
            meeting = new MeetingService().AddMeeting(meeting);
            if (meeting.Id == 0) return null;
            return meeting;
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
