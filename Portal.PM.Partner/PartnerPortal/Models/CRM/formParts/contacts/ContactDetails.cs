using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("People")]
    public class ContactDetails : _baseContacts
    {
        public int PartnerContactID { get; set; }
        public bool IsPartnershipProperty { get; set; }
        public string PartnerFirstName { get; set; }
        public string PartnerLastName { get; set; }
        public int ContactSubTypeID { get; set; }
        public string ContactSubType { get; set; }
    }
}