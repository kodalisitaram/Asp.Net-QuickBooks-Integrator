using MK.QuickBooksIntegration.ServiceContracts;
using QbSync.QbXml;
using QbSync.QbXml.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MK.QuickBooksIntegration
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, Namespace = "http://developer.intuit.com/")]
    public class QuickBooksIntegrationService : IQuickBooksIntegrationService
    {
        private string LogFilePath = ConfigurationManager.AppSettings["QBLogPath"].ToString();
        private string LogFile = ConfigurationManager.AppSettings["QBLogFile"].ToString();
        private char Filler = '-';
        private bool QBIntegrationEnabled;
        private bool QBCustomersSynced;
        private string LastError;

        public string[] authenticate(string strUserName, string strPassword)
        {
            string[] _rtn = new string[4];
            _rtn[0] = strUserName;
            this.Log(true, "Authenticate", "Date Time : ");
            this.Log(false, "strUserName : ", strUserName);
            this.Log(false, "strPassword : ", strPassword);
            if (CheckHash(strPassword, "")) //Do the actual UserID and Password validation.
            {
                _rtn[1] = "";
            }
            else
            {
                _rtn[1] = "nvu"; //This means, Not Valid User
            }
            return _rtn;
        }

        private bool CheckHash(string strPassword, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public string clientVersion(string strVersion)
        {
            string _mk = ConfigurationManager.AppSettings["QBWCStatusReport"].ToString();
            if (_mk == "A")
            {
                return "";
            }
            else
            {
                return "E:";
            }
        }

        public string closeConnection(string ticket)
        {
            this.Log(true, "Close Connection", "Date Time : ");
            this.Log(false, "ticket : ", ticket);
            return "";
        }

        public string connectionError(string ticket, string hresult, string message)
        {
            this.Log(true, "Connection Error", "Date Time : ");
            this.Log(false, "ticket : ", ticket);
            this.Log(false, "message : ", message);
            return "";
        }

        public string getLastError(string ticket)
        {
            this.Log(true, "Get Last Error", "Date Time : ");
            this.Log(false, "ticket : ", ticket);
            return this.LastError;
        }

        public int receiveResponseXML(string ticket, string response, string hresult, string message)
        {
            this.Log(true, "Receive Response XML", "Date Time : ");
            this.Log(false, "ticket : ", ticket);
            this.Log(false, "response : ", response);
            this.Log(false, "hresult : ", hresult);
            this.Log(false, "message : ", message);

            //In your sendRequestXML method if you have requested to list out all the customers, then this is how you parse the customers data
            QbXmlResponse _res = new QbXmlResponse();
            var dta = _res.GetSingleItemFromResponse<CustomerQueryRsType>(response);
            foreach (CustomerRet _rtnCust in dta.CustomerRet)
            {
                //Process Individual Customer by inserting then in to a DB or what ever you want to do...
            }

            //In your sendRequestXML method if you have requested to add new customers, then this how you parse the new customer data.
            QbXmlResponse _res1 = new QbXmlResponse();
            List<CustomerAddRsType> dta1 = _res1.GetItemsFromResponse<CustomerAddRsType>(response).ToList();
            foreach (CustomerAddRsType _custAddRes in dta1)
            {
                //Makes sure to save the refrence number that QuickBooks Assigns to the customer.
            }

            //In your sendRequestXML method if you have requested to add new invoice, then this is how you parse the response data.
            QbXmlResponse _res2 = new QbXmlResponse();
            var _invStatusData = _res2.GetItemsFromResponse<InvoiceAddRsType>(response);
            foreach (InvoiceAddRsType _invStatusItem in _invStatusData)
            {
                //
            }

            //This is super important. Till you return 100, QVWC will continue to call the operations in loop. So use this to when the volume of data is high.
            //Test your implementation well.
            return 100;
        }

        public string sendRequestXML(string ticket, string strHCPResponse, string strCompanyFileName, string qbXMLCountry, int qbXMLMajorVers, int qbXMLMinorVers)
        {
            this.Log(true, "Send Request XML", "Date Time : ");
            this.Log(false, "ticket : ", ticket);
            this.Log(false, "QBCustomersSynced : ", this.QBCustomersSynced.ToString());
            //
            QbXmlRequest _QbXmlRequest = new QbXmlRequest();
            QBXMLMsgsRq _QBXmlMsgReq = new QBXMLMsgsRq();

            //How to Add One or Multiple Invoices
            List<InvoiceAddRqType> _invAddRequest = new List<InvoiceAddRqType>(); //Int a List 
            InvoiceAddRqType _qbInvoiceAddRequest = new InvoiceAddRqType();
            BillAddress _qbInvoiceBillTo = new BillAddress();
            InvoiceLineAdd _qbInvoiceLine = new InvoiceLineAdd();
            CustomerRef _qbInvoiceCustomer = new CustomerRef();
            ItemRef _qbInvoiceItem = new ItemRef();
            List<InvoiceLineAdd> _qbInvoiceLines = new List<InvoiceLineAdd>();
            InvoiceAdd _qbInvoice = new InvoiceAdd();

            _qbInvoiceAddRequest.requestID  = "Your Request ID";
            _qbInvoiceCustomer.ListID       = "";
            //Set Customer Reference
            _qbInvoice.CustomerRef          = _qbInvoiceCustomer;
            _qbInvoice.TxnDate              = Convert.ToDateTime((DateTime.Now).ToShortDateString());
            _qbInvoice.RefNumber            = "";

            _qbInvoiceBillTo.Addr1          = "";
            _qbInvoiceBillTo.Addr2          = "";
            _qbInvoiceBillTo.City           = "";
            _qbInvoiceBillTo.State          = "";
            _qbInvoiceBillTo.PostalCode     = "";
            //Set Bill To Address
            _qbInvoice.BillAddress          = _qbInvoiceBillTo;
            //Set The Item Details
            _qbInvoiceItem.FullName         = "YourInvoiceItem"; //Make Sure An Item is defined with the same name in your quick books account.

            _qbInvoiceLine.ItemRef          = _qbInvoiceItem;
            _qbInvoiceLine.Desc             = "";
            _qbInvoiceLine.Quantity         = 1;
            _qbInvoiceLine.Rate             = 0;
            _qbInvoiceLine.Amount           = 0;
            _qbInvoiceLine.TaxAmount        = 0;
            _qbInvoiceLines.Add(_qbInvoiceLine);
            //
            _qbInvoice.InvoiceLineAdd       = _qbInvoiceLines;
            _qbInvoiceAddRequest.InvoiceAdd = _qbInvoice;
            /**/
            _invAddRequest.Add(_qbInvoiceAddRequest); //Add you Invoice Request to the List Initialted above
            /**/

            /*Add a new Customer*/
            List<CustomerAddRqType> _newCustomers = new List<CustomerAddRqType>();
            CustomerAdd _custToBeAdded      = new CustomerAdd();
            _custToBeAdded.Name             = "";
            _custToBeAdded.CompanyName      = "";
            BillAddress _billTo             = new BillAddress();
            _billTo.Addr1                   = "";
            _billTo.Addr2                   = "";
            _billTo.City                    = "";
            _billTo.State                   = "";
            _billTo.Country                 = "";
            _billTo.PostalCode              = "";
            _custToBeAdded.ShipAddress      = new ShipAddress() { Addr1 = "", Addr2 = "", City = "", State = "", PostalCode = "", Country = "" };
            _custToBeAdded.BillAddress      = _billTo;
            //
            CustomerAddRqType _addRequest   = new CustomerAddRqType();
            _addRequest.CustomerAdd         = _custToBeAdded;
            _newCustomers.Add(_addRequest);
            /**/
            _QBXmlMsgReq.CustomerAddRq = _newCustomers; //You can add one or more new customers at a time
            _QBXmlMsgReq.InvoiceAddRq = _invAddRequest; //You can add one or more new invocies at a time
            _QbXmlRequest.Add(_QBXmlMsgReq);
            return _QbXmlRequest.GetRequest();

        }


        public string serverVersion()
        {
            this.Log(true, "Server Version", "Date Time : ");
            return "";
        }

        private void Log(bool Marker, string Title, string Data)
        {
            /* I used file logging to understand the sequence in which every operation is called.
             * It also hepled me understand the i/o parameters of every operation. 
             */ 

        }
    }
}
