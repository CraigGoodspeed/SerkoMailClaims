using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace restAPI.Controllers
{
    [Route("~/expense/claim")]
    public class ExpenseController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage parseInputString()
        {
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(MailHandler.StringHelper.ParseString(Request.Content.ReadAsStringAsync().Result).OuterXml, System.Text.Encoding.UTF8, "application/xml") };
        }

        [HttpGet]
        public string zaza()
        {
            return "zaza";
        }

    }
}
