using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FetchReservationDetail (int TypeID = -1)
        {
            string view;
            switch (TypeID)
            {
                case 0: view = "partials/_reservationGuest"; break;
                case 16: view = "partials/_reservationDetail"; break;
                case 17: view = "partials/_reservationDetail"; break;
                default: view = "partials/_reservationHold"; break;
            }
            return PartialView(view);
        }
    }
}