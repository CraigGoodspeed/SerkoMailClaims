using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    
    public XmlDocument ParseIncomingString(string body)
	{
        return MailHandler.StringHelper.ParseString(body);
	}

    public string test()
    {
        return "test";
    }
	
}
