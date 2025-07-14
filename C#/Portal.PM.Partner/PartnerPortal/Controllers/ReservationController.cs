using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Acme.Services;
using Acme.ViewModels;

namespace Acme.Controllers
{
    public class ReservationController : Controller
    {
        private ReservationService _reservationService { get; set; }

        public ReservationController()
        {
            _reservationService = new Services.ReservationService();
        }

        // GET: Reservation
        public ActionResult Index()
        {

            return View();
        }
        

        [HttpPost]
        public async Task<JsonNetResult> FetchReservationsJson(Models.ExternalApplication.Escapia.Post.Data postData)
        {
            var resv = new Services.Vendors.Escapia.SearchReservationSummaries();
            List<ReservationListViewModel> data = await resv.GetReservations(postData);
            return new JsonNetResult(data);
        }

        /* // RSA Testing 05-11-2020
        [HttpGet]
        public async Task<JsonNetResult> FetchReservationsEscapiaJson()
        {
            var escapia = new Services.Vendors.Escapia.SearchReservationSummaries();
            var data = await escapia.GetReservations(new Models.ExternalApplication.Escapia.Post.Data() { });
            return new JsonNetResult(data);
        }
        */
        //*
        [HttpGet]
        public JsonNetResult FetchReservationsEscapiaJson()
        {
            var escapia = new Services.Vendors.Escapia.SearchReservationSummaries();
            Task.Factory.StartNew(() => escapia.getApi());
            ViewModels.Common.MessageResult result = new ViewModels.Common.MessageResult { success = true };
            return new JsonNetResult(result);
        }
        //*/

        [HttpGet]
        public async Task<ActionResult> FetchReservationJson(string reservationNumber)
        {
            var escapia = new Services.Vendors.Escapia.GetReservationByNumber(reservationNumber);
            ReservationListViewModel data = await escapia.Get(reservationNumber);
            
            if (data.IsNull())
                return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            return new JsonNetResult(data);
        }

        [HttpGet]
        public async Task<ActionResult> FetchReservationTypes()
        {
            var initReservationType = new Services.ReservationTypeService();
            var reservationTypes = initReservationType.GetReservationTypes();

            if (reservationTypes.IsNull())
                return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            return new JsonNetResult(reservationTypes);
        }
        



        [HttpPost]
        public void UpdateReservation(ReservationListViewModel postData)
        {
            var resv = new Services.ReservationService();
            var model = resv.InitializePostData(postData);
            resv.Update(model);
        }

        public ActionResult ReservationModal()
        {
            return PartialView();
        }
    }
}