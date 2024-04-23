using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyNotes : _baseProperty
    {
        public string GeneralNotes { get; set; }
    }
}