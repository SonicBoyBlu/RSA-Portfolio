using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using Acme.Models;
using Acme.ViewModels;

namespace Acme.Services.Vendors.Escapia
{
    public class EscapiaMap
    {
        public static Models.ExternalApplication.Escapia.Post.Data InitializePostData()
        {
            Models.ExternalApplication.Escapia.Post.Data result = new Models.ExternalApplication.Escapia.Post.Data();

            result.pageSize = 10000000;
            result.pageNumber = 1;
            result.specification = new Models.ExternalApplication.Escapia.Post.Specification();
            result.specification.pmcid = GlobalSettings.Escapia.PMCID.ToStringOrDefault();
            result.specification.unitNativePMSIDs = new List<string>();
            result.specification.stayDateRangeSearchSpecification = new Models.ExternalApplication.Escapia.Post.StayDateRangeSearchSpecification();
            result.specification.stayDateRangeSearchSpecification.dateRange = new Models.ExternalApplication.Escapia.Post.DateRange();
            result.specification.stayDateRangeSearchSpecification.dateRange = ParsePostDates();
            result.specification.stayDateRangeSearchSpecification.dateRangeSearchMethod = "StartDate";

            return result;
        }
        private static Models.ExternalApplication.Escapia.Post.DateRange ParsePostDates()
        {

            // -----------------------------------
            // Parse date set defaults
            // ALL  26110 records Minutes: 7.9544425666667 Seconds: 57
            // -1 YEAR  6458  records Minutes: 2.11926826666667 Seconds: 7
            // - 6 mo   3845 records Minutes: 1.26016133 Seconds: 15
            DateTime dtStart = DateTime.Now.AddMonths(-6);  //6 mo
            DateTime dtEnd = DateTime.Now.AddYears(5);
            // -----------------------------------


            bool isTest = false;
            if (isTest)
            {
                // -----------------------------------
                // // Parse date set defaults
                dtStart = DateTime.Now.AddDays(-2);
                dtEnd = DateTime.Now.AddDays(2);
                // -----------------------------------
            }

            Models.ExternalApplication.Escapia.Post.DateRange results =
                new Models.ExternalApplication.Escapia.Post.DateRange
                {
                    startDate = dtStart.ToString("yyyy-MM-dd"),
                    endDate = dtEnd.ToString("yyyy-MM-dd")
                };

            return results;
        }
        public static  List<ReservationListViewModel> InitializeViewModelFromSearchReservationSummaries(List<Models.ExternalApplication.Escapia.APIReturn.SearchReservationSummaries> reservations)
        {
            var initMarketingCategories = new Services.ReservationSourceService();
            List<ViewModels.Common.IdTitlePair> marketingCategories = initMarketingCategories.GetMarketingCategories();

            var initReservationType = new Services.ReservationTypeService();
            var reservationTypes = initReservationType.GetReservationTypes();


            List<ReservationListViewModel> list = new List<ReservationListViewModel>();

            foreach (var reservation in reservations)
            {
                ViewModels.ReservationListViewModel model = new ReservationListViewModel();

                // model.MetaID = reservation.metaid;
                model.ReservationNumber = reservation.reservationNumber;
                model.FirstName = reservation.primaryGuest.firstName;
                model.LastName = reservation.primaryGuest.lastName;
                model.PaymentStatus = reservation.paymentStatus;
                model.DateStart = reservation.stayDateRange.startDate.ToDateTimeOrDefault();
                model.DateEnd = reservation.stayDateRange.endDate.ToDateTimeOrDefault();
                model.PaymentStatusFull = (reservation.paymentStatus == "FullyPaid");

                model.BookingSource = FillBookingSource(reservation.marketingCategoryNativePMSID,marketingCategories);
                model.Status = FillStatus(model.DateStart, model.DateEnd).ToStringOrDefault().Trim().ToLower();

                int reservationTypeNativePMSID = reservation.reservationTypeNativePMSID.ToInt32OrDefault();
                model.ReservationType = (from t in reservationTypes where t.ID == reservationTypeNativePMSID select t.Name).FirstOrDefault();
                model.ReservationTypeInitial = (from t in reservationTypes where t.ID == reservationTypeNativePMSID select t.Initial).FirstOrDefault();


                var initRepository = new Data.Repositories.ReservationRepository();
                Reservation result = new Reservation();
                
                result = initRepository.GetReservation(model.ReservationNumber);
                result = (result.IsNull() ? new Reservation() : result);

                model.Reservation = result;
                model.UploadGuestIdentification = result.UploadGuestIdentification;
                model.HasConcierge = result.ConciergeNotified.HasValue;

                model.IsPSCityContract = result.PSCitySummaryIdNumber.IsNotNullOrEmpty();
                model.SentArrivalInstructions = result.GuestArrivalInstructionsSent.HasValue;

                if (result.IsNotNull())
                {
                    string strConciergeNotified = (result.ConciergeNotified.HasValue)
                        ? result.ConciergeNotified.Value.ToString("MM/dd/yyyy")
                        : string.Empty;

                    string strGuestArrivalInstructionsSentString = (result.GuestArrivalInstructionsSent.HasValue)
                        ? result.GuestArrivalInstructionsSent.Value.ToString("MM/dd/yyyy")
                        : string.Empty;

                    model.Reservation.ConciergeNotifiedString = strConciergeNotified;
                    model.Reservation.GuestArrivalInstructionsSentString = strGuestArrivalInstructionsSentString;
                }
                
                list.Add(model);
            }

            return list;
        }
        public static ReservationListViewModel InitializeViewModelFromDB(Models.Reservation reservation, ReservationTypeViewModel reservationType)
        {
            if (reservation.IsNull())
                return null;

            ViewModels.ReservationListViewModel viewModel = new ReservationListViewModel();
            viewModel.ReservationNumber = reservation.ReservationNumber;
            viewModel.FirstName = reservation.FirstName;
            viewModel.LastName = reservation.LastName;
            viewModel.DateStart = reservation.DateStart.ToDateTimeOrDefault();
            viewModel.DateEnd = reservation.DateEnd.ToDateTimeOrDefault();
            viewModel.PaymentStatusFull = reservation.PaymentStatusFull;

            viewModel.BookingSource = reservation.BookingSource;
            viewModel.Status = FillStatus(viewModel.DateStart, viewModel.DateEnd).ToStringOrDefault().Trim().ToLower();
            viewModel.ReservationCreatedAt = reservation.ReservationCreatedAt;

            if (reservationType.IsNotNull())
            {
                viewModel.ReservationType = reservationType.Name;
                viewModel.ReservationTypeInitial = reservationType.Initial;
            }

            viewModel = FillOptionValues(reservation, viewModel);

            return viewModel;
        }
        public static List<ReservationListViewModel> InitializeViewModelFromDB(List<Models.Reservation> data)
        {
            List<ReservationListViewModel> list = new List<ReservationListViewModel>();

            var initReservationType = new Services.ReservationTypeService();
            var reservationTypes = initReservationType.GetReservationTypes();

            foreach (var reservation in data)
            {
                var reservType = reservation.ReservationType.ToStringOrDefault().ToLower();

                ReservationTypeViewModel reservationType = (from t in reservationTypes where t.Name.ToLower() == reservType select t).FirstOrDefault();

                ViewModels.ReservationListViewModel viewModel = InitializeViewModelFromDB(reservation, reservationType);

                if(viewModel.IsNotNull())
                    list.Add(viewModel);
            }

            return list;
        }
        public static string FillStatus(DateTime dateStart, DateTime dateEnd)
        {
            // arriving , inhome , departed , all

            DateTime today = GlobalUtilities.GetDateZeroTime(DateTime.Now);
            
            string status = "all";

            if (dateStart >= today)
                return "arriving";

            if (dateStart <= today && dateEnd > today)
                return "inhome";

            if (dateEnd <= today)
                return "departed";

            return status;

        }
        private static string FillBookingSource(string marketingCategoryNativePMSID, List<ViewModels.Common.IdTitlePair> marketingCategories)
        {
            return (
                from mc in marketingCategories
                where mc.Id == marketingCategoryNativePMSID.ToInt32OrDefault()
                select mc.Title
            ).FirstOrDefault();
        }
        public static ViewModels.ReservationListViewModel FillOptionValues(Models.Reservation data,ReservationListViewModel viewModel)
        {

            viewModel.Reservation = data;
            viewModel.UploadGuestIdentification = data.UploadGuestIdentification;
            viewModel.HasConcierge = data.ConciergeNotified.HasValue;
            viewModel.IsPSCityContract = data.PSCitySummaryIdNumber.IsNotNullOrEmpty();
            viewModel.SentArrivalInstructions = data.GuestArrivalInstructionsSent.HasValue;
            
            string strConciergeNotified = (data.ConciergeNotified.HasValue)
                ? data.ConciergeNotified.Value.ToString("MM/dd/yyyy")
                : string.Empty;

            string strGuestArrivalInstructionsSentString = (data.GuestArrivalInstructionsSent.HasValue)
                ? data.GuestArrivalInstructionsSent.Value.ToString("MM/dd/yyyy")
                : string.Empty;

            viewModel.Reservation.ConciergeNotifiedString = strConciergeNotified;
            viewModel.Reservation.GuestArrivalInstructionsSentString = strGuestArrivalInstructionsSentString;
            
            return viewModel;
        }
        public static ReservationListViewModel InitializeViewModelFromSearchReservationDetail(Models.ExternalApplication.Escapia.APIReturn.ReservationDetail reservation)
        {
            var initMarketingCategories = new Services.ReservationSourceService();
            List<ViewModels.Common.IdTitlePair> marketingCategories = initMarketingCategories.GetMarketingCategories();

            var initReservationType = new Services.ReservationTypeService();
            var reservationTypes = initReservationType.GetReservationTypes();


            ViewModels.ReservationListViewModel model = new ReservationListViewModel();

            // model.MetaID = reservation.metaid;
            model.ReservationNumber = reservation.reservationNumber;
            model.FirstName = reservation.guests.FirstOrDefault().firstName;
            model.LastName = reservation.guests.FirstOrDefault().lastName;
            model.PaymentStatus = reservation.paymentStatus;
            model.DateStart = reservation.stayDateRange.startDate.ToDateTimeOrDefault();
            model.DateEnd = reservation.stayDateRange.endDate.ToDateTimeOrDefault();
            model.PaymentStatusFull = (reservation.paymentStatus == "FullyPaid");
            model.BookingSource = FillBookingSource(reservation.marketingCategoryNativePMSID, marketingCategories);
            model.Status = FillStatus(model.DateStart, model.DateEnd).ToStringOrDefault().Trim().ToLower();

            int reservationTypeNativePMSID = reservation.reservationTypeNativePMSID.ToInt32OrDefault();
            model.ReservationType = (from t in reservationTypes where t.ID == reservationTypeNativePMSID select t.Name).FirstOrDefault();
            model.ReservationType = (from t in reservationTypes where t.ID == reservationTypeNativePMSID select t.Initial).FirstOrDefault();

            var initRepository = new Data.Repositories.ReservationRepository();
            Reservation result = new Reservation();

            result = initRepository.GetReservation(model.ReservationNumber);
            result = (result.IsNull() ? new Reservation() : result);

            model.Reservation = result;
            model.UploadGuestIdentification = result.UploadGuestIdentification;
            model.HasConcierge = result.ConciergeNotified.HasValue;

            model.IsPSCityContract = result.PSCitySummaryIdNumber.IsNotNullOrEmpty();
            model.SentArrivalInstructions = result.GuestArrivalInstructionsSent.HasValue;

            model.ReservationCreatedAt = reservation.createdAt.ToDateTimeOrDefault();


            if (result.IsNotNull())
            {
                string strConciergeNotified = (result.ConciergeNotified.HasValue)
                    ? result.ConciergeNotified.Value.ToString("MM/dd/yyyy")
                    : string.Empty;

                string strGuestArrivalInstructionsSentString = (result.GuestArrivalInstructionsSent.HasValue)
                    ? result.GuestArrivalInstructionsSent.Value.ToString("MM/dd/yyyy")
                    : string.Empty;

                model.Reservation.ConciergeNotifiedString = strConciergeNotified;
                model.Reservation.GuestArrivalInstructionsSentString = strGuestArrivalInstructionsSentString;


            }

            return model;

        }
        public static Models.ExternalApplication.Escapia.PostSearchUser InitializeSearchUserPostData(string lastname, string firstname, string emailAddress)
        {
            Models.ExternalApplication.Escapia.PostSearchUser result = new Models.ExternalApplication.Escapia.PostSearchUser();
            Models.ExternalApplication.Escapia.PostSearchUserSpecification specification = new Models.ExternalApplication.Escapia.PostSearchUserSpecification();
            specification.lastName = lastname.ToStringOrDefault().Trim();
            specification.firstName = firstname.ToStringOrDefault().Trim();
            specification.emailAddress = emailAddress.ToStringOrDefault().Trim().ToLower();

            result.pageSize = 10000000;
            result.pageNumber = 1;
            result.Specification = specification;

            return result;
        }
    }
}