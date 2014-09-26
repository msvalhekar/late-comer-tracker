using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void MarkLate(int teamId, int meetingId, int employeeId, string reason, string source)
        {
            new TeamDao().MarkLate(teamId, meetingId, employeeId, reason, source);
        }

        public void LogPenalty(Penalty penalty)
        {
            new TeamDao().LogPenalty(penalty);
        }

        public void NotifyServedPenalty(Penalty penalty)
        {
            var team = Get(penalty.TeamId);
            new EmailSender().Send(
                team.Employees.Select(x => x.EmailId).ToList(),
                null,
                "Penalty Served",
                GetBodyForPenaltyServed(team.Employees, penalty));
        }

        public void NotifyLateComers(int teamId, int meetingId, List<KeyValuePair<int,string>> lateEmpReasonList)
        {
            var team = Get(teamId);
            var meeting = new MeetingService().Get(meetingId);
            var lateEmps = team.Employees.Where(x => lateEmpReasonList.Any(y => y.Key == x.Id)).ToList();
            var onTimeEmps = team.Employees.Except(lateEmps);
            new EmailSender().Send(
                lateEmps.Select(x => x.EmailId).ToList(),
                onTimeEmps.Select(x => x.EmailId).ToList(),
                string.Format("{0} ({1} pnts)", meeting.Name, meeting.Severity),
                GetBodyForLateComers(team.Employees, lateEmpReasonList));
        }

        private string GetBodyForLateComers(IList<Employee> employees, List<KeyValuePair<int, string>> lateEmpReasonList)
        {
            var lateComers = GetBodyLateComers(employees, lateEmpReasonList);
            var summary = GetBodySummary(employees);
            var body = string.Format("<html><body>{0}</body></html>", lateComers + summary);
            return body;
        }

        private string GetBodyLateComers(IList<Employee> employees, List<KeyValuePair<int, string>> lateEmpReasonList)
        {
            if (lateEmpReasonList == null) return string.Empty;

            var sbBody = new StringBuilder();
            sbBody.Append("<table style=\"border: 1px solid #ddd\">");
            sbBody.Append("<thead style=\"background-color: #ffc0cb;\"><tr>");
            sbBody.Append("<th style=\"border: 1px solid #ddd\">Name</th>");
            sbBody.Append("<th style=\"border: 1px solid #ddd\">Reason</th>");
            sbBody.Append("</tr></thead>");
            foreach (var employee in lateEmpReasonList)
            {
                sbBody.Append("<tr style=\"border: 1px solid #ddd\">");
                sbBody.Append("<td style=\"border: 1px solid #ddd\">" + employees.First(x => x.Id == employee.Key).Name + "</td>");
                sbBody.Append("<td style=\"border: 1px solid #ddd\">" + employee.Value + "</td>");
                sbBody.Append("</tr>");
            }
            sbBody.Append("</table><br/>");
            return sbBody.ToString();
        }

        private string GetBodySummary(IList<Employee> employees)
        {
            var sbBody = new StringBuilder("<h3>Team Summary</h3>");
            sbBody.Append("<table style=\"border: 1px solid #ddd\">");
            sbBody.Append("<thead style=\"background-color: #cef6d8;\"><tr>");
            sbBody.Append("<th style=\"border: 1px solid #ddd;\">Name</th>");
            sbBody.Append("<th style=\"border: 1px solid #ddd\">Unsettled Penalties</th>");
            sbBody.Append("<th style=\"border: 1px solid #ddd\">Served Penalties</th>");
            sbBody.Append("</tr></thead>");
            foreach (var employee in employees.OrderByDescending(x => x.UnsettledPoints))
            {
                sbBody.Append("<tr style=\"border: 1px solid #ddd\">");
                sbBody.Append("<td style=\"border: 1px solid #ddd\">" + employee.Name + "</td>");
                sbBody.Append("<td style=\"border: 1px solid #ddd\">" + employee.FormattedDuePenalties + "</td>");
                sbBody.Append("<td style=\"border: 1px solid #ddd\">" + employee.PenaltyList.Count + "</td>");
                sbBody.Append("</tr>");
            }
            sbBody.Append("</table>");
            return sbBody.ToString();
        }

        private string GetBodyForPenaltyServed(IList<Employee> employees, Penalty penalty)
        {
            var penaltyServed = GetBodyPenaltyServed(penalty);
            var summary = GetBodySummary(employees);
            var body = string.Format("<html><body>{0}</body></html>", penaltyServed + summary);
            return body;
        }

        private string GetBodyPenaltyServed(Penalty penalty)
        {
            var employee = new EmployeeService().Get(penalty.EmpId);

            var sbBody = new StringBuilder();
            sbBody.Append("<table style=\"border: 1px solid #ddd\">");
            sbBody.Append("<thead style=\"background-color: #ffc0cb;\"><tr>");
            sbBody.Append("<th style=\"border: 1px solid #ddd\">Name</th>");
            sbBody.Append("<th style=\"border: 1px solid #ddd\">How</th>");
            sbBody.Append("<th style=\"border: 1px solid #ddd\">When</th>");
            sbBody.Append("</tr></thead>");
            
            sbBody.Append("<tr style=\"border: 1px solid #ddd\">");
            sbBody.Append("<td style=\"border: 1px solid #ddd\">" + employee.Name + "</td>");
            sbBody.Append("<td style=\"border: 1px solid #ddd\">" + penalty.How + "</td>");
            sbBody.Append("<td style=\"border: 1px solid #ddd\">" + penalty.WhenString + "</td>");
            sbBody.Append("</tr>");

            sbBody.Append("</table><br/>");
            return sbBody.ToString();
        }
    }
}
