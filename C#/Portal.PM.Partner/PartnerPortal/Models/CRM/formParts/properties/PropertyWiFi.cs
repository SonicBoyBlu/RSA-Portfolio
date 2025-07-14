using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyWiFi : _baseProperty
    {
        public string WiFiName_SSID { get; set; }
        public string WiFiPassword { get; set; }
    }
}