using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MailInteraction
{
    public class MailMessage
    {
        public string body { get; internal set; }
        public string id { get; internal set; }
        public string from { get; internal set; }
        public string date { get; internal set; }
        public MailMessage(string content, string id, string from, string date) {
            this.body = content;
            this.id = id;
            this.from = from;
            this.date = date;
        }
    }
    public class MailHelper : IDisposable
    {

        private string username;
        private string password;
        private ServerProperties popServer;
        private ServerProperties smtpServer;
        Pop3Client mailClient;
        

        public MailHelper(ServerProperties popServer, ServerProperties smtpServer, string username, string password)
        {
            mailClient = new Pop3Client();
            this.username = username;
            this.popServer = popServer;
            this.smtpServer = smtpServer;
            this.password = password;
            mailClient.Connect(popServer.url, popServer.port, popServer.useSSL);
            mailClient.Authenticate(username, password);
        }

        private List<MailMessage> getMessages(string subject)
        {
            int messageCount = mailClient.GetMessageCount();
            List<MailMessage> toReturn = new List<MailMessage>();
            for (int i = 1; i <= messageCount; i++)
            {
                Message mailMessage = mailClient.GetMessage(i);

                toReturn.Add(new MailMessage(mailMessage.FindFirstPlainTextVersion().GetBodyAsText(), mailMessage.Headers.MessageId, mailMessage.Headers.From.Address, mailMessage.Headers.Date));
            }
            return toReturn;
        }

        public List<MailMessage> getStringMessages(string subject)
        {
            return getMessages(subject);
        }

        public void sendMessage(string toAddress, string body, string subject)
        {
            sendMessage(toAddress, body, string.Empty, subject);
        }

        public void sendMessage(string toAddress, string body, string messageId, string subject)
        {
            SmtpClient client = new SmtpClient();
            client.Host = smtpServer.url;
            client.Port = smtpServer.port;
            client.EnableSsl = smtpServer.useSSL;
            client.Credentials = new NetworkCredential(username, password);

            using (System.Net.Mail.MailMessage sendMe = new System.Net.Mail.MailMessage(new MailAddress(username), new MailAddress(toAddress)))
            {
                sendMe.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(messageId))
                {
                    sendMe.Headers.Add("In-Reply-To", messageId);
                    sendMe.Headers.Add("References", messageId);
                }
                sendMe.Subject = subject;
                sendMe.Body = body;
                client.Send(sendMe);
            }
        }

        public void Dispose()
        {
                mailClient.Disconnect();
        }
    }
}
