using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.DAOs
{
    public class MeetingDao : BaseDao
    {
        public IList<Meeting> GetAllMeetings()
        {
            var table = GetDataTable("select mtg_id, mtg_name, mtg_description, mtg_severity from Meeting");

            return (from DataRow row in table.Rows
                select new Meeting
                {
                    Id = Convert.ToInt32(row["mtg_id"]),
                    Name = row["mtg_name"].ToString(),
                    Description = row["mtg_description"].ToString(),
                    Severity = Convert.ToInt32(row["mtg_severity"])
                }).ToList();
        }

        public Meeting GetMeeting(int id)
        {
            var commandText = "select mtg_id, mtg_name, mtg_description, mtg_severity from Meeting"
                              + " where mtg_id = " + id;

            var dataTable = GetDataTable(commandText);
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            return new Meeting
            {
                Id = Convert.ToInt32(row["mtg_id"]),
                Name = row["mtg_name"].ToString(),
                Description = row["mtg_description"].ToString(),
                Severity = Convert.ToInt32(row["mtg_severity"])
            };
        }

        public Meeting AddMeeting(Meeting meeting)
        {
            throw new NotImplementedException();
        }
    }
}
