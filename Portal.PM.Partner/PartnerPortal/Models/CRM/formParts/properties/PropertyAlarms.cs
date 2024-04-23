using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyAlarms : _baseProperty
    {
        public string AlarmCompanyContact { get; set; }
        public string AlarmCode { get; set; }
        public string AlarmPassword { get; set; }
    }
}