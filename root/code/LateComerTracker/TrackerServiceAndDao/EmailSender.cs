using System.Text;
using System.Net.Mail;
using System.Collections.Generic;

namespace LateComerTracker.Backend
{
    public class EmailSender
    {
        public void Send(IList<string> toList, IList<string> ccList, string subject, string body)
        {
            var from = new MailAddress("mvalhekar@spiderlogic.com", "Late Comer Tracker", Encoding.UTF8);
            Send(from, toList, ccList, subject, body);
        }

        private void Send(MailAddress from, IEnumerable<string> toList, IEnumerable<string> ccList, string subject, string body)
        {
            var message = new MailMessage
            {
                From = from,
                BodyEncoding = Encoding.UTF8,
                Body = body,
                IsBodyHtml = true,
                SubjectEncoding = Encoding.UTF8,
                Subject = subject
            };

            if(toList != null) message.To.Add(string.Join(",", toList));
            if (ccList != null) message.CC.Add(string.Join(",", ccList));

            try
            {
                var client = new SmtpClient
                {
                    Host = "exchcas.corp.wipfli.com"
                };
                client.Send(message);
            }
            catch
            {
            }
            message.Dispose();
        }
    }
}
