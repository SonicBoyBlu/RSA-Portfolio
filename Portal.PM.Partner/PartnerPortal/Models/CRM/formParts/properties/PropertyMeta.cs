using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyMeta : _baseProperty
    {
        public bool IsActive { get; set; }
        public string Tags { get; set; }
    }
}