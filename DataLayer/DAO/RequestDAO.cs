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
        
        public Request createRequest(string mailFrom, string mailContent, DateTime createdDate)
        {
            Request toReturn = new Request();
            toReturn.MailFrom = mailFrom;
            toReturn.MailContent = Encoding.UTF8.GetBytes(mailContent);
            toReturn.CreateDate = createdDate;
            entities.Requests1.Add(toReturn);
            entities.SaveChanges();
            return toReturn;
        }

        public Request getById(int id)
        {
            return entities.Requests1.Where(i => i.id == id).First();
        }

    }
}
