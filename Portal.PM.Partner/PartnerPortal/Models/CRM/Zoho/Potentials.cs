using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Potentials
{
    public class Potential
    {
        public string PotentialID { get; set; }
        public string Prediction { get; set; }
        public string PotentialOwner { get; set; }
        public string AccountName { get; set; }
        public string PotentialName { get; set; }
        public string FullName { get; set; }

        public bool IsPartnershipProperty { get; set; }
        public string PartnerFirstName { get; set; }
        public string PartnerLastName { get; set; }
        public string PartnerFullName { get { return PartnerFirstName + " " + PartnerLastName; } }

        public string Contacts
        {
            get
            {
                string name = PotentialName.Trim();
                name += string.IsNullOrEmpty(PartnerFullName) ? string.Empty : "<br />" + PartnerFullName;
                return name;
            }
        }

        public string MaillingAddress { get; set; }
        public string MaillingCity { get; set; }
        public string MaillingState { get; set; }
        public string MaillingZip { get; set; }
        public string Country { get; set; }
        public string MiscCountryTownCity { get; set; }
        public string MiscCountryPostalCode { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Neighborhood { get; set; }

        public string Lead { get; set; }
        public string LeadSource { get; set; }

        public string Tag { get; set; }
        public string UrlVrbo { get; set; }
        public string UrlOtherListing { get; set; }
        public string UrlVacationRentals { get; set; }
        public string UrlHomeAway { get; set; }
        public string UrlTripAdvisor { get; set; }
        public string UrlFlipkey { get; set; }
        public string UrlAirBnB { get; set; }

        public int Probability { get; set; }
        public string Stage { get; set; }
        public string CampaignSource { get; set; }
        public DateTime ClosingDate { get; set; }
        public string ContactName { get; set; }
        //public string ContactType { get; set; }
        public string PropertyType { get; set; }
        public string Type { get; set; }


        public string InteriorBedrooms { get; set; }
        public string InteriorKitchen { get; set; }
        public string InteriorBathrooms { get; set; }
        public string AdditionalInteriorSpaces { get; set; }
        public string ExteriorAdditionalSpaces { get; set; }
        public string ExteriorCurbAppeal { get; set; }
        public string ExteriorPoolArea { get; set; }
        public string MiscComplexes { get; set; }

        public string SpecificNeeds { get; set; }
        public string Offered { get; set; }



        public int SquareFootage { get; set; }
        public bool IsPool { get; set; }
        public bool IsSpa { get; set; }
        public int NumBeds { get; set; }
        public int NumBaths { get; set; }
        public int NumHalfBaths { get; set; }
        //public bool IsCoveredPatio { get; set; }
        public bool IsGarage { get; set; }
        public bool IsFireFeature { get; set; }
        public bool IsCabana { get; set; }
        public bool IsOutdoorKitchen { get; set; }
        public bool IsOutdoorShower { get; set; }
        public bool IsWaterFeatures { get; set; }
        //public bool IsPoolTable { get; set; }
        public bool IsPuttingGreen { get; set; }
        public string Casita { get; set; }
        public bool IsPingPongPoolTable { get; set; }
        public string OtherAmenities { get; set; }

        public DateTime ProjectedLaunchDate { get; set; }
        public string ProjectedLaunchDateDelay { get; set; }

        public string WeeklyNotes { get; set; }
        public string NextSteps { get; set; }
        public DateTime WeeklyApptDate { get; set; }

        public decimal Amount { get; set; }

        public double CurrentAnnualRevenue { get; set; }
        public double GoalRevenue { get; set; }
        public double ExpectedRevenue { get; set; }
        public string CurrentlyRented { get; set; }
        public string CurrentlyRentedNotes { get; set; }


        public string Challenges { get; set; }
        public string Objections { get; set; }
        public string Expectations { get; set; }
        //public string MultipleDecisionMakers { get; set; }
        public string RetirementPlans { get; set; }
        //public string Goals { get; set; }
        public string PersonalUsage { get; set; }
        //public string Incentives { get; set; }

        /*
        public string SocialTwitter { get; set; }

        public string SocialFacebook { get; set; }

        public string SocialSkype { get; set; }

        public string SocialLinkedIn { get; set; }

        public string SocialGooglePlus { get; set; }
        */

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
        public string LeadOwner { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateLastActive { get; set; }
        public string SmOwnerID { get; set; }
        public string AccountID { get; set; }
        public string ContactID { get; set; }
        public string CampaignID { get; set; }
    }
}