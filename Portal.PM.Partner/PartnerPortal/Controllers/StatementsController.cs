using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class StatementsController : Controller
    {
        // GET: Statements
        public ActionResult Index()
        {
            return View();
        }
    }
}