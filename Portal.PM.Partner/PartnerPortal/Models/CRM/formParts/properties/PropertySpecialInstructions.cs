using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertySpecialInstructions : _baseProperty
    {
        public string SpecialInstructions1 { get; set; }
        public string SpecialInstructions2 { get; set; }
    }
}