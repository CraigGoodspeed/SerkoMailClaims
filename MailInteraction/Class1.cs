using MailInteraction.Claim;
using Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailInteraction
{
    public class MailHelper : IDisposable
    {
        private string username;
        private string password;
        private ServerProperties popServer;
        private ServerProperties smtpServer;
        private string messageSubject;
        Pop3Client mailClient;

        public MailHelper(ServerProperties popServer, ServerProperties smtpServer, string username, string password)
        {
            mailClient = new Pop3Client();
            this.username = username;
            this.popServer = popServer;
            this.smtpServer = smtpServer;
            this.password = password;
            mailClient.Connect(popServer.url, username, password, popServer.port, popServer.useSSL);
        }

        private List<Pop3Message> getMessages(string subject)
        {
            IEnumerable<Pop3Message> messages = mailClient.ListAndRetrieve();
            return messages.Where(i => i.Subject.ToUpper().IndexOf(subject.ToUpper()) > -1).ToList();
        }

        public List<ClaimMail> getStringMessages(string subject)
        {
            return getMessages(subject).Where(i => i.Attachments.Count() == 0).Select(msg => new ClaimMail() { claimant = msg.From, body = msg.Body }).ToList();
        }
        /// <summary>
        /// there are possibly many attachments
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public List<ClaimAttachment> getAttachmentMessages(string subject)
        {
            List<ClaimDetails> toReturn = new List<ClaimDetails>();
            List<Pop3Message> withAttachments = getMessages(subject).Where(i => i.Attachments.Count() > 0).ToList();
            foreach(Pop3Message message in withAttachments){
                toReturn.AddRange(message.Attachments.Where(msg => msg.Name.ToLower().IndexOf("claim") == 0).Select(att => new ClaimDetails() { claimant = username, attachmentBody=att.GetData() }).ToList());
            }
            return toReturn;
        }

        public void sendMessage(string toAddress, string body, string subject)
        {
            SmtpClient client = new SmtpClient();
            client.Host = smtpServer.url;
            client.Port = smtpServer.port;
            client.EnableSsl = smtpServer.useSSL;
            client.Credentials = new NetworkCredential(username, password);
            using(MailMessage sendMe = new MailMessage(new MailAddress(username), new MailAddress(toAddress))){
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
