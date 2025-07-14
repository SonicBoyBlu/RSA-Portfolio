using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyOwners : _baseProperty
    {
        public string Owner1 { get; set; }
        public string Owner2 { get; set; }
        public string HousePhone { get; set; }

    }
}