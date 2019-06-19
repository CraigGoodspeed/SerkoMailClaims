using BusinessLayer.Message;
using BusinessLayer.Validators;
using DataLayer;
using DataLayer.DAO;
using DataLayer.DTO;
using MailInteraction;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ProcessInbox(string restEndPoint)
        {
            XmlDocument xdoc = getMailConfiguration();
            XmlSerializer serializer = new XmlSerializer(typeof(MailInteraction.ServerProperties));
            smtp = Deserialiser.loadMailConfiguration(xdoc.SelectSingleNode("//mailConfig/smtpProperties/ServerProperties"), serializer);
            pop3 = Deserialiser.loadMailConfiguration(xdoc.SelectSingleNode("//mailConfig/pop3Properties/ServerProperties"), serializer);
            XmlNode credentials = xdoc.SelectSingleNode("//mailConfig/credentials");
            username = credentials.Attributes["username"].InnerText;
            password = credentials.Attributes["password"].InnerText;
            mailHelper = new MailHelper(pop3, smtp, username, password);
            this.restEndPoint = restEndPoint;

        }
        private XmlDocument getMailConfiguration()
        {
            XmlDocument xdoc = new XmlDocument();
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("BusinessLayer.mailConfig.xml"))
            {
                xdoc.Load(stream);
            }
            return xdoc;
        }
        public void ProcessMailForExpenses(string subjectName)
        {
            List<MailMessage> claims = mailHelper.getStringMessages(subjectName);
            foreach (MailMessage claim in claims)
            {

                //store the request....
                RequestDAO request = new RequestDAO();
                if (request.shouldProcess(claim.id))
                {
                    
                    Request req = request.createRequestOrGetRequest(claim.from, claim.body, DateTime.Now, claim.id);
                    //call the rest service.
                    XmlDocument xdoc = RestHelper.callRestService(restEndPoint, claim.body);
                    ExpenseDTO expenseDTO = Deserialiser.deserialiseContent<ExpenseDTO>(xdoc);
                    ExpenseDAO expenseDAO = new ExpenseDAO();
                    Expense expenseToValidate = expenseDAO.CreateExpense(expenseDTO, req);

                    ExpenseValidator validator = new ExpenseValidator(expenseToValidate);

                    mailHelper.sendMessage(claim.from, validator.getMessage(), claim.id, string.Format("response to claim dated {0}", claim.date));
                    //deserialise the content
                    //validate the request
                    //store - valid request or failure and respond to user
                }

            }
        }

        public void Dispose()
        {
            mailHelper.Dispose();
        }
    }
}
