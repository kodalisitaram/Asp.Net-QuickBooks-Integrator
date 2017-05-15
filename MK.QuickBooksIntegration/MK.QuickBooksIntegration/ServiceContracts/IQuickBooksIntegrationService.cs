using System.ServiceModel;
using System.ServiceModel.Web;

namespace MK.QuickBooksIntegration.ServiceContracts
{
    [ServiceContract(Namespace = "http://developer.intuit.com/")]
    [XmlSerializerFormat]
    public interface IQuickBooksIntegrationService
    {
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/serverVersion", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [OperationContract(Action = "http://developer.intuit.com/serverVersion")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedResponse, RequestFormat = WebMessageFormat.Xml)]
        string serverVersion();

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/clientVersion", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [OperationContract(Action = "http://developer.intuit.com/clientVersion")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedResponse, RequestFormat = WebMessageFormat.Xml)]
        string clientVersion(string strVersion);

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/closeConnection", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [OperationContract(Action = "http://developer.intuit.com/closeConnection")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedResponse, RequestFormat = WebMessageFormat.Xml)]
        string closeConnection(string ticket);


        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/connectionError", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [OperationContract(Action = "http://developer.intuit.com/connectionError")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedResponse, RequestFormat = WebMessageFormat.Xml)]
        string connectionError(string ticket, string hresult, string message);


        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/getLastError", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [OperationContract(Action = "http://developer.intuit.com/getLastError")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedResponse, RequestFormat = WebMessageFormat.Xml)]
        string getLastError(string ticket);


        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/authenticate", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [OperationContract(Action = "http://developer.intuit.com/authenticate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare)]
        string[] authenticate(string strUserName, string strPassword);

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/receiveResponseXML", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [OperationContract(Action = "http://developer.intuit.com/receiveResponseXML")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedResponse, RequestFormat = WebMessageFormat.Xml)]
        int receiveResponseXML(string ticket, string response, string hresult, string message);

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://developer.intuit.com/sendRequestXML", RequestNamespace = "http://developer.intuit.com/", ResponseNamespace = "http://developer.intuit.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [OperationContract(Action = "http://developer.intuit.com/sendRequestXML")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedResponse, RequestFormat = WebMessageFormat.Xml)]
        string sendRequestXML(string ticket, string strHCPResponse, string strCompanyFileName, string qbXMLCountry, int qbXMLMajorVers, int qbXMLMinorVers);
    }
}
