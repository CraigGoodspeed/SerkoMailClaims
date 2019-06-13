using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailInteraction;
using System.Collections.Generic;

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
            helper.sendMessage("serkomailclaims@gmail.com", string.Format("zazaza {0}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")), "claim");
        }
        [TestMethod]
        public void CheckGmailConnection()
        {
            checkMessageSend();
            List<ClaimDetails> details = helper.getStringMessages("claim");
            Assert.IsTrue(details.Count == 1);
        }


        [TestMethod]
        public void CheckAttachments()
        {
            checkMessageSend();
            List<ClaimDetails> details = helper.getAttachmentMessages("claim");
            Assert.IsTrue(details.Count == 1);
        }

        [TestCleanup()]
        public void cleanUp()
        {
            helper.Dispose();
        }
    }
}
