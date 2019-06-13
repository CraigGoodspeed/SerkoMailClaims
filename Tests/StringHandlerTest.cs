using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailHandler;
using System.Xml.Serialization;
using Entities;

namespace Tests
{
    [TestClass]
    public class StringHandlerTests
    {
        [TestMethod]
        public void ParseSimpleString()
        {
            string toParse = "hello world <how> asdkjh this is hows hows content </how> can we like to \nmajsd\t <good>nice one brother</good>";
            XmlDocument returned = StringHelper.ParseString(toParse);
            Assert.IsTrue(returned.GetElementsByTagName("how").Count == 1);
            Assert.IsTrue(returned.GetElementsByTagName("good").Count == 1);
        }

        [TestMethod]
        public void ParseGivenSample()
        {
            string toParse = "Hi Yvaine,\nPlease create an expense claim for the below.\nrequested...\nRelevant details are marked up as\n<expense><cost_centre>DEV002</cost_centre>\n<total>1024.01</total><payment_method>personal card</payment_method>\n</expense>\nFrom: Ivan Castle\nSent: Friday, 16 February 2018 10:32 AM\nTo: Antoine Lloyd <Antoine.Lloyd@example.com>\nSubject: test\nHi Antoine,\nPlease create a reservation at the <vendor>Viaduct Steakhouse</vendor> our\n<description>development team’s project end celebration dinner</description> on\n<date>Tuesday 27 April 2017</date>. We expect to arrive around\n7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.\nRegards,\nIvan";
            XmlDocument parsedSample = StringHelper.ParseString(toParse);
            Expense expenseClaim = null;
            XmlSerializer serial = new XmlSerializer(typeof(Expense));
            using (XmlReader reader = new XmlNodeReader(parsedSample))
            {
                expenseClaim = (Expense)serial.Deserialize(reader);
            }
            Assert.IsTrue(expenseClaim.Total == 1024.01d);
            Assert.IsTrue(expenseClaim.CostCenter == "DEV002");


        }
        [TestMethod]
        public void AttemptCustomPOP()
        {
            char[] toPop = new char[]{'a','s','d','f','g'};
            StringHelper.ManipulateCharArray(toPop,'j');
            Assert.IsTrue(new string(toPop) == "sdfgj");
        }
    }
}
