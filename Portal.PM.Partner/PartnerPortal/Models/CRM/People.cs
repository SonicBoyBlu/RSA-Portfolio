using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM.Contacts
{
    public class ContactListItem
    {
        public int PersonID { get; set; }
        public int? PropertyID { get; set; }
        public int? VendorID { get; set; }
        public int? LeadID { get; set; }
        public int? PotentialID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PartnerFirstName { get; set; }
        public string PartnerLastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("<span ng-class='rsa-test'>{0} {1}</span>", FirstName, LastName);
            }
        }
        public string PartnerFullName
        {
            get
            {
                return PartnerFirstName + " " + PartnerLastName;
            }
        }
        public int ContactTypeID { get; set; }
        public string ContactType { get; set; }
        public int ContactSubTypeID { get; set; }
        public string ContactSubType { get; set; }

        public string Lead { get; set; }
        public string LeadSource { get; set; }
        public string LeadType { get; set; }

        public int VendorTypeID { get; set; }
        public string VendorType { get; set; }
        public string Company { get; set; }

        public int PropertyTypeID { get; set; }
        public string PropertyType { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public string UrlPhoneNumber
        {
            get
            {
                return string.Format("<a href='tel:{0}'>{0}</a>", PhoneNumber);
            }
        }
        public string EmailAddress { get; set; }
        public string EmailType { get; set; }
        public string UrlEmailAddress
        {
            get
            {
                return string.Format("<a href='mailto:{0}'>{0}</a>", EmailAddress);
            }
        }

        public string Website { get; set; }
        public string UrlWebsite
        {
            get
            {
                return string.Format("<a href='{0}' target='_blank'>{0}</a>", Website);
            }
        }

        public string Category { get; set; }
        public string AccountName { get; set; }
        public string PropertyAddress { get; set; }
        public string Stage { get; set; }
        public string Goals { get; set; }
        public DateTime DateLastActive { get; set; }
        public double ExpectedRevenue { get; set; }
    }


    [Table("People")]
    public class Contact
    {
        public Contact()
        {
            EmailAddresses = new List<Details.EmailAddress>();
            PhoneNumbers = new List<Details.PhoneNumber>();
            SocialMedias = new List<Details.SocialMedia>();
            LeadDetail = new List<Details.LeadDetail>();
            PotentialDetail = new List<Details.PotentialDetail>();
            VendorDetail = new List<Details.VendorDetail>();
            Notes = new List<Details.Note>();
            Properties = new List<Properties.Property>();
        }

        public int PersonID { get; set; }
        //public DataTypes.CRM.ContactType ContactTypeID { get; set; }
        public int ContactTypeID { get; set; }
        public string ContactTypeName { get { return ((DataTypes.CRM.ContactType)ContactTypeID).ToString(); } }
        public DataTypes.CRM.ContactType ContactType { get { return (DataTypes.CRM.ContactType)ContactTypeID; } }
        public int ContactSubTypeID { get; set; }
        public string ContactSubType { get; set; }
        public int PipelineStageID { get; set; }
        public string PipelineStage { get; set; }

        public int VendorTypeID { get; set; }
        public string VendorType { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string FullName { get { return string.Format("<span ng-class='rsa-test'>{2}{0} {1}</span>", FirstName, LastName, string.IsNullOrEmpty(Salutation) ? "" : Salutation + " "); } }

        public string PartnerFirstName { get; set; }
        public string PartnerLastName { get; set; }
        public string PartnerFullName { get { return string.Format("{0} {1}", PartnerFirstName, PartnerLastName); } }

        public bool IsPartnershipProperty { get; set; }
        public int PartnerContactID { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string ReferralGift { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }

        public List<Details.EmailAddress> EmailAddresses { get; set; }
        public List<Details.PhoneNumber> PhoneNumbers { get; set; }
        public List<Details.SocialMedia> SocialMedias { get; set; }
        public List<Details.LeadDetail> LeadDetail { get; set; }
        public List<Details.PotentialDetail> PotentialDetail { get; set; }
        public List<Details.VendorDetail> VendorDetail { get; set; }
        public List<Details.Note> Notes { get; set; }
        public List<Properties.Property> Properties { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateLastActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ZohoContactID { get; set; }
        public string ZohoAccountID { get; set; }
        public string ZohoLeadID { get; set; }
        public string ZohoPotentialID { get; set; }
        public string ZohoVendorID { get; set; }
        public string ZohoSmOwnerID { get; set; }
    }

    public class Details
    {
        public class EmailAddress
        {
            public int ID { get; set; }
            public string Address { get; set; }
            public int EmailTypeID { get; set; }
            public string EmailType { get; set; }
            public string Label { get; set; }
            public bool IsPrimary { get; set; }
            public string UrlEmailLink { get { return string.Format("<a href='mailto:{0}'>{0}</a>", Address); } }
        }

        public class PhoneNumber
        {
            public int ID { get; set; }
            public int PersonID { get; set; }
            [Column("PhoneNumber")]
            public string Number { get; set; }
            public int PhoneNumberTypeID { get; set; }
            public string PhoneNumberType { get; set; }
            public string Label { get; set; }
            public bool IsPrimary { get; set; }
            public string UrlPhoneLink { get { return string.Format("<a href='tel:{0}'>{0}</a>", Number); } }
        }

        public class SocialMedia
        {
            public int ID { get; set; }
            public int SocialMediaTypeID { get; set; }
            public string SocialMediaType { get; set; }
            public string UserName { get; set; }
        }

        public class LeadDetail
        {
            public int? ExtendedLeadID { get; set; }
            public int PersonID { get; set; }
            public string Objections { get; set; }
            public string Challenges { get; set; }
            public string Expectations { get; set; }
            public float RevenueGoals { get; set; }
            public string Goals { get; set; }
            public int Prediction { get; set; }
            public string RetirementPlans { get; set; }
            public DateTime? DateProjectedLaunch { get; set; }
            public string ProjectedLaunchDateDelay { get; set; }
            public string PropertyAddress { get; set; }
            public bool IsPartnershipProperty { get; set; }
            public string MultipleDecisionMakers { get; set; }
            public float CurrentAnnualRevenue { get; set; }
            public string CurrentlyRented { get; set; }
            public string CurrentlyRentedNotes { get; set; }
            public string UrlAirBnB { get; set; }
            public string UrlFlipkey { get; set; }
            public string UrlHomeaway { get; set; }
            public string UrlTripAdvisor { get; set; }
            public string UrlVacationRentals { get; set; }
            public string UrlVRBO { get; set; }
            public string UrlOtherListing { get; set; }
            public string Lead { get; set; }
            public string LeadSource { get; set; }
            public string LeadStatus { get; set; }
            public string LeadOwner { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public string ModifiedBy { get; set; }
            public DateTime DateModified { get; set; }
            public string ZohoSmOwnerID { get; set; }
            public string ZohoLeadID { get; set; }
        }

        public class PotentialDetail
        {
            public int? ExtendedPotentialID { get; set; }
            public int PropertyID { get; set; }
            public int PersonID { get; set; }
            public string PotentialName { get; set; }
            public string Stage { get; set; }
            public string NextStep { get; set; }
            public string SpecificNeeds { get; set; }
            public string Offered { get; set; }
            public float ExpectedRevenue { get; set; }
            public float Amount { get; set; }
            public DateTime? DateClosing { get; set; }
            public string ExteriorAdditionalSpaces { get; set; }
            public string ExteriorCurbAppeal { get; set; }
            public string ExteriorPoolArea { get; set; }
            public string InteriorBathrooms { get; set; }
            public string InteriorBedrooms { get; set; }
            public string InteriorKitchen { get; set; }
            public string InteriorLivingroom { get; set; }
            public string AdditionalInteriorSpaces { get; set; }
            public DateTime? DateWeeklyAppt { get; set; }
            public string WeeklyNotes { get; set; }
            public string PotentialOwner { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public string ModifiedBy { get; set; }
            public DateTime DateModified { get; set; }
            public string ZohoSmOwnerID { get; set; }
            public string ZohoAccountID { get; set; }
            public string ZohoPotentialID { get; set; }
            public string ZohoContactID { get; set; }
            public string ZohoCampaignID { get; set; }
        }

        public class VendorDetail
        {
            public int? VendorID { get; set; }
            public int PersonID { get; set; }
            public string Website { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public DateTime DateModified { get; set; }
            public bool IsActive { get; set; }
        }

        public class Note
        {
            public int? NoteID { get; set; }
            public int NoteTypeID { get; set; }
            public DataTypes.CRM.NoteType NoteType { get; set; }
            public int? PersonID { get; set; }
            public int? PropertyID { get; set; }
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
}