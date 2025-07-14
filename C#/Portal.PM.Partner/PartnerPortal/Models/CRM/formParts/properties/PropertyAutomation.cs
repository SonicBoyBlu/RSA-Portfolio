using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyAutomation : _baseProperty
    {
        public bool IsPointCentralPaid { get; set; }
        public bool HasPointCentralLock { get; set; }
        public bool HasPointCentralThermostat { get; set; }
        public bool HasGoZoneWifi { get; set; }
        public bool HasNoiseAware { get; set; }
        public bool HasEarthquakeValve { get; set; }
        public string MatterportLink { get; set; }
    }
}