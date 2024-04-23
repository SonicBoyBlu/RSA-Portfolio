using Acme.Services.Accounting.QuickBooks;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Acme.Controllers
{
    /// <summary>
    /// Handles callback from QBO authentication.
    /// </summary>
    public class QuickBooksCallbackController : Controller
    {
        //Call back from QuickBooks with token information.
        public async Task<ActionResult> Index()
        {
            //Set the tocken information.
            var service = new QuickBooksService();
            string code = Request.QueryString["code"];
            string realmId = Request.QueryString["realmId"];
            await service.SetToken(realmId, code);

            // Currently only WorkOrders are requiring access to QBO
            return RedirectToAction("Index", "QuickBooks");
        }

    }
}