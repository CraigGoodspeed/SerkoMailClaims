using DataLayer;
using DataLayer.DAO;
using DataLayer.DTO;
using MailInteraction;
using Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessLayer
{
    public class ProcessInbox : IDisposable
    {
        MailHelper mailHelper;
        ServerProperties pop3;
        ServerProperties smtp;
        string restEndPoint;
        string username;
        string password;

        public ProcessInbox(XmlDocument xdoc, string restEndPoint)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MailInteraction.ServerProperties));
            smtp = Deserialiser.loadMailConfiguration(xdoc.SelectSingleNode("//mailConfig/smtpProperties/ServerProperties"), serializer);
            pop3 = Deserialiser.loadMailConfiguration(xdoc.SelectSingleNode("//mailConfig/pop3Properties/ServerProperties"), serializer);
            XmlNode credentials = xdoc.SelectSingleNode("//mailConfig/credentials");
            username = credentials.Attributes["username"].InnerText;
            password = credentials.Attributes["password"].InnerText;
            mailHelper = new MailHelper(pop3, smtp, username, password);
            this.restEndPoint = restEndPoint;

        }
        public void ProcessMailForExpenses(string subjectName)
        {
            List<Pop3Message> claims = mailHelper.getStringMessages(subjectName);
            foreach (Pop3Message claim in claims)
            {

                //store the request....
                RequestDAO request = new RequestDAO();
                Request req = request.createRequest(claim.From, claim.RawMessage, DateTime.Now);
                //call the rest service.
                XmlDocument xdoc = RestHelper.callRestService(restEndPoint, claim.Body);
                ExpenseDTO expenseDTO = Deserialiser.deserialiseContent<ExpenseDTO>(xdoc);
                ExpenseDAO expenseDAO = new ExpenseDAO();

                expenseDAO.CreateExpense(expenseDTO, req);

                //deserialise the content
                //validate the request
                //store - valid request or failure and respond to user

            }
        }

        public void Dispose()
        {
            mailHelper.Dispose();
        }
    }
}
