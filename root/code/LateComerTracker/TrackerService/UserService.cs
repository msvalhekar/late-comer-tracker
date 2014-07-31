using System.Collections.Generic;
using LateComerTracker.Dao;
using TrackerModels;

namespace LateCommerTracker.Service
{
    public class UserService
    {
        public IList<User> GetUsers()
        {
            return new UserDao().GetUsers();
        }
    }
}
