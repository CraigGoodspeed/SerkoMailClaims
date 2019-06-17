using MailInteraction;
using MailInteraction.Claim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MailboxMonitor
{
    public partial class Monitor : ServiceBase
    {
        ServerProperties pop3Properties;
        ServerProperties smtpProperties;
        string username;
        string password;
        MailHelper mailHelper;

        public Monitor()
        {
            InitializeComponent();
        }

        private static ServerProperties loadMailConfiguration(XmlNode node, XmlSerializer serializer)
        {
            return (ServerProperties)serializer.Deserialize(new XmlNodeReader(node));
        }

        protected override void OnStart(string[] args)
        {
            string smtpConfig = System.Configuration.ConfigurationManager.AppSettings["mailSetupPath"];
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(smtpConfig);
            XmlSerializer serializer = new XmlSerializer(typeof(MailInteraction.ServerProperties));
            smtpProperties = loadMailConfiguration(xdoc.SelectSingleNode("//mailConfig/smtpProperties"),serializer);
            pop3Properties = loadMailConfiguration(xdoc.SelectSingleNode("//mailConfig/pop3Properties"), serializer);
            XmlNode credentials = xdoc.SelectSingleNode("//mailConfig/credentials");
            username = credentials.Attributes["username"].InnerText;
            password = credentials.Attributes["password"].InnerText;
            timer.Interval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["mailCheckIntervalSeconds"]) * 1000;
            mailHelper = new MailHelper(pop3Properties, smtpProperties, username, password);
        }

        protected override void OnStop()
        {
            mailHelper.Dispose();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            List<ClaimMail> claims = mailHelper.getStringMessages("claim");
            foreach (ClaimMail claim in claims)
            {
                //call the rest service.
            }
        }
    }
}
