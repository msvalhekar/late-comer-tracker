using System.Collections.Generic;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
{
    public class MeetingService
    {
        public IList<Meeting> GetAll()
        {
            return new MeetingDao().GetAll();
        }

        public Meeting Get(int id)
        {
            return new MeetingDao().Get(id);
        }

        //public Meeting Get(string name)
        //{
        //    return new MeetingDao().Get(name);
        //}

        public Meeting Add(Meeting meeting)
        {
            return new MeetingDao().Add(meeting);
        }

        public bool Delete(int id)
        {
            return new MeetingDao().Delete(id);
        }
    }
}
