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
        public IList<Meeting> GetAll()
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

        public Meeting Get(int id)
        {
            return GetMeetingWhere(new KeyValuePair<string, string>("mtg_id", id.ToString(CultureInfo.InvariantCulture)));
        }

        public Meeting Get(string name)
        {
            return GetMeetingWhere(new KeyValuePair<string, string>("mtg_name", "'" + name + "'"));
        }

        private Meeting GetMeetingWhere(KeyValuePair<string, string> wherePair)
        {
            var commandText = "select mtg_id, mtg_name, mtg_description, mtg_severity from Meeting"
                              + " where " + wherePair.Key + " = " + wherePair.Value;
            
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

        public Meeting Add(Meeting meeting)
        {
            if (meeting == null) return null;

            var commandText = string.Format("INSERT INTO Meeting (mtg_name, mtg_description, mtg_severity) VALUES ('{0}', '{1}', {2})",
                meeting.Name, meeting.Description, meeting.Severity);

            if (-1 < ExecuteNonQuery(commandText))
            {
                return Get(meeting.Name);
            }
            return meeting;
        }

        public bool Delete(int id)
        {
            var commandText = string.Format("DELETE Meeting WHERE mtg_id = {0}"
                //+ "DELETE Team WHERE team_id = {0}"
                , id);

            return -1 < ExecuteNonQuery(commandText);
        }
    }
}
