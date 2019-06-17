using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MailboxMonitor
{
    public partial class Monitor : ServiceBase
    {
        string httpRestEndPoint;
        ProcessInbox inbox;
        public Monitor()
        {
            InitializeComponent();
        }

        

        protected override void OnStart(string[] args)
        {
            string smtpConfig = System.Configuration.ConfigurationManager.AppSettings["mailSetupPath"];
            
            timer.Interval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["mailCheckIntervalSeconds"]) * 1000;
            httpRestEndPoint = System.Configuration.ConfigurationManager.AppSettings["restEndPoint"];
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(smtpConfig);
            inbox = new ProcessInbox(xdoc, httpRestEndPoint);
        }

        protected override void OnStop()
        {
            inbox.Dispose();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            inbox.ProcessMailForExpenses("claim");
        }

        
        
    }
}
