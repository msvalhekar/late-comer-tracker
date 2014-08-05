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
            return new MeetingService().GetAll();
        }

        public Meeting Get(int id)
        {
            return new MeetingService().Get(id);
        }

        //public Team GetByName(string name)
        //{
        //    return new MeetingService().Get(name);
        //}

        public Meeting Post(Meeting meeting)
        {
            meeting = new MeetingService().Add(meeting);
            if (meeting.Id == 0) return null;
            return meeting;
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public bool Delete(int id)
        {
            return new MeetingService().Delete(id);
        }
    }
}
