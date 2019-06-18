using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailInteraction;
using System.Collections.Generic;
using MailHandler;
using Pop3;
using DataLayer.DTO;
using DataLayer;
using System.Xml;
using BusinessLayer;

namespace Tests
{
    [TestClass]
    public class MailInteraction
    {
        MailHelper helper;

        [TestInitialize()]
        public void setupVariables()
        {
            ServerProperties pop3 = new ServerProperties("pop.gmail.com", 995, true);
            ServerProperties smtp = new ServerProperties("smtp.gmail.com", 587, true);
            helper = new MailHelper(pop3, smtp, "serkomailclaims@gmail.com", "Th!$!$$3cur3!");
        }
        public void checkMessageSend()
        {
            string body = "Hi Yvaine,\nPlease create an expense claim for the below.\nrequested...\nRelevant details are marked up as\n<expense><cost_centre>DEV002</cost_centre>\n<total>1024.01</total><payment_method>personal card</payment_method>\n</expense>\nFrom: Ivan Castle\nSent: Friday, 16 February 2018 10:32 AM\nTo: Antoine Lloyd <Antoine.Lloyd@example.com>\nSubject: test\nHi Antoine,\nPlease create a reservation at the <vendor>Viaduct Steakhouse</vendor> our\n<description>development team’s project end celebration dinner</description> on\n<date>Tuesday 27 April 2017</date>. We expect to arrive around\n7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.\nRegards,\nIvan";
            helper.sendMessage("serkomailclaims@gmail.com", body, "claim");
        }
        [TestMethod]
        public void CheckGmailConnection()
        {
            checkMessageSend();
            List<Pop3.Pop3Message> details = helper.getStringMessages("claim");
            foreach (Pop3Message claim in details)
            {
                ExpenseDTO ex = StringHelper.deserialiseContent<ExpenseDTO>(claim.RawMessage);
                if(ex.Total != 0)
                    Assert.IsTrue(ex.Total == new decimal(1024.01d));
                helper.sendMessage(claim.From, "thank you for submitting your claim, it is busy being processed.", "this claim is being processed");
            }
        }


        [TestMethod]
        public void ProcessInboxBusinessLayer()
        {
            //checkMessageSend();
            //XmlDocument mailConfig = new XmlDocument();
            //mailConfig.Load("./mailConfig.xml");
            ProcessInbox inbox = new ProcessInbox("http://localhost:8000/expense/claim");
            inbox.ProcessMailForExpenses("claim");
        }

        [TestCleanup()]
        public void cleanUp()
        {
            helper.Dispose();
        }
    }
}
