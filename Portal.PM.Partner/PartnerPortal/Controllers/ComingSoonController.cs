using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class ComingSoonController : Controller
    {
        // GET: ComingSoon
        public ActionResult Index(int PageID)
        {
            var model = Methods.KnowledgeBase.GetKnowledgeBaseItem(PageID);
            return View(model);
        }
    }
}