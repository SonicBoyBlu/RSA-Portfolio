using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Lists
{
    public class TypeAndStatus
    {
        public TypeAndStatus()
        {
            ContactTypes = new List<Types.Contact>();
            ContactSubTypes = new List<Types.ContactSub>();
            EmailTypes = new List<Types.Email>();
            PhoneTypes = new List<Types.Phone>();
            SalutationType = Helpers.Salutations.GetSalutations();
            SocialMediaTypes = new List<Types.SocialMedia>();
            //PipelineTypes = new List<Types.Pipeline>();
            PropertyTypes = new List<Types.Property>();
            VendorTypes = new List<Types.Vendor>();

            PipelineStageTypes = new List<Statuses.PipelineStageType>();
            PropertyStatus = new List<Statuses.Property>();
            Tags = new List<Tags>();
            States = Helpers.States.GetStates();
        }
        public List<Types.Contact> ContactTypes { get; set; }
        public List<Types.ContactSub> ContactSubTypes { get; set; }
        public List<Types.Email> EmailTypes { get; set; }
        public List<Types.Phone> PhoneTypes { get; set; }
        public List<Types.SocialMedia> SocialMediaTypes { get; set; }
        //public List<Types.Pipeline> PipelineTypes { get; set; }
        public List<Types.Property> PropertyTypes { get; set; }
        public List<Types.Vendor> VendorTypes { get; set; }
        public List<Common.Salutation> SalutationType { get; set; }
        public List<Statuses.PipelineStageType> PipelineStageTypes { get; set; }
        public List<Statuses.Property> PropertyStatus { get; set; }
        public List<Tags> Tags { get; set; }
        public List<Common.State> States { get; set; }
    }
}

namespace Acme.Models
{
    public class Tags
    {
        public string Description { get; set; }
        public string Tag { get; set; }
    }
}
namespace Acme.Models.CRM.Types
{
    public class Contact
    {
        public int ContactTypeID { get; set; }
        public string ContactType { get; set; }
        public bool IsActive { get; set; }
    }
    public class ContactSub
    {
        public int ContactSubTypeID { get; set; }
        public string ContactSubType { get; set; }
        public int ContactTypeID { get; set; }
        public bool IsActive { get; set; }
    }

    public class Email
    {
        public int EmailTypeID { get; set; }
        public string EmailType { get; set; }
        public bool IsActive { get; set; }
    }

    public class Phone
    {
        public int PhoneTypeID { get; set; }
        public string PhoneType { get; set; }
        public bool IsActive { get; set; }
    }

    public class SocialMedia
    {
        public int SocialMediaTypeID { get; set; }
        public string SocialMediaType { get; set; }
        public bool IsActive { get; set; }
    }
    /*
    public class PipelineS
    {
        public int PipelineTypeID { get; set; }
        public string PipelineType { get; set; }
        public bool IsActive { get; set; }
    }
    */
    public class Property
    {
        public int PropertyTypeID { get; set; }
        public string PropertyType { get; set; }
        public bool IsActive { get; set; }
    }
    

    public class Vendor
    {
        public int VendorTypeID { get; set; }
        public string VendorType { get; set; }
        public bool IsActive { get; set; }
    }

}

namespace Acme.Models.CRM.Statuses
{
    public class PipelineStageType
    {
        public int PipelineStageID { get; set; }
        public string PipelineStage { get; set; }
        public bool IsActive { get; set; }
    }

    public class Property
    {
        public int PropertyStatusID { get; set; }
        public string PropertyStatus { get; set; }
        public bool IsActive { get; set; }
    }

}