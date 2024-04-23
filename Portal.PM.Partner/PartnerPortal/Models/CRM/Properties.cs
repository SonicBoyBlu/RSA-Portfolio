using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Acme.Models.CRM.Properties
{
    public class PropertyListItem
    {
        public int PropertyID { get; set; }
        public int? PersonID { get; set; }
        public bool IsActive { get; set; }

        public string PropertyName { get; set; }

        public string RentalStatusIcon
        {
            get
            {
                string icon = "<i class='fa fa-{0}'></i>";
                switch (RentalStatusID)
                {
                    case 1: return string.Format(icon, "question-circle text-warning");
                    case 2: return string.Format(icon, "check-circle text-success");
                    case 3: return string.Format(icon, "minus-circle text-danger");
                    case 4: return string.Format(icon, "pause-circle text-danger");
                    case 5: return string.Format(icon, "crosshairs text-info");
                    default: return string.Format(icon, "question-circle text-dark");
                }
            }
        }
        public string PropertyType { get; set; }
        public int PropertyTypeID { get; set; }
        public int RentalStatusID { get; set; }
        public string RentalStatus { get; set; }
        public string UnitCode { get; set; }

        public string Neighborhood { get; set; }
        public string Address1 { get; set; }
    }

    public class Property : ICloneable
    {
        public Property()
        {
            _titlePropertyFeatures = "Features & Amenities";
            _titlePropertyAddress = "Address";
            _titlePropertyLogistics = "Logistics";
        }
        public virtual object Clone() { return this.MemberwiseClone(); }
        //[Editable(false)]
        [ReadOnly(true)]
        public string _titlePropertyFeatures { get; set; }
        [ReadOnly(true)]
        public string _titlePropertyAddress { get; set; }
        [ReadOnly(true)]
        public string _titlePropertyLogistics { get; set; }


        [Key]
        public int? PropertyID { get; set; }
        public int? PersonID { get; set; }
        public string PropertyName { get; set; }
        public string AccountName { get; set; }
        public int PipelineStatusID { get; set; }
        public string RentalStatusIcon { get
            {
                string icon = "<i class='fa fa-{0}'></i>";
                switch (RentalStatusID)
                {
                    case 1: return string.Format(icon, "question-circle text-warning");
                    case 2: return string.Format(icon, "check-circle text-success");
                    case 3: return string.Format(icon, "minus-circle text-danger");
                    case 4: return string.Format(icon, "pause-circle text-danger");
                    case 5: return string.Format(icon, "crosshairs text-info");
                    default: return  string.Format(icon, "question-circle text-dark"); 
                }
            } 
        }
        public int RentalStatusID { get; set; }
        public string RentalStatus { get; set; }
        public string UnitCode { get; set; }
        public string EscapiaPropID { get; set; }
        public string Neighborhood { get; set; }
        public string MiscComplexes { get; set; }

        public string Owner1 { get; set; }
        public string Owner2 { get; set; }
        public string HousePhone { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string PersonalUsage { get; set; }
        public string Incentives { get; set; }
        public string ExpectationsGoals { get; set; }

        public string GeneralNotes { get; set; }

        public string SpecialInstructions1 { get; set; }
        public string SpecialInstructions2 { get; set; }

        public string PropertyType { get; set; }
        public int PropertyTypeID { get; set; }
        public int SquareFootage { get; set; }
        public int NumBeds { get; set; }
        public int NumBaths { get; set; }
        public int NumHalfBaths { get; set; }

        public bool IsPointCentralPaid { get; set; }
        public bool HasPointCentralLock { get; set; }
        public bool HasPointCentralThermostat { get; set; }
        public bool HasGoZoneWifi { get; set; }
        public bool HasNoiseAware { get; set; }
        public bool HasEarthquakeValve { get; set; }
        public string MatterportLink { get; set; }

        public string AlarmCompanyContact { get; set; }
        public string AlarmCode { get; set; }
        public string AlarmPassword { get; set; }

        public string LockBoxCode { get; set; }
        public string GateCode { get; set; }
        public string GarageCode { get; set; }
        public string KeylessDoorCode { get; set; }
        public string MiscCodes { get; set; }

        public string WiFiName_SSID { get; set; }
        public string WiFiPassword { get; set; }

        public string PoolContact { get; set; }
        public string PoolSchedule { get; set; }
        public string LandscapingContact { get; set; }
        public string LandscapingSchedule { get; set; }
        public string Appliances { get; set; }
        public string Handyman { get; set; }
        public string TvInternet { get; set; }
        public string PestContact { get; set; }

        public DateTime? DateApplicationSubmitted { get; set; }
        public DateTime? DateOnBoard { get; set; }
        public DateTime? DateOffBoard { get; set; }
        public DateTime? DatePermitRenewal { get; set; }
        public string RenewalServiceProgram { get; set; }
        public DateTime? DatePcBatteryChange { get; set; }
        public DateTime? DateMinHouseholdItemCheck { get; set; }

        public string CityPropertyID { get; set; }
        public string ActiveCitations { get; set; }
        public string CitationDates { get; set; }

        public bool IsDoNotInspect { get; set; }
        public string InspectorName { get; set; }
        public DateTime? DateLastInspection { get; set; }

        public string Website { get; set; }
        public string SocialFacebook { get; set; }
        public string SocialGooglePlus { get; set; }
        public string SocialInstagram { get; set; }
        public string SocialLinkedIn { get; set; }
        public string SocialTwitter { get; set; }



        public bool HasPool { get; set; }
        public string PoolType { get; set; }
        public string PoolDimensions { get; set; }
        public bool HasSpa { get; set; }
        public string SpaType { get; set; }
        public string SpaDimensions { get; set; }
        public bool HasGarage { get; set; }
        public bool HasFireFeature { get; set; }
        public bool HasOutdoorShower { get; set; }
        public bool HasOutdoorKitchen { get; set; }
        public bool HasCabana { get; set; }
        public bool HasPuttingGreen { get; set; }
        public bool HasWaterFeatures { get; set; }
        public bool HasPingPongTable { get; set; }
        public bool HasPoolTable { get; set; }
        public bool HasCoveredPatio { get; set; }
        public bool HasSystemsVideo { get; set; }
        public string CasitaDetails { get; set; }
        public string OtherAmenities { get; set; }


        public bool IsActive { get; set; }
        public string Tags { get; set; }

        public string AccountOwner { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateLastActivity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string ZohoACCOUNTID { get; set; }
        public string ZohoSMOWNERID { get; set; }
        public string ZohoPARENTACCOUNTID { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
    }
    public class PropertyNote
    {
        public int NoteID { get; set; }
        public int PersonID { get; set; }
        public int PropertyID { get; set; }
        public string Description { get; set; }
        public int EnteredByID { get; set; }
        public int ModifiedByID { get; set; }
        public string EnteredBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }

    }

}