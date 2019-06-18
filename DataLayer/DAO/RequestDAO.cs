using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAO
{
    
    public class RequestDAO : BaseDAO
    {
        public RequestDAO() : base(){
        }
        
        public Request createRequestOrGetRequest(string mailFrom, string mailContent, DateTime createdDate, string messageID)
        {

            Request toReturn = (from i in entities.Requests1 where i.MessageID == messageID select i).FirstOrDefault();
            if (toReturn != null)
                return toReturn;
            toReturn = new Request();
            toReturn.MailFrom = mailFrom;
            toReturn.MailContent = Encoding.UTF8.GetBytes(mailContent);
            toReturn.CreateDate = createdDate;
            toReturn.MessageID = messageID;
            entities.Requests1.Add(toReturn);
            entities.SaveChanges();
            return toReturn;
        }

        public Request getById(int id)
        {
            return entities.Requests1.Where(i => i.id == id).First();
        }

        /// <summary>
        /// An email has a identifier on the mail, this identifier is stored. and will allow us to only process new emails.
        /// </summary>
        /// <param name="messageID">the message identifier received from pop3 message</param>
        /// <returns>true, processme: false do not</returns>
        public bool shouldProcess(string messageID)
        {
            Request message = (from i in entities.Requests1 where i.MessageID == messageID select i).FirstOrDefault();
            if (message != null )
            {
                Expense expense = (from e in entities.Expenses1 where e.RequestID == message.id select e).FirstOrDefault();
                return expense == null;
            }
            return true;
        }
    }
}
