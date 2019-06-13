using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MailHandler
{
    public static class StringHelper
    {
        private const char OPENCHARACTER = '<';
        private const char CLOSECHARACTER = '>';
        private const string CLOSINGSTRING = "</{0}>";
        private const string ROOTELEMENTTORETURN = "expense";
        private const string regExToValidateElementNames = "[@?]";
        public static XmlDocument ParseString(string toParse, string rootElementName = ROOTELEMENTTORETURN)
        {
            XmlDocument toReturn = new XmlDocument();
            XmlElement rootElement = toReturn.CreateElement(rootElementName);
            toReturn.AppendChild(rootElement);
            StringReader reader = new StringReader(toParse);//Please note -- using statement not required see https://docs.microsoft.com/en-us/dotnet/api/system.io.stringreader?view=netframework-4.6
            
            XmlElement currentNode = null;
            string currentElement = string.Empty;
            string content = string.Empty;
            string endMarker = string.Empty;
            char[] endString = new char[0];
            bool open = false;
            
            for (int currentValue = reader.Read(); currentValue > -1; currentValue = reader.Read())
            {
                char currentCharacter = (char)currentValue;
                if (string.IsNullOrEmpty(currentElement) || open)
                {
                    open = (open || currentCharacter == OPENCHARACTER) && currentCharacter != CLOSECHARACTER;
                    if (open && currentCharacter != OPENCHARACTER)
                    {
                        //we are open lets add to currentElement.
                        currentElement = string.Concat(currentElement, currentCharacter);
                    }
                    else if(!string.IsNullOrEmpty(currentElement))//the only way we hit this is when we encounter > so the current element is known.
                    {
                        if (!ValidateCurrentElement(currentElement))
                        {
                            currentElement = string.Empty;
                            continue;
                        }
                        endMarker = string.Format(CLOSINGSTRING, currentElement);
                        endString = new char[endMarker.Length];
                        currentNode = toReturn.CreateElement(currentElement);
                        
                    }
                }
                else
                {
                    content = string.Concat(content, currentCharacter);
                    ManipulateCharArray(endString,currentCharacter);
                    if (new string(endString) == endMarker)
                    {
                        if (currentNode.Name == rootElementName)
                        {
                            rootElement.InnerXml = content.Substring(0, content.Length - endString.Length);
                        }
                        else
                        {
                            currentNode.InnerXml = content.Substring(0, content.Length - endString.Length);
                            rootElement.AppendChild(currentNode);
                        }
                        open = false;
                        currentElement = string.Empty;
                        content = string.Empty;
                        endString = new char[0];
                    }
                }
            }

            return toReturn;
        }
        public static void ManipulateCharArray(char[] items, char nextItem)
        {
            //[a,s,d,f,g],j
            for (int i = 1; i < items.Length; i++)
            {
                items[i - 1] = items[i];
            }
            items[items.Length - 1] = nextItem;
        }
        public static bool ValidateCurrentElement(string elementName)
        {
            Regex reg = new Regex(regExToValidateElementNames);
            return !reg.IsMatch(elementName);
        }

        public static T deserialiseContent<T>(string body)
        {
            XmlDocument parsedSample = StringHelper.ParseString(body);
            XmlSerializer serial = new XmlSerializer(typeof(T));
            T toReturn = default(T);
            using (XmlReader reader = new XmlNodeReader(parsedSample))
            {
                toReturn = (T)serial.Deserialize(reader);
            }
            return toReturn;
        }
    }
}
