using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedPotentials")]
    public class PotentialExterior : _basePotentials
    {
        public string ExteriorAdditionalSpaces { get; set; }
        public string ExteriorCurbAppeal { get; set; }
        public string ExteriorPoolArea { get; set; }
    }
}