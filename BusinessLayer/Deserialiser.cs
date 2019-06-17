using MailInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessLayer
{
    public class Deserialiser
    {
        public static T deserialiseContent<T>(XmlDocument xdoc)
        {
            XmlSerializer serial = new XmlSerializer(typeof(T));
            T toReturn = default(T);
            using (XmlReader reader = new XmlNodeReader(xdoc))
            {
                toReturn = (T)serial.Deserialize(reader);
            }
            return toReturn;
        }
        public static ServerProperties loadMailConfiguration(XmlNode node, XmlSerializer serializer)
        {
            return (ServerProperties)serializer.Deserialize(new XmlNodeReader(node));
        }
    }
}
