using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Acme.Models.ExternalApplication
{
    public class Escapia
    {
        public class APIReturn
        {
            public class SearchReservationSummaries
            {
                [JsonProperty("$id")] public string metaid { get; set; } //":"2",
                public string reservationNumber { get; set; } //":"RES-18186",
                public int unitNativePMSID { get; set; } //":"133161",
                public int guestCount { get; set; } //":8,
                public string reservationGroup { get; set; } //":null,
                public float rent { get; set; } //":4160.0000,
                public float total { get; set; } //":5628.9800,
                public float paymentTotal { get; set; } //":5628.9800,
                public string holdType { get; set; } //":"None",
                public bool confirmed { get; set; } //":true,
                public string paymentStatus { get; set; } //":"FullyPaid",
                public string depositStatus { get; set; } //":"Paid",
                public string marketingCategoryNativePMSID { get; set; } //":"6979",
                public string reservationTypeNativePMSID { get; set; } //":"0",
                public bool bookedByTravelAgent { get; set; } //":false,
                public bool isLockOff { get; set; } //":false,
                public float netRentToOwner { get; set; } //":4160.0000,
                public int id { get; set; } //":0 NOT USED
                public int pmcid { get; set; } //":"1596",
                public string nativePMSID { get; set; } //":"8528460"
                public StayDateRange stayDateRange { get; set; }
                public PrimaryGuest primaryGuest { get; set; }
                public List<string> OwnerNativePMSIDs { get; set; }
            }

            public class ReservationDetail
            {
                public Diagnosis failureDiagnosis { get; set; }
                public string reservationNumber { get; set; }
                public StayDateRange stayDateRange { get; set; }
                public string unitNativePMSID { get; set; }
                public List<Guest> guests { get; set; }
                public string doorCode { get; set; }
                public int adults { get; set; }
                public int children { get; set; }
                public ReservationGroup reservationGroup { get; set; }
                public float rent { get; set; }
                public float tax { get; set; }
                public List<Discount> discounts { get; set; }
                public float discount { get; set; }
                public float total { get; set; }
                public float paymentTotal { get; set; }
                public float serviceFee { get; set; }
                public List<PaymentScheduleItem> paymentSchedule { get; set; }
                public List<ReservationRate> rates { get; set; }
                public string reservationDetailURL { get; set; }
                public string paymentURL { get; set; }
                public string checkInCheckOutURL { get; set; }
                public string moveURL { get; set; }
                public string chargeDetailURL { get; set; }
                public List<Note> notes { get; set; }
                public string holdType { get; set; }
                public string reservationTypeNativePMSID { get; set; }
                public string pricingPlanNativePMSID { get; set; }
                public string kabaLockCode { get; set; }
                public bool immovable { get; set; }
                public string status { get; set; }
                public string paymentStatus { get; set; }
                public string occupancyStatus { get; set; }
                public string depositStatus { get; set; }
                public string marketingCategoryNativePMSID { get; set; }
                public bool bookedByTravelAgent { get; set; }
                public string createdBy { get; set; }
                public string createdAt { get; set; }
                public bool isLockOff { get; set; }
                public List<PackagePricingSummary> packages { get; set; }
                public List<ExtraPricingSummary> extras { get; set; }
                public List<AddOnPricingSummary> addOns { get; set; }
                public List<AdditionalProperty> additionalProperties { get; set; }
                public string partnerID { get; set; }
                public string partnerName { get; set; }
                public PaymentMethod preferredPaymentMethod { get; set; }
                public string iataNumber { get; set; }
                public string checkInTime { get; set; }
                public string checkOutTime { get; set; }
                public int id { get; set; }
                public string pmcid { get; set; }
                public string nativePMSID { get; set; }
            }

            public class MarketingCategory
            {
                [JsonProperty("$id")] public int metaid { get; set; }
                public string category { get; set; }
                public string correspondingInquirySource { get; set; }
                public List<MarketingSource> marketingSources { get; set; }
                public string pmcid { get; set; }
                public string nativePMSID { get; set; }
            }

            public class MarketingSource
            {
                [JsonProperty("$id")] public int metaid { get; set; }
                public string source { get; set; }
                public string correspondingInquirySource { get; set; }
                public string pmcid { get; set; }
                public string nativePMSID { get; set; }
            }

            public class Unit
            {
                public string unitName { get; set; }
                public string unitCode { get; set; }
                public string unitComplex { get; set; }
                public string unitHeadline { get; set; }
                public Address address { get; set; }
                public string headline { get; set; }
                public double latitude { get; set; }
                public double longitude { get; set; }
                public double bedrooms { get; set; }
                public double bathrooms { get; set; }
                public double sleeps { get; set; }
                public string internalDescription { get; set; }
                public string webDescription { get; set; }
                public string shortDescription { get; set; }
                public string guestGuidance { get; set; }
                public List<FeatureGroup> featureGroups { get; set; }
                public UnitType unitType { get; set; }
                public string thumbnailImageURL { get; set; }
                public bool available { get; set; }
                public string notes { get; set; }
                public string keycode { get; set; }
                public string kabaDoorName { get; set; }
                public string detailURL { get; set; }
                public List<Owner> owners { get; set; }
                public List<ScaledImage> scaledImages { get; set; }
                public bool occupied { get; set; }
                public double proximity { get; set; }
                public string proximityUnitOfMeasure { get; set; }
                public string vacantUntil { get; set; }
                public string occupiedUntil { get; set; }
                public string nextArrival { get; set; }
                public List<Phone> phones { get; set; }
                public string maintenanceNotes { get; set; }
                public string unitVacancyStatus { get; set; }
                public UnitHousekeepingStatusType unitHousekeepingStatusType { get; set; }
                public string instantArea { get; set; }
                public string instantType { get; set; }
                public string instantLocation { get; set; }
                public Location location { get; set; }
                public LocationOption option1 { get; set; }
                public LocationOption option2 { get; set; }
                public LocationOption option3 { get; set; }
                public List<AdditionalProperty> additionalProperties { get; set; }
                public bool isActive { get; set; }
                public string housekeepingNotes { get; set; }
                public int totalComplimentaryNights { get; set; }
                public int availableComplimentaryNights { get; set; }
                public int id { get; set; }
                public string pmcid { get; set; }
                public string nativePMSID { get; set; }
            }

            public class ReservationTypes
            {
                [JsonProperty("$id")] public int metaid { get; set; }
                public string type { get; set; }
                public bool isDefaultStay { get; set; }
                public string ownerStayType { get; set; }
                public int id { get; set; }
                public string pmcid { get; set; }
                public string nativePMSID { get; set; }
            }

            public class SearchOwnersResult
            {
                [JsonProperty("$id")] public int metaid { get; set; }
                [JsonProperty("SearchOwner")] public List<Owner> results { get; set; }
                public int totalCount { get; set; }
                public int pageSize { get; set; }
                public int pageNumber { get; set; }
                public int fromIndex { get; set; }
                public int toIndex { get; set; }

            }
        }

        public class Post
        {
            public class Data
            {
                public Data()
                {
                    specification = new Specification()
                    {
                        stayDateRangeSearchSpecification = new StayDateRangeSearchSpecification()
                        {
                            dateRange = new DateRange()
                            {
                                endDate = DateTime.Now.Date.ToString(),
                                startDate = DateTime.Now.AddDays(-30).Date.ToString()
                            }
                        },
                        pmcid = "1596"
                    };
                    custom = new Custom()
                    {
                        task = "all",
                        searchTerm = string.Empty
                    };
                    pageSize = 1000;
                }
                public Specification specification { get; set; }
                public Custom custom { get; set; }
                public int pageSize { get; set; }
                public int pageNumber { get; set; }
            }

            public class Specification
            {
                public string pmcid { get; set; }
                public List<string> unitNativePMSIDs { get; set; }
                public StayDateRangeSearchSpecification stayDateRangeSearchSpecification { get; set; }
            }

            public class StayDateRangeSearchSpecification
            {
                public DateRange dateRange { get; set; }
                public string dateRangeSearchMethod { get; set; }
            }

            public class DateRange
            {
                public string startDate { get; set; }
                public string endDate { get; set; }
            }

            public class Custom
            {
                public string task { get; set; }
                public string searchTerm { get; set; }
                public string paymentStatus { get; set; }
            }
        }

        public class PostSearchUser
        {
            public PostSearchUserSpecification Specification { get; set; }
            public int pageSize { get; set; }
            public int pageNumber { get; set; }

        }

        public class PostSearchUserSpecification
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailAddress { get; set; }

        }

        public class Owner
        {
            [JsonProperty("$id")] public int metaid { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public List<Phone> phones { get; set; }
            public List<Email> emails { get; set; }
            public List<Address> addresses { get; set; }
            public string notes { get; set; }
            public int id { get; set; }
            public string pmcid { get; set; }
            public string nativePMSID { get; set; }
            public List<OwnedUnit> ownedUnits { get; set; }
            public List<string> ownsUnitNativePMSIDs { get; set; }
            public List<APIReturn.Unit> Unit { get; set; }
        }

        public class Discount
        {
        }

        public class ScaledImage
        {
        }

        public class OwnerOLD
        {
            public int Id { get; set; }
        }

        public class FeatureGroup
        {
        }

        public class Guest
        {
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public List<Address> addresses { get; set; }
            public List<Phone> phones { get; set; }
            public List<Email> emails { get; set; }
            public bool isPrimaryGuest { get; set; }
            public bool isVIP { get; set; }
            public bool isWarn { get; set; }
            public string notes { get; set; }
            public string fullNameLastCommaFirst { get; set; }
            public string fullNameFirstMiddleLast { get; set; }
            public string fullNameFirstLast { get; set; }
            public string detailURL { get; set; }
            public int id { get; set; }
            public string pmcid { get; set; }
            public string nativePMSID { get; set; }
        }

        public class ReservationRate
        {
        }

        public class PaymentScheduleItem
        {
        }

        public class StayDateRange
        {
            [JsonProperty("$id")] public string id { get; set; } //":"2",
            public string startDate { get; set; } //"2019-01-15",
            public string endDate { get; set; } //"2019-01-15",

        }

        public class PrimaryGuest
        {
            [JsonProperty("$id")] public string metaid { get; set; } //": "4",
            public string firstName { get; set; } //": "Debbie",
            public string middleName { get; set; } //": "",
            public string lastName { get; set; } //": "Yarbrough",
            public bool isPrimaryGuest { get; set; } //": false,
            public bool isVIP { get; set; } //": false,
            public bool isWarn { get; set; } //": false,
            public string notes { get; set; } //": "",
            public string fullNameLastCommaFirst { get; set; } //": "Yarbrough, Debbie",
            public string fullNameFirstMiddleLast { get; set; } //": "Debbie  Yarbrough",
            public string fullNameFirstLast { get; set; } //": "Debbie Yarbrough",

            public string detailURL { get; set; } //": null,

            public int id { get; set; } //": 0, NOT USED
            public int pmcid { get; set; } //": "1596",
            public string nativePMSID { get; set; } //": "10618369",
            public Address address { get; set; }
            public List<Phone> phones { get; set; }
            public List<Email> emails { get; set; }

        }

        public class AdditionalProperty
        {
            [JsonProperty("$id")] public string metaid { get; set; }

            //"$id":"126",
            //"name":"Reservations Form",
            //"value":"CITY ID: ABC123\nConcierge: Steve",
            //"id":8630,
            //"pmcid":"1596",
            //"nativePMSID":"8630"
            public string name { get; set; }
            public string value { get; set; }
            public int id { get; set; }
            public string pmcid { get; set; }
            public string nativePMSID { get; set; }

        }

        public class AddOnPricingSummary
        {
        }

        public class ExtraPricingSummary
        {
        }

        public class PackagePricingSummary
        {
        }

        public class Note
        {
            public string text { get; set; }
            public string modifiedOn { get; set; }
            public int id { get; set; }
            public string pmcid { get; set; }
            public string nativePMSID { get; set; }
        }

        public class PaymentMethod
        {
            public string guestNativePMSID { get; set; }
            public string lastModified { get; set; }
            public bool preferred { get; set; }
            public string type { get; set; }
            public CreditCard creditCard { get; set; }
            public ElectronicCheck electronicCheck { get; set; }
            public int id { get; set; }
            public string pmcid { get; set; }
            public string nativePMSID { get; set; }
        }

        public class ElectronicCheck
        {
        }

        public class CreditCard
        {
        }

        public class ReservationGroup
        {
            public string nativePMSID { get; set; }
            public string name { get; set; }
            public string detailURL { get; set; }
        }

        public class Diagnosis
        {
            public string displayMessage { get; set; }
            public string internalMessage { get; set; }
        }

        public class Address
        {
            [JsonProperty("$id")] public string metaid { get; set; } //:"5",
            public string street1 { get; set; } //:"9-B Paseo Del Pajaro, Santa Fe N",
            public string street2 { get; set; } //:"",
            public string city { get; set; } //:"Santa Fe",
            public string state { get; set; } //:"NM",
            public string province { get; set; } //:null,
            public string zip { get; set; } //:"87506",
            public string country { get; set; } //:"US",
            public string addressContactTypeId { get; set; } //:"Standard",
            public string isPrimary { get; set; } //:true,
            public string id { get; set; } //:0,
            public string pmcid { get; set; } //:"1596",
            public string nativePMSID { get; set; } //:"8726539"

        }

        public class Phone
        {
            [JsonProperty("$id")] public string metaid { get; set; } // "6",
            public string countryCode { get; set; } // "1",
            public string areaCode { get; set; } // "806",
            public string number { get; set; } // "2362340",
            public string extension { get; set; } // "",
            public string phoneContactTypeId { get; set; } // "2",
            public string isPrimary { get; set; } // true,
            public string id { get; set; } // 0,
            public string pmcid { get; set; } // "1596",
            public string nativePMSID { get; set; } // "Phone1"

        }

        public class Email
        {
            [JsonProperty("$id")] public string metaid { get; set; } // "6",
            public string address { get; set; } //"flosm@aol.com",
            public string emailContactTypeId { get; set; } //"0",
            public string isPrimary { get; set; } //true,
            public string id { get; set; } //0,
            public string pmcid { get; set; } //"1596",
            public string nativePMSID { get; set; } //"Email"

        }

        public class LocationOption
        {
        }

        public class Location
        {
        }

        public class UnitHousekeepingStatusType
        {
        }

        public class UnitType
        {
        }

        public class OwnedUnit
        {
            [JsonProperty("$id")] public string metaid { get; set; }
            public string nativePMSID { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public string isActive { get; set; }
            public string share { get; set; }
        }
    }
}
