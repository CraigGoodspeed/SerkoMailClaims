using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailHandler;
using System.Xml.Serialization;
using DataLayer.DTO;

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
            //string toParse = "Hi Yvaine,\nPlease create an expense claim for the below.\nrequested...\nRelevant details are marked up as\n<expense><cost_centre>DEV002</cost_centre>\n<total>1024.01</total><payment_method>personal card</payment_method>\n</expense>\nFrom: Ivan Castle\nSent: Friday, 16 February 2018 10:32 AM\nTo: Antoine Lloyd <Antoine.Lloyd@example.com>\nSubject: test\nHi Antoine,\nPlease create a reservation at the <vendor>Viaduct Steakhouse</vendor> our\n<description>development team’s project end celebration dinner</description> on\n<date>Tuesday 27 April 2017</date>. We expect to arrive around\n7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.\nRegards,\nIvan";
            //string toParse = "--000000000000832260058b9711db\nContent-Type: text/plain; charset=\"UTF-8\"\n\nasddasd\n<Total>123.22</Total>\n\n---------------------------------\nKind regards\nCraig Goodspeed\nProgrammer\n082 454 2957\nwww.goodspeed.co.za\n---------------------------------\n\n--000000000000832260058b9711db\nContent-Type: text/html; charset=\"UTF-8\"\nContent-Transfer-Encoding: quoted-printable\n\n<div dir=3D\"ltr\"><div class=3D\"gmail_default\" style=3D\"font-family:verdana,=\nsans-serif\">asddasd</div><div class=3D\"gmail_default\" style=3D\"font-family:=\nverdana,sans-serif\">&lt;Total&gt;123.22&lt;/Total&gt;</div><div class=3D\"gm=\nail_default\" style=3D\"font-family:verdana,sans-serif\"><br clear=3D\"all\"></d=\niv><div><div dir=3D\"ltr\" class=3D\"gmail_signature\" data-smartmail=3D\"gmail_=\nsignature\"><div dir=3D\"ltr\">---------------------------------<div>Kind rega=\nrds</div><div>Craig Goodspeed</div><div>Programmer</div><div>082 454 2957</=\ndiv><div><a href=3D\"http://www.goodspeed.co.za\" target=3D\"_blank\">www.goods=\npeed.co.za</a></div><div>---------------------------------</div></div></div=\n></div></div>\n\n--000000000000832260058b9711db--";
            string toParse = "<total>112.12</total>\nasdkjhaskd ajskdh\n<cost_centre>mooo</cost_centre>\nuahsd";
            XmlDocument parsedSample = StringHelper.ParseString(toParse);
            ExpenseDTO expenseClaim = null;
            XmlSerializer serial = new XmlSerializer(typeof(ExpenseDTO));
            using (XmlReader reader = new XmlNodeReader(parsedSample))
            {
                expenseClaim = (ExpenseDTO)serial.Deserialize(reader);
            }
            Assert.IsTrue(expenseClaim.Total == new decimal(1024.01d));
            Assert.IsTrue(expenseClaim.CostCentre == "DEV002");


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
