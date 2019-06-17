using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

	[OperationContract]
    [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Xml,
        ResponseFormat = WebMessageFormat.Xml,
        UriTemplate = "mail/claim"
        )
    ]
	XmlDocument ParseIncomingString(string body);

    [OperationContract]
    [WebGet(UriTemplate = "test", ResponseFormat = WebMessageFormat.Xml, RequestFormat = WebMessageFormat.Xml)]  
    string test();

}
