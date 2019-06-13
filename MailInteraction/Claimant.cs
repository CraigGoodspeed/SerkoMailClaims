using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MailInteraction
{
    abstract class Claimant
    {
        public string claimant { get; set; }

        protected abstract List<XmlDocument> parseData();

        public delegate void persist(Claimant data);

        public void persist(Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using(StringReader sr = new StringReader(parseData()[0].OuterXml))
            {
                serializer.Deserialize(sr)
            }
        }
    }
}
