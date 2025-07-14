using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedLeads")]
    public class LeadDetails : _baseLeads
    {
        public string PropertyAddress { get; set; }
        public bool IsPartnershipProperty { get; set; }
        public string MultipleDecisionMakers { get; set; }
        public string Lead { get; set; }
        public string LeadSource { get; set; }
        public string LeadStatus { get; set; }
    }
}