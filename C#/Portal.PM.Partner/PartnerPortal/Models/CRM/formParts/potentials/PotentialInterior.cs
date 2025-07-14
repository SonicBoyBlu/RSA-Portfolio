using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedPotentials")]
    public class PotentialInterior : _basePotentials
    {
        public string InteriorBathrooms { get; set; }
        public string InteriorBedrooms { get; set; }
        public string InteriorKitchen { get; set; }
        public string InteriorLivingroom { get; set; }
        public string AdditionalInteriorSpaces { get; set; }
    }
}