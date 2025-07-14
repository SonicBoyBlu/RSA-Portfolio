using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyCodes : _baseProperty
    {
        public string LockBoxCode { get; set; }
        public string GateCode { get; set; }
        public string GarageCode { get; set; }
        public string KeylessDoorCode { get; set; }
        public string MiscCodes { get; set; }
    }
}