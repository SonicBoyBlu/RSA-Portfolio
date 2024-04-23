using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class MessagesController : Controller
    {
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FetchMessages()
        {
            int welcomeid = 10;
            int messageid = 11;
            switch (Identity.Current.UserType)
            {
                case DataTypes.UserType.Employee: welcomeid = 2; break;
            }
            var model = new ViewModels.DashboardViewModel()
            {
                WelcomeMessage = Methods.SystemMessages.GetMessage(welcomeid),
                NewOwnerMessage = Methods.SystemMessages.GetMessage(messageid)
            };
            return PartialView("partials/_messages", model);
        }

    }
}