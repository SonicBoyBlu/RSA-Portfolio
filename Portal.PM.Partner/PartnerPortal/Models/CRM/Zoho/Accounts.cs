using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Zoho.Properties
{
    public class Property
    {
        public Property() {
            RentalStatus = DataTypes.RentalStatus.All;
        }
        public int PropertyID { get; set; }

        public string AccountID { get; set; }
        public string PartentAccountID { get; set; }
        public string EscapiaID { get; set; }
        public DataTypes.RentalStatus RentalStatus { get; set; }
        public int RentalStatusID
        {
            get
            {
                return (int)RentalStatus;
            }
        }
        public string RentalStatusIcon { 
            get {
                string icon = string.Empty;
                switch (RentalStatus)
                {
                    case DataTypes.RentalStatus.Active: icon = "fa-check-circle text-success"; break;
                    case DataTypes.RentalStatus.Inactive: icon = "fa-minus-circle text-danger"; break;
                    case DataTypes.RentalStatus.Unknown: icon = "fa-question-circle text-info"; break;
                    case DataTypes.RentalStatus.Pending: icon = "fa-tasks text-warning"; break;
                    case DataTypes.RentalStatus.Prospect: icon = "fa-flag-checkered text-primary"; break;
                    default: icon = "fa-question-circle text-info"; break;
                }
                return string.Format("<span class='d-none'>{2}</span><i class='fa {0}' title='{1}'></i>", icon, RentalStatus.ToString(), (int)RentalStatus);
            }
        }
        public string AccountName { get; set; }
        public string PropertyName { get; set; }
        public string UnitCode { get; set; }
        public string PropertyType { get; set; }
        public string CityPropertyID { get; set; }


        public string Owner1 { get; set; }
        public string Owner2 { get; set; }
        public string Owners {
            get {
                string o1 = string.IsNullOrEmpty(Owner1) ? "" : Owner1;
                string o2 = string.IsNullOrEmpty(Owner2) ? "" : Owner2;
                string owners = o1;
                owners += !string.IsNullOrEmpty(owners) && !string.IsNullOrEmpty(o2) ? "<br /> " + o2 : o2;

                return owners;
                     //((Owner1 + (string.IsNullOrEmpty(Owner1) ? "" : (string.IsNullOrEmpty(Owner2) ? "" : "(Pri), ")) + Owner2) + string.IsNullOrEmpty(Owner2) ? "" : "(Sec)");
            }
        }





        public string Address
        {
            get
            {
                string addr = Street + (!string.IsNullOrEmpty(Street) && !string.IsNullOrEmpty(City) ? ", " : "") + City;
                return addr;
            }
        }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HousePhone { get; set; }

        public string Neighborhood { get; set; }
        public string MiscComplexes { get; set; }

        public string SpecialInstructions1 { get; set; }
        public string SpecialInstructions2 { get; set; }

        public bool IsPointCentralLock { get; set; }
        public bool IsGoZoneWifi { get; set; }
        public bool IsPointCentralThermostat { get; set; }
        public bool IsNoiseAware { get; set; }

        public string GateCode { get; set; }
        public string WifiNameSSID { get; set; }
        public string WifiPassword { get; set; }
        public string LockBoxCode { get; set; }
        public string AlarmCompanyContact { get; set; }
        public string AlarmCode { get; set; }
        public string AlarmPassword { get; set; }
        public string GarageCode { get; set; }
        public string KeylessDoorCode { get; set; }
        public string MiscCodes { get; set; }


        public DateTime DatePcBatteryChanged { get; set; }
        public DateTime DateMinHouseholdItemCheck { get; set; }
        public DateTime DatePermitRenewal { get; set; }
        public DateTime DateApplicationSubmitted { get; set; }

        public DateTime DateOnBoard { get; set; }
        public DateTime DateOffBoard { get; set; }

        public int ActiveCitations { get; set; }
        public string CitationDates { get; set; }
        public string RenewalServiceProgram { get; set; }

        public string PoolContact { get; set; }
        public string PoolSchedule { get; set; }
        public string LandscapingContact { get; set; }
        public string LandscapingSchedule { get; set; }
        public string PestContact { get; set; }
        public string Handyman { get; set; }
        public string TvInternet { get; set; }
        public string AppliancesName { get; set; }

        public bool IsDoNotInspect { get; set; }
        public DateTime DateLastInspected { get; set; }
        public string InspectorName { get; set; }

        public string MatterPortLink { get; set; }
        public string Website { get; set; }



        public int SquareFootage { get; set; }
        public bool IsPool { get; set; }
        public string PoolType { get; set; }
        public string PoolDimensions { get; set; }
        public bool IsSpa { get; set; }
        public string SpaType { get; set; }
        public string SpaDimensions { get; set; }
        public int NumBeds { get; set; }
        public int NumBaths { get; set; }
        public int NumHalfBaths { get; set; }
        public bool IsCoveredPatio { get; set; }
        public bool IsGarage { get; set; }
        public bool IsFireFeature { get; set; }
        public bool IsCabana { get; set; }
        public bool IsOutdoorKitchen { get; set; }
        public bool IsOutdoorShower { get; set; }
        public bool IsWaterFeatures { get; set; }
        public bool IsPoolTable { get; set; }
        public bool IsPingPongTable { get; set; }
        public bool IsPuttingGreen { get; set; }
        public string Casita { get; set; }
        public string OtherAmenities { get; set; }

        public bool IsEarthquakeValue { get; set; }
        public bool IsPointCentralPaid { get; set; }
        public bool IsSystemVideo { get; set; }



        public string GeneralNotes { get; set; }
        public string ExpectationsGoals { get; set; }
        public string Incentives { get; set; }
        public string PersonalUsage { get; set; }


        public string SocialInstagram { get; set; }

        public string SocialFacebook { get; set; }


        public string Tag { get; set; }




        public string AccountOwner { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateLastActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SmOwnerID { get; set; }
    }

}

namespace DataTypes
{
    #region Enums
    public enum RentalStatus
    {
        All = 0,
        Unknown = 1,
        Active = 2,
        Inactive = 3,
        Pending = 4,
        Prospect = 5
    }
    #endregion
}