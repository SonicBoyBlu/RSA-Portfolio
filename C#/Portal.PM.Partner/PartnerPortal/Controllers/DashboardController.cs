using Acme.Services.Ticketing;
using Acme.ViewModels;
using System;
using System.Web.Mvc;
using Acme.Services;
using WebMarkupMin.AspNet4.Mvc;

namespace Acme.Controllers
{
    [CompressContent]
    [MinifyHtml]
    public class DashboardController : Controller
    {
        private UserIdentity me
        {
            get { return Identity.Current;  }
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            int welcomeid = 10;
            int messageid = 11;

            switch (Identity.Current.UserType)
            {
                case DataTypes.UserType.Employee: welcomeid = 2; messageid = 12; break; 
            }

            var model = new ViewModels.DashboardViewModel()
            {
                WelcomeMessage = Methods.SystemMessages.GetMessage(welcomeid),
                NewOwnerMessage = Methods.SystemMessages.GetMessage(messageid)
            };

            model.TicketCounts = new TicketCounts();

            try
            {
                ITicketingService ticketingService = new SmarterTrackService();
                var ticketCounts = Identity.Current.UserType == DataTypes.UserType.Employee ?
                    ticketingService.GetEmployeeTicketCount(Identity.Current.Email) :
                    ticketingService.GetTicketCount(Identity.Current.Email);

                model.TicketCounts.TotalWaitingOnAgent = ticketCounts.ActiveTicketCount;
                model.TicketCounts.TotalWaitingOnMe = ticketCounts.WaitingTicketCount;
            }
            catch
            {
            }

            return View(model);
        }

        public ActionResult NextStep()
        {
            int CatID = 3;
            int ParentID = 8;
            switch (me.UserType)
            {
                case DataTypes.UserType.Employee: CatID = 9; ParentID = 2; break;
            }

            var data = Methods.Education.GetLessons(CatID, ParentID);
            var model = data.Lessons;

            return PartialView("cards/NextSteps", model);
        }

        public ActionResult Staff()
        {
            var api = new Services.Vendors.BambooHR.Api();
            var data = api.GetEmployeesTimeOff(DateTime.Now, DateTime.Now.AddDays(7));
            var model = new ViewModels.TeamMembersViewModel();

            return PartialView("cards/Staff", data);
        }

        [JsonErrorHandler]
        public ActionResult GetBadgeCounts()
        {
            BadgeCounts model = new BadgeCounts(); //Methods.Tickets.GetTicketCounts();

            ITicketingService ticketingService = new SmarterTrackService();

            var resv = new ReservationService();

            if(Identity.Current.UserType == DataTypes.UserType.Employee)
            {
                var ticketCounts = ticketingService.GetEmployeeTicketCount(Identity.Current.Email);
                model.ActionItemsCount = ticketCounts.WaitingTicketCount;
                model.TaskCount = ticketingService.GetEmployeeTaskCount(Identity.Current.Email);
                var lastRefreshedOn = resv.GetAPILastRefreshedOn();
                model.ReservationRefreshedOn = GlobalUtilities.ToPrettyDate(lastRefreshedOn);
            }

            return new JsonNetResult(new { Counts = model });
        }
    }
}