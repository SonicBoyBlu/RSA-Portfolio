using System;
using System.Collections.Generic;
using System.Linq;
using Acme.Data.Repositories;
using Acme.Models;
using Acme.Models.ExternalApplication;
using Acme.Services.Vendors.Escapia;
using Acme.ViewModels;

namespace Acme.Services {
    public class ReservationService {
        private ReservationRepository _reservation;
        public ReservationService () {
            _reservation = new ReservationRepository ();
        }
        public Models.Reservation Update (Models.Reservation data) {
            Reservation result = new Reservation ();

            if (data.ReservationNumber.IsNullOrEmpty ())
                return null;

            data.ID = _reservation.GetReservationId (data.ReservationNumber);
            data.Modified = DateTime.Now;

            if (data.ID == 0)
                result = _reservation.Add (data);

            if (data.ID > 0)
                result = _reservation.Update (data);

            if (result.IsNull ()) {
                // Error logging
                return null;
            }

            return result;
        }
        public int UpdateReservationStatus()
        {
            var reservations = _reservation.GetReservations();

            foreach (var data in reservations)
            {
                if (data.ReservationNumber.IsNullOrEmpty())
                    return 0;

                data.ID = _reservation.GetReservationId(data.ReservationNumber);

                if (data.ID == 0)
                    return 0;
                
                data.Status = EscapiaMap.FillStatus(data.DateStart.GetValueOrDefault(), data.DateEnd.GetValueOrDefault()).ToStringOrDefault().Trim().ToLower();

                _reservation.Update(data);
            }

            return reservations.Count();

        }
        public Models.Reservation InitializePostData (ReservationListViewModel data) {

            Models.Reservation model = new Reservation ();
            model.ReservationNumber = data.ReservationNumber.ToStringOrDefault ();

            model.FirstName = data.FirstName.ToStringOrDefault();
            model.LastName = data.LastName.ToStringOrDefault();
            model.DateStart = data.DateStart;
            model.DateEnd = data.DateEnd;
            model.Status = data.Status;
            model.PaymentStatusFull = data.PaymentStatusFull;
            model.BookingSource = data.BookingSource;

            model.InputEscapia = data.Reservation.InputEscapia;
            model.CreateVirtualGuestFolder = data.Reservation.CreateVirtualGuestFolder;
            model.SaveOTAItinerary = data.Reservation.SaveOTAItinerary;
            model.AdjustRateMatchPayout = data.Reservation.AdjustRateMatchPayout;
            model.PrintCreditCardAuth = data.Reservation.PrintCreditCardAuth;
            model.AddTravelAgent = data.Reservation.AddTravelAgent;
            model.ContractSigned = data.Reservation.ContractSigned;
            model.ExcludePayment = data.Reservation.ExcludePayment;
            model.IntakeChannelRequestGuestInfo = data.Reservation.IntakeChannelRequestGuestInfo;
            model.UpdateEscapia = data.Reservation.UpdateEscapia;
            model.UploadGuestIdentification = data.Reservation.UploadGuestIdentification;
            model.AttachCopyPayment = data.Reservation.AddTravelAgent;
            model.SendCopyPaymentAccounting = data.Reservation.SendCopyPaymentAccounting;
            model.PostPayment = data.Reservation.PostPayment;

            model.ConciergeNotified = data.Reservation.ConciergeNotified;
            model.GuestArrivalInstructionsSent = data.Reservation.GuestArrivalInstructionsSent;

            model.ReservationType = data.ReservationType;
            

            // Palm Springs City Contract Summary
            string pscitycontractnumber = data.Reservation.PSCityContractNumber.ToStringOrDefault().Trim();
            model.PSCityContractNumber = (pscitycontractnumber.IsNullOrEmpty() ? null : pscitycontractnumber);

            string pscitysummaryidnumber = data.Reservation.PSCitySummaryIdNumber.ToStringOrDefault().Trim().ToUpper();
            model.PSCitySummaryIdNumber = (pscitysummaryidnumber.IsNullOrEmpty() ? null : pscitysummaryidnumber);

            model.PSCityContractCancellation = data.Reservation.PSCityContractCancellation;
            model.PSCityContractRevision = data.Reservation.PSCityContractRevision;
            model.LongTermRentalAgreement = data.Reservation.LongTermRentalAgreement;
            model.ExcludeAutomaticPayments = data.Reservation.ExcludeAutomaticPayments;
            model.FinalPaymentDueDateAddCalendar = data.Reservation.FinalPaymentDueDateAddCalendar;
            model.SendConfirmationOTA = data.Reservation.SendConfirmationOTA;

            return model;
        }
        public DateTime GetAPILastRefreshedOn()
        {
            return _reservation.GetAPILastRefreshedOn();
        }
        internal int UpdateReservationType()
        {

            List<Reservation> reservations = _reservation.GetReservationsMissingTypes();

            foreach (var data in reservations)
            {
                if (data.ID == 0 || data.ReservationNumber.IsNullOrEmpty())
                    continue;

                var escapia = new Services.Vendors.Escapia.GetReservationByNumber(data.ReservationNumber);
                var foundResvUsingApiRawJson = escapia.GetReservationDetail(data.ReservationNumber).Result;

                if (foundResvUsingApiRawJson.IsNull())
                    continue;
                
                ReservationListViewModel model = EscapiaMap.InitializeViewModelFromSearchReservationDetail(foundResvUsingApiRawJson);

                if (model.IsNull())
                    continue;
                
                data.ReservationType = model.ReservationType;
                _reservation.Update(data);
                
            }
            
            return reservations.Count();

        }
        internal int UpdateReservationCreatedAt()
        {
            List<Reservation> reservations = _reservation.GetReservationsMissingCreatedAt();
            try
            {
                reservations = reservations.OrderByDescending(x => x.DateStart).ToList();

                foreach (var data in reservations)
                {
                    if (data.ID == 0 || data.ReservationNumber.IsNullOrEmpty())
                        continue;

                    var escapia = new Services.Vendors.Escapia.GetReservationByNumber(data.ReservationNumber);
                    var foundResvUsingApiRawJson = escapia.GetReservationDetail(data.ReservationNumber).Result;

                    if (foundResvUsingApiRawJson.IsNull())
                        continue;

                    ReservationListViewModel model =
                        EscapiaMap.InitializeViewModelFromSearchReservationDetail(foundResvUsingApiRawJson);

                    if (model.IsNull())
                        continue;

                    if (!model.ReservationCreatedAt.IsValidDateTime())
                        continue;

                    data.ReservationCreatedAt = model.ReservationCreatedAt;
                    _reservation.Update(data);

                }
            } catch(Exception ex) { }
            return reservations.Count();

        }
    }



    public class ReservationCategoryService {
        public ReservationCategoryService () {

        }

        public Models.ReservationCategory InitializeDBModel (Models.ExternalApplication.Escapia.APIReturn.MarketingCategory apiMarketingCategory)
        {
            var result = new ReservationCategory();

            if (apiMarketingCategory.IsNull() || apiMarketingCategory.category.IsNullOrEmpty())
                return null;

            //result.ID = apiMarketingCategory.ID;
            result.Name = apiMarketingCategory.category.ToStringOrDefault();
            result.NativePMSID = apiMarketingCategory.nativePMSID.ToInt32OrDefault();

            string strCorrespondingInquirySource = apiMarketingCategory.correspondingInquirySource.ToStringOrDefault();
            result.CorrespondingInquirySource = (strCorrespondingInquirySource.IsNullOrEmpty() ? null : strCorrespondingInquirySource);

            result.Modified = DateTime.Now;

            return result;
        }
    }

    public class ReservationSourceService
    {
        public ReservationSourceService()
        {

        }

        public Models.ReservationSource InitializeDBModel(int reservationCategoryID, Models.ExternalApplication.Escapia.APIReturn.MarketingSource apiMarketingSource)
        {
            var result = new ReservationSource();

            if ( reservationCategoryID == 0 || apiMarketingSource.IsNull() || apiMarketingSource.source.IsNullOrEmpty())
                return null;

            //result.ID = apiMarketingCategory.ID;
            result.ReservationCategoryID = reservationCategoryID;
            result.Name = apiMarketingSource.source.ToStringOrDefault();
            result.NativePMSID = apiMarketingSource.nativePMSID.ToInt32OrDefault();

            string strCorrespondingInquirySource = apiMarketingSource.correspondingInquirySource.ToStringOrDefault();
            result.CorrespondingInquirySource = (strCorrespondingInquirySource.IsNullOrEmpty() ? null : strCorrespondingInquirySource);

            result.Modified = DateTime.Now;

            return result;
        }

        public List<ViewModels.Common.IdTitlePair> GetMarketingCategories()
        {
            var results = new List<ViewModels.Common.IdTitlePair>();

            var initResvRepo = new Data.Repositories.ReservationCategoryRepository();
            List<ReservationCategory> categories = initResvRepo.GetAll();

            foreach (var category in categories)
            {
                results.Add(
                    new ViewModels.Common.IdTitlePair
                    {
                        Id = category.NativePMSID,
                        Title = category.Name
                    }
                );
            }

            return results;
        }
    }

    public class ReservationTypeService
    {
        public ReservationTypeService()
        {

        }

        public Models.ReservationType InitializeDBModel(Models.ExternalApplication.Escapia.APIReturn.ReservationTypes apiReservationType)
        {
            var result = new ReservationType();

            if (apiReservationType.IsNull() || apiReservationType.ownerStayType.IsNullOrEmpty())
                return null;

            //result.ID = apiMarketingCategory.ID;
            result.Name = apiReservationType.type.ToStringOrDefault();
            result.OwnerStayType = apiReservationType.ownerStayType.ToStringOrDefault();
            result.NativePMSID = apiReservationType.nativePMSID.ToInt32OrDefault();
            result.Modified = DateTime.Now;

            return result;
        }

        public List<ViewModels.ReservationTypeViewModel> GetReservationTypes()
        {
            var results = new List<ViewModels.ReservationTypeViewModel>();

            var initReserType = new Data.Repositories.ReservationTypeRepository();
            List<Models.ReservationType> types = initReserType.GetAll();

            foreach (var item in types)
            {
                results.Add( new ViewModels.ReservationTypeViewModel()
                    {
                        ID = item.NativePMSID,
                        Name = item.Name,
                        Initial = item.Initial
                    }
                );
            }

            return results;
        }
    }
}
