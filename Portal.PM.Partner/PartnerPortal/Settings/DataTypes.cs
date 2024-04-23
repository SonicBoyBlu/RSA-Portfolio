namespace DataTypes
{
    public enum UserType
    {
        All = 0,
        Employee = 1,
        Owner = 2,
        Vendor = 3
    }

    public enum TicketStatus
    {
        Open = 1,
        Waiting = 2,
        Closed = 3,
        ClosedAndLocked = 4
    }
    public enum TicketPriority
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Urgent = 4
    }

    public class CRM
    {
        public enum ContactType
        {
            All = 0,
            Uncategorized = 1,
            Lead = 2 ,
            Potential = 3,
            Owner = 4,
            Vendor = 5,

            AdvertisingMarketing = 6,
            Handyman = 7,
            LocalMerchant = 8,
            OwnerPrimary = 9,
            OwnerRepresentative = 10,
            OwnerSecondary = 11,
            RealEstateRelated = 12,
            Realtors = 13
        }

        public enum PhoneNumberType
        {
            Other = 1,
            Mobile = 2,
            Home = 3,
            Work = 4,
            Fax = 5
        }

        public enum EmailAddressType
        {
            Other = 1,
            Personal = 2,
            Work = 3
        }

        public enum SocialMediaType
        {
            Other = 1,
            Twitter = 2,
            Facebook = 3,
            LinkedIn = 4,
            GooglePlus = 5,
            Skype = 6,
            Pinterest = 7
        }

        public enum NoteType
        {
            Uncategorized = 1,
            Person = 2,
            Property = 3,
            ContactDetails = 4,
            VendorDetails = 5,
            LeadDetails = 6,
            PotentialDetails = 7,
            Admin = 8,
            General = 9,
            System = 10
        }
    }
}