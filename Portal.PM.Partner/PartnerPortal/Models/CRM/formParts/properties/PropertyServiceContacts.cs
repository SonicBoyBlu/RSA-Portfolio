using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyServiceContacts : _baseProperty
    {
        public string PoolContact { get; set; }
        public string PoolSchedule { get; set; }
        public string LandscapingContact { get; set; }
        public string LandscapingSchedule { get; set; }
        public string Appliances { get; set; }
        public string Handyman { get; set; }
        public string TvInternet { get; set; }
        public string PestContact { get; set; }
    }
}