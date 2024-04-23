using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Vendors
{
    public class Vendor
    {
        public string VendorID { get; set; }
        public string VendorType { get; set; }
        public string VendorName { get; set; }
        public string VendorOwner { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CreatedBy { get; set; }
        public string PhoneSecondary { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public string ModifiedBy { get; set; }
        public string Tag { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SmOwnerID { get; set; }
    }
}