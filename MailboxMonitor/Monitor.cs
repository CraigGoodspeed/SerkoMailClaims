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
        System.Timers.Timer timer = null;
        public Monitor()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            resetTimer();
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string smtpConfig = System.Configuration.ConfigurationManager.AppSettings["mailSetupPath"];
            string httpRestEndPoint = System.Configuration.ConfigurationManager.AppSettings["restEndPoint"];
            ProcessInbox inbox = new ProcessInbox(httpRestEndPoint);
            inbox.ProcessMailForExpenses("claim");
        }
        private void resetTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = double.Parse(System.Configuration.ConfigurationManager.AppSettings["mailCheckIntervalSeconds"]) * 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            try
            {
                inbox.Dispose();
            }
            catch (Exception) { }
        }
    }
}
