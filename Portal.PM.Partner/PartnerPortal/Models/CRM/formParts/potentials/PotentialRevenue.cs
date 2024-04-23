using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedPotentials")]
    public class PotentialRevenue : _basePotentials
    {
        public string Offered { get; set; }
        public float ExpectedRevenue { get; set; }
        public float Amount { get; set; }
        public DateTime? DateClosing { get; set; }
    }
}