using Acme.Models.QuickBooks;
using Acme.Data.Repositories;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Acme.Services.Tracking;

namespace Acme.Services.Accounting.QuickBooks
{
    /// <summary>
    /// Wrapper class for interfacing with QBO
    /// Handles:
    /// QBO authentication and Token management
    /// CRUD calls for QBO entities.
    /// </summary>
    public class QuickBooksService
    {
        private IAccountingCredentialsService _credentialsSvc;
        private WorkOrderRepository _workOrderRepository;
   
        public QuickBooksService()
        {
            _credentialsSvc = new QuickBooksCredentialService();
            _workOrderRepository = new WorkOrderRepository();
        }

        public QuickBooksService(IAccountingCredentialsService credentialsService)
        {
            _credentialsSvc = credentialsService;
            _workOrderRepository = new WorkOrderRepository();
        }


        #region Properties
        // Store credentials in the session.
        AccountingCredentials _creds = null;
        private AccountingCredentials GetCredentials()
        {
                if (_creds == null)
                {
                    _creds = _credentialsSvc.GetCredentials();
                }
                return _creds;
        }

        private void SaveCredentials(AccountingCredentials creds)
        {
            _credentialsSvc.SaveCredentials(creds);
            _creds = null;
        }
 
        // Get the Intuit service context.
        private ServiceContext ServiceContext
        {
            get
            {
                var creds = GetCredentials();
                OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(creds.Token);
                // Create a ServiceContext with Auth tokens and realmId
                ServiceContext serviceContext = new ServiceContext(creds.RealmId, IntuitServicesType.QBO, oauthValidator);
                serviceContext.IppConfiguration.MinorVersion.Qbo = "23";
                serviceContext.IppConfiguration.BaseUrl.Qbo = getApiUrl();
                return serviceContext;
            }
        }

        // Get the Intuit OAuth Client.
        private OAuth2Client _oAuthClient;
        private OAuth2Client OAuthClient
        {
            get
            {
                if (_oAuthClient == null)
                {
                    string clientid = ConfigurationManager.AppSettings["clientid"];
                    string clientsecret = ConfigurationManager.AppSettings["clientsecret"];
                    string redirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
                    string environment = ConfigurationManager.AppSettings["appEnvironment"];
                    _oAuthClient = new OAuth2Client(clientid, clientsecret, redirectUrl, environment);
                }
                return _oAuthClient;
            }
        }
        #endregion

        #region Public Methods


        /// <summary>
        /// Gets the QBO url for authorization.
        /// </summary>
        /// <returns>QBO Authentication Url</returns>
        public string GetAuthorizeUrl()
        {
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            return OAuthClient.GetAuthorizationURL(scopes);
        }

        /// <summary>
        /// Checks if authentication is still valid.
        /// </summary>
        /// <returns>True if already authenticated and credentials are still valid, false otherwise.</returns>
        public async System.Threading.Tasks.Task<bool> IsAuthenticated()
        {
            try
            {
                var creds = GetCredentials();
                if(!string.IsNullOrWhiteSpace(creds.RefreshToken))
                {
                    await RefreshToken(creds.RefreshToken, creds.RealmId);
                }

                return creds != null && !string.IsNullOrEmpty(creds.RealmId) && !string.IsNullOrEmpty(creds.Token);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Stores QBO authentication token, token expiry and realmId.
        /// </summary>
        /// <param name="realmId">Realm Id returned from QBO authentication callback</param>
        /// <param name="code">Code returned from QBO authentication callback</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task SetToken(string realmId, string code)
        {
            var token = await OAuthClient.GetBearerTokenAsync(code);

            AccountingCredentials creds = new AccountingCredentials()
            {
                RealmId = realmId,
                Token = token.AccessToken,
                RefreshToken = token.RefreshToken,
                Expiration = token.AccessTokenExpiresIn
            };

            SaveCredentials(creds);
        }

        public async System.Threading.Tasks.Task RefreshToken(string refreshToken, string realmId)
        {
            var token = await OAuthClient.RefreshTokenAsync(refreshToken);
            var creds = new AccountingCredentials()
            {
                RealmId = realmId,
                Token = token.AccessToken,
                RefreshToken = token.RefreshToken,
                Expiration = token.AccessTokenExpiresIn
            };

            SaveCredentials(creds);
        }

        public void Ping()
        {
            DataService ds = new DataService(ServiceContext);
            _entityList = ds.FindAll(new Intuit.Ipp.Data.Customer(), 1, 1);
            
        }

        static ReadOnlyCollection<Intuit.Ipp.Data.Customer> _entityList = null;
        static List<Models.QuickBooks.Customer> _customers;
        static Item _laborItem;
        static Item _materialsItem;
        static Item _reportedItem;
        static Item _resolutionItem;
        static Term _term;
        static TaxCode _stateTaxCode;

        /// <summary>
        /// Save invoices to QBO 
        /// </summary>
        /// <param name="qboInvoices">List of internal invoices.</param>
        /// <returns>List of QBO invoices that were created.</returns>
        public List<QuickbooksResult<QboInvoice>> SaveInvoices(List<QboInvoice> qboInvoices)
        {
            InitializeQBOItems();

            var results = new List<QuickbooksResult<QboInvoice>>();

            foreach (var qboInvoice in qboInvoices)
            {
                try
                {
                    QuickbooksResult<QboInvoice> result = new QuickbooksResult<QboInvoice>();

                    //Customer Ids don't line up between production and sandbox.
                    //When running against the sandbox, call AssignSanboxCustomerToInvoice.
                    if (isSandbox())
                    {
                        AssignSanboxCustomerToInvoice(qboInvoice);
                    }

                    //Save the invoice to QBO
                    //results.Add(new QuickbooksResult<QboInvoice>() { Entity = qboInvoice, OperationSucceeded = true });
                    results.Add(SaveInvoice(qboInvoice));
                    _workOrderRepository.MarkInvoiceAsUploaded(int.Parse(qboInvoice.Id), int.Parse(qboInvoice.QboId), qboInvoice.QboInvoiceNum, qboInvoice.QboAmount, qboInvoice.QboTxnDate);                  
                }
                catch (Exception ex)
                {
                    //Capture errors
                    results.Add(new QuickbooksResult<QboInvoice>()
                    {
                        Entity = qboInvoice,
                        OperationSucceeded = false,
                        ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                    });
                }
            }

            return results;
        }

        /// <summary>
        /// Save invoice to QBO
        /// </summary>
        /// <param name="qboInvoice">Internal invoice</param>
        /// <returns>QBO create invoice</returns>
        public QuickbooksResult<QboInvoice> SaveInvoice(QboInvoice qboInvoice)
        {
            DataService ds = new DataService(ServiceContext);
            //ds.GetPdf()

            // Create the invoice
            Invoice invoice = new Invoice();

            // Assign the customer to the invoice.
            invoice.CustomerRef = new ReferenceType()
            {
                name = qboInvoice.CustomerName,
                Value = qboInvoice.CustomerRefID.ToString()
            };

            invoice.TxnDate = qboInvoice.TxnDate;
            invoice.TxnDateSpecified = true;
            invoice.DueDate = qboInvoice.DueDate;
            invoice.DueDateSpecified = true;

            // Customer memo and notes
            invoice.CustomerMemo = new MemoRef() { Value = qboInvoice.CustomerMemo_Value };
            invoice.PrivateNote = qboInvoice.CustomerMemo_Value;


            //SalesTermRef
            invoice.SalesTermRef = new ReferenceType()  
            {
                name = _term.Name,
                Value = _term.Id
            };

            List<Line> lineList = new List<Line>();

            //Add Item Reported and Item Resolution.
            Line itemReportedLine = new Line();
            itemReportedLine.AmountSpecified = true;
            itemReportedLine.Description = qboInvoice.Description;
            itemReportedLine.Amount = new Decimal(0);
            itemReportedLine.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            itemReportedLine.DetailTypeSpecified = true;


            SalesItemLineDetail lineItemReportedSalesItemLineDetail = new SalesItemLineDetail();
            lineItemReportedSalesItemLineDetail.AnyIntuitObject = new Decimal(0);
            lineItemReportedSalesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
            lineItemReportedSalesItemLineDetail.Qty = new Decimal(1);
            lineItemReportedSalesItemLineDetail.QtySpecified = true;
            lineItemReportedSalesItemLineDetail.ServiceDate = qboInvoice.TxnDate;
            lineItemReportedSalesItemLineDetail.ServiceDateSpecified = true;

            lineItemReportedSalesItemLineDetail.ItemRef = new ReferenceType()
            {
                name = _reportedItem.Name,
                Value = _reportedItem.Id
            };
            itemReportedLine.AnyIntuitObject = lineItemReportedSalesItemLineDetail;
            lineList.Add(itemReportedLine);

            Line itemResolutionLine = new Line();
            itemResolutionLine.AmountSpecified = true;
            itemResolutionLine.Description = qboInvoice.Resolution;
            itemResolutionLine.Amount = new Decimal(0);
            itemResolutionLine.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            itemResolutionLine.DetailTypeSpecified = true;

            SalesItemLineDetail lineItemResolutionSalesItemLineDetail = new SalesItemLineDetail();
            lineItemResolutionSalesItemLineDetail.AnyIntuitObject = new Decimal(0);
            lineItemResolutionSalesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
            lineItemResolutionSalesItemLineDetail.Qty = new Decimal(1);
            lineItemResolutionSalesItemLineDetail.QtySpecified = true;
            lineItemResolutionSalesItemLineDetail.ServiceDate = qboInvoice.TxnDate;
            lineItemResolutionSalesItemLineDetail.ServiceDateSpecified = true;
            lineItemResolutionSalesItemLineDetail.ItemRef = new ReferenceType()
            {
                name = _resolutionItem.Name,
                Value = _resolutionItem.Id
            };
            itemResolutionLine.AnyIntuitObject = lineItemResolutionSalesItemLineDetail;
            lineList.Add(itemResolutionLine);


            // Add line items.  
            foreach (var qboLineItem in qboInvoice.LineItems)
            {
                //Line
                Line invoiceLine = new Line();
                //Line Description
                invoiceLine.Description = qboLineItem.Description;

                //Line Amount
                invoiceLine.Amount = new Decimal(qboLineItem.Amount); 
                if(qboLineItem.SalesItemLineDetail_Qty > 1)
                {
                    invoiceLine.Amount = new Decimal(qboLineItem.Amount * qboLineItem.SalesItemLineDetail_Qty);
                }
                
                invoiceLine.AmountSpecified = true;
                //Line Detail Type
                invoiceLine.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                invoiceLine.DetailTypeSpecified = true;
                //Line Sales Item Line Detail
                SalesItemLineDetail lineSalesItemLineDetail = new SalesItemLineDetail();
                //Line Sales Item Line Detail - ItemRef

                var item = qboLineItem.SalesItemLineDetail_ItemRefName == "Materials" ? _materialsItem : _laborItem;

                lineSalesItemLineDetail.ItemRef = new ReferenceType()
                {
                    name = item.Name,
                    Value = item.Id
                };

                //Line Sales Item Line Detail - UnitPrice
                lineSalesItemLineDetail.AnyIntuitObject = new Decimal(qboLineItem.SalesItemLineDetail_UnitPrice);
                lineSalesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
                //Line Sales Item Line Detail - Qty
                lineSalesItemLineDetail.Qty = new Decimal(qboLineItem.SalesItemLineDetail_Qty);
                lineSalesItemLineDetail.QtySpecified = true;

                //Line Sales Item Line Detail - TaxCodeRef
                //For US companies, this can be 'TAX' or 'NON'
                lineSalesItemLineDetail.TaxCodeRef = new ReferenceType()
                {
                    Value = qboLineItem.SalesItemLineDetail_TaxCodeRefId
                };

                //Line Sales Item Line Detail - ServiceDate 
                lineSalesItemLineDetail.ServiceDate = qboLineItem.SalesItemLineDetail_ServiceDate;
                lineSalesItemLineDetail.ServiceDateSpecified = true;
                //Assign Sales Item Line Detail to Line Item
                invoiceLine.AnyIntuitObject = lineSalesItemLineDetail;

                lineList.Add(invoiceLine);
            }

            invoice.Line = lineList.ToArray();

            //TxnTaxDetail
            TxnTaxDetail txnTaxDetail = new TxnTaxDetail();
            txnTaxDetail.TxnTaxCodeRef = new ReferenceType()
            {
                name = _stateTaxCode.Name,
                Value = _stateTaxCode.Id
            };

            Line taxLine = new Line();
            taxLine.DetailType = LineDetailTypeEnum.TaxLineDetail;
            TaxLineDetail taxLineDetail = new TaxLineDetail();
            //Assigning the fist Tax Rate in this Tax Code
            taxLineDetail.TaxRateRef = _stateTaxCode.SalesTaxRateList.TaxRateDetail[0].TaxRateRef;
            taxLine.AnyIntuitObject = taxLineDetail;
            txnTaxDetail.TaxLine = new Line[] { taxLine };
            invoice.TxnTaxDetail = txnTaxDetail;

            invoice = ds.Add(invoice);
            qboInvoice.QboId = invoice.Id;
            qboInvoice.QboInvoiceNum = invoice.DocNumber;
            qboInvoice.QboAmount = (double)invoice.TotalAmt;
            qboInvoice.QboTxnDate = invoice.TxnDate;
            qboInvoice.Link = invoice.InvoiceLink;
            
            return new QuickbooksResult<QboInvoice>()
            {
                OperationSucceeded = true,
                Entity = qboInvoice
            };
        }
        #endregion
        
        private List<Acme.Models.QuickBooks.Customer> GetCustomers()
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.GetCustomers();
        }

        private void InitializeQBOItems()
        {
            if (_laborItem == null)
            {
                DataService ds = new DataService(ServiceContext);
                QueryService<TaxCode> stateTaxCodeQueryService = new QueryService<TaxCode>(ServiceContext);
                QueryService<Term> termQueryService = new QueryService<Term>(ServiceContext);
                QueryService<Item> itemQueryService = new QueryService<Item>(ServiceContext);
                _laborItem = itemQueryService.ExecuteIdsQuery("Select * From Item Where Name = 'Labor' StartPosition 1 MaxResults 1").FirstOrDefault();
                _materialsItem = itemQueryService.ExecuteIdsQuery("Select * From Item Where Name = 'Materials' StartPosition 1 MaxResults 1").FirstOrDefault();
                _reportedItem = itemQueryService.ExecuteIdsQuery("Select * From Item Where Name = 'Item Reported' StartPosition 1 MaxResults 1").FirstOrDefault();
                _resolutionItem = itemQueryService.ExecuteIdsQuery("Select * From Item Where Name = 'Item Resolution' StartPosition 1 MaxResults 1").FirstOrDefault();
                _term = termQueryService.ExecuteIdsQuery("Select * From Term Where Name='Due on receipt' StartPosition 1 MaxResults 1").FirstOrDefault();
                //_stateTaxCode = stateTaxCodeQueryService.ExecuteIdsQuery("Select * From TaxCode StartPosition 1 MaxResults 1").FirstOrDefault();
                _stateTaxCode = stateTaxCodeQueryService.ExecuteIdsQuery("Select * From TaxCode Where Name = 'CA Sales Tax as of 4/1/18' StartPosition 1 MaxResults 1").FirstOrDefault();
            }
        }

        public List<QuickbooksResult<string>> VerifyItems()
        {
            List<QuickbooksResult<string>> results = new List<QuickBooks.QuickbooksResult<string>>();

            try
            {
                InitializeQBOItems();

                if (_laborItem == null) { results.Add(new QuickbooksResult<string>() { ErrorMessage = "'Labor' Item Not Found", OperationSucceeded = false }); }
                if (_materialsItem == null) { results.Add(new QuickbooksResult<string>() { ErrorMessage = "'Materials' Item Not Found", OperationSucceeded = false }); }
                if (_reportedItem == null) { results.Add(new QuickbooksResult<string>() { ErrorMessage = "'Item Reported' Not Found", OperationSucceeded = false }); }
                if (_resolutionItem == null) { results.Add(new QuickbooksResult<string>() { ErrorMessage = "'Item Resolution' Not Found", OperationSucceeded = false }); }
                if (_term == null) { results.Add(new QuickbooksResult<string>() { ErrorMessage = "'Term' Not Found", OperationSucceeded = false }); }
                if (_stateTaxCode == null) { results.Add(new QuickbooksResult<string>() { ErrorMessage = "TaxCode Not Found", OperationSucceeded = false }); }
            }
            catch(Exception ex)
            {
                results.Add(new QuickbooksResult<string>()
                {
                    OperationSucceeded = false,
                    ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                }); 

            }

            if(results.Count == 0)
            {
                results.Add(new QuickbooksResult<string>() { ErrorMessage = "SUCCESS: All items are correctly configured in QBO", OperationSucceeded = true });
            }
 
            return results;
        }


        public DateTime GetInvoice(string invoiceId)
        {
            //DataService ds = new DataService(ServiceContext);
            //var invoice = ds.FindById<Invoice>(new Invoice() { Id = invoiceId.ToString() });
            QueryService<Invoice> invoiceQueryService = new QueryService<Invoice>(ServiceContext);
            var invoice = invoiceQueryService.ExecuteIdsQuery($"SELECT * FROM Invoice WHERE DocNumber = '{invoiceId}'").FirstOrDefault();
            System.Diagnostics.Trace.WriteLine($"{invoice.TotalAmt}");
            return invoice.TxnDate;
        }

        private void AssignSanboxCustomerToInvoice(QboInvoice qboInvoice)
        {
            if (_entityList == null)
            {
                DataService ds = new DataService(ServiceContext);
                _entityList = ds.FindAll(new Intuit.Ipp.Data.Customer(), 1, 500);
                _customers = GetCustomers();
            }

            //Line up customer ids with the sandbox customers.
            var customer = _customers.FirstOrDefault(x => x.ID == qboInvoice.CustomerRefID);

            if (customer == null)
            {
                throw new Exception($"Customer id {qboInvoice.CustomerRefID} does not exist in our database.");
            }

            var qboCustomer = _entityList.FirstOrDefault(x => x.DisplayName == customer.DisplayName);

            if (qboCustomer == null)
            {
                throw new Exception($"Customer does not exist in Quickbooks Online.");
            }

            qboInvoice.CustomerRefID = int.Parse(qboCustomer.Id);
        }


        //TBD: Read from config.
        private bool isSandbox()
        {
            return false;
        }

        private string getApiUrl()
        {
            return !isSandbox() ? "https://quickbooks.api.intuit.com" : "https://sandbox-quickbooks.api.intuit.com";
        }

        //Temp Used for adding customers to sandbox for testing.
        /*
        private void UploadCustomers()
        {
            var customers = GetCustomers();
            SaveCustomers(customers);
        }


        private void SaveCustomers(List<Acme.Models.QuickBooks.Customer> customers)
        {
            DataService ds = new DataService(ServiceContext);
            int count = 0;
            int max = 100;
            int start = 300;

            foreach (var customer in customers)
            {
                count++;
                if (count > start && count <= max + start)
                {
                    var qbCustomer = new Intuit.Ipp.Data.Customer();
                    qbCustomer.DisplayName = customer.DisplayName;
                    qbCustomer.FullyQualifiedName = customer.FullyQualifiedName;
                    qbCustomer = ds.Add(qbCustomer);
                }
            }
        }
        */
    }

}
