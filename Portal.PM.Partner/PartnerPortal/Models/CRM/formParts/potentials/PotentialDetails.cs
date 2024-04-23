using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedPotentials")]
    public class PotentialDetail : _basePotentials
    {
        public string PotentialName { get; set; }
        public string Stage { get; set; }
        public string NextStep { get; set; }
        public string SpecificNeeds { get; set; }
    }
}