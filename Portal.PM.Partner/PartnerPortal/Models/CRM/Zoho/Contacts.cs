using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Zoho.Contacts
{    public class Contact
    {
        public string ContactID { get; set; }
        public string AccountName { get; set; }
        public string ContactType { get; set; }
        public string VendorType { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string SmsPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string OtherPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SecondaryEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {  get { return FirstName + " " + LastName; } }
        public DateTime DateOfBirth { get; set; }
        public bool IsEmailOptOut { get; set; }
        public string Tag { get; set; }
        public string ReferralGift { get; set; }
        public string Website { get; set; }
        public string PartnerFirstName { get; set; }
        public string PartnerLastName { get; set; }
        public string PartnerFullName {  get { return PartnerFirstName + " " + PartnerLastName; } }

        public string Contacts
        {
            get
            {
                string name = FullName.Trim();
                name += string.IsNullOrEmpty(PartnerFullName) ? string.Empty : "<br />" + PartnerFullName;
                return name;
            }
        }
        public string Phones
        {
            get
            {
                string phone  = Phone.Trim();
                phone += string.IsNullOrEmpty(phone) ? string.Empty : "<br />" + MobilePhone;
                return phone;
            }
        }


        public bool IsPartnershipProperty { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string VendorName { get; set; }
        public string Department { get; set; }
        public string ReportingTo { get; set; }
        public string ReportsTo { get; set; }
        public DateTime DateLastActive { get; set; }
        public string MailingStreet { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public string MailingCountry { get; set; }
        public string MiscCountryTownCity { get; set; }
        public string MiscCountryPostalCode { get; set; }
        public string SocialTwitter { get; set; }

        public string SocialFacebook { get; set; }

        public string SocialSkype { get; set; }

        public string SocialLinkedIn { get; set; }

        public string SocialGooglePlus { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
        public string ContactOwner { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SmOwnerID { get; set; }
        public string AccountID { get; set; }
        public string VendorID { get; set; }
    }
}