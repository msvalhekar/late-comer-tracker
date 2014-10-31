using System.Collections.Generic;
using System.Text;
using LateComerTracker.Backend.DAOs;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.Services
{
    public class ConfigurationService
    {
        public IEnumerable<Configuration> GetAll()
        {
            return new ConfigurationDao().GetAll();
        }

        //public Configuration Add(Configuration configuration)
        //{
        //    return new ConfigurationDao().Add(configuration);
        //}

        //public bool Delete(Configuration configuration)
        //{
        //    return new ConfigurationDao().Delete(configuration);
        //}

        public void Update(Configuration configuration)
        {
            new ConfigurationDao().Update(configuration);
            Configurations.Reset();
        }

        public IList<RuleSet> Rules()
        {
            var toDo = new RuleSet
            {
                Type = RuleSetType.Safe,
                Title = "No late-mark",
                Rules = new List<string>
                {
                    "If not attending the meeting due to <b>vacation</b>",
                    "If late or not attending the meeting and have informed at least <b>a day prior</b> (via skype chat or email only)"
                }
            };

            var toNotDo = new RuleSet
            {
                Type = RuleSetType.NotSafe,
                Title = "Get late-mark",
                Rules = new List<string>
                {
                    "If late or not attending the meeting and have <b>not informed</b>",
                    "If late or not attending the meeting and have informed on the <b>same day</b>",
                }
            };

            var notes = new RuleSet
            {
                Type = RuleSetType.Notes,
                Title = "Note:",
                Rules = new List<string>
                {
                    "Late = missed at least one update from team member (applicable only for standup meetings)",
                    "Points are given as per configured meeting severity:" + GetMeetingsDetails()
                }
            };

            return new List<RuleSet> {toDo, toNotDo, notes};
        }

        private string GetMeetingsDetails()
        {
            var text = new StringBuilder();
            var meetings = new MeetingDao().GetAll();
            foreach (var meeting in meetings)
            {
                text.AppendFormat("<br/>{0} : {1}", meeting.Name, meeting.Severity);
            }
            return text.ToString();
        }
    }
}