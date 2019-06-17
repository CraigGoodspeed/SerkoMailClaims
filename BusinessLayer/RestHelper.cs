using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessLayer
{
    public class RestHelper
    {
        public static XmlDocument callRestService(string uri, string message)
        {
            XmlDocument xdoc = new XmlDocument();
            using (HttpClient endPoint = new HttpClient())
            {
                StringContent sendMe = new StringContent(message);
                xdoc.Load(endPoint.PostAsync(uri, sendMe).Result.Content.ReadAsStreamAsync().Result);
            }
            return xdoc;
        }


    }
}
