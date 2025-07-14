using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FetchBookingReportOverview(ViewModels.BookingReportOverviewViewModel q)
        {
            var model = q;
            var now = DateTime.Now;
            q.DateStart = new DateTime(now.Year, now.Month, 1);
            now = q.DateStart.AddMonths(1).AddDays(-1);
            q.DateEnd = new DateTime(now.Year, now.Month, now.Day);

            return PartialView("partials/_BookingReportOverview", model);
        }
    }
}