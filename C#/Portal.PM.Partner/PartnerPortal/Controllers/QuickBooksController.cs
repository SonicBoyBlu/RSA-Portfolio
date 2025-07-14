using Acme.Models.QuickBooks;
using Acme.Services.Accounting.QuickBooks;
using Acme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class QuickBooksController : BaseController
    {
        private QuickBooksService QBOService;
        
        public QuickBooksController() : base()
        {
            QBOService = new QuickBooksService();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuickBooksConnect()
        {
            return Redirect(QBOService.GetAuthorizeUrl());
        }

        public ActionResult QuickBooksCreateInvoices()
        {
            return View();
        }

        public async Task<JsonNetResult> QuickBookStatus()
        {
            return new JsonNetResult(new
            {
                authenticated = await QBOService.IsAuthenticated()
            });
        }

        public JsonNetResult VerifyItems()
        {
            var results = QBOService.VerifyItems();

            return new JsonNetResult(new
            {
                results = results
            });
        }

        public JsonNetResult UploadToQuickBooks(List<QboInvoice> invoices)
        {
           
            var uploadedInvoices = QBOService.SaveInvoices(invoices);
            var user = (UserIdentity)User.Identity;

            foreach (QuickbooksResult<QboInvoice> result in uploadedInvoices)
            {
                //Persist the invoice upload user actions.
                if (result.OperationSucceeded)
                {
                    var invoice = result.Entity;
                    string details = $"{invoice.QboInvoiceNum} - {invoice.CustomerName} - ${invoice.QboAmount}";
                    _userActionService.SaveUserAction(user.UserID, UserActionVerb.Upload, UserActionTargetType.Invoice, int.Parse(invoice.Id), details);
                }
            }

            return new JsonNetResult(new
            {
                uploadedInvoices = uploadedInvoices
            });
        }

        public ActionResult History()
        {
            return View();
        }
    }
}