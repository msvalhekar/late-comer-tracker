using System.Collections.Generic;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
{
    public class MeetingService
    {
        public IList<Meeting> GetAllMeetings()
        {
            return new MeetingDao().GetAllMeetings();
        }

        public Meeting GetMeeting(int id)
        {
            return new MeetingDao().GetMeeting(id);
        }

        //public Meeting GetMeeting(string name)
        //{
        //    return new MeetingDao().GetMeeting(name);
        //}

        public Meeting AddMeeting(Meeting meeting)
        {
            return new MeetingDao().AddMeeting(meeting);
        }
    }
}
