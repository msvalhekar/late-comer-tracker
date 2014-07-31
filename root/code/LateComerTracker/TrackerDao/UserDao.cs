using System.Collections.Generic;
using TrackerModels;

namespace LateComerTracker.Dao
{
    public class UserDao
    {
        public IList<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Meenalkumar",
                    TotalPoints = 0,
                    UnsettledPoints = 0,
                    SettledPenalties = 0
                },
                new User
                {
                    Id = 1,
                    Name = "Mohan",
                    TotalPoints = 15,
                    UnsettledPoints = 4,
                    SettledPenalties = 2
                }
            };
        }
    }
}
