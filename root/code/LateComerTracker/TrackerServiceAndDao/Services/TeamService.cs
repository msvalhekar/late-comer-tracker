﻿using System.Collections.Generic;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
{
    public class TeamService
    {
        public IList<Team> GetAll()
        {
            return new TeamDao().GetAll();
        }

        public Team Get(int id)
        {
            return new TeamDao().Get(id);
        }

        public Team Get(string name)
        {
            return new TeamDao().Get(name);
        }

        public Team Add(Team team)
        {
            return new TeamDao().Add(team);
        }

        public bool Delete(int id)
        {
            return new TeamDao().Delete(id);
        }

        public void Edit(Team team)
        {
            new TeamDao().Update(team);
        }
    }
}
