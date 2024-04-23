using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyFeatures : _baseProperty
    {
        public bool HasPool { get; set; }
        public string PoolType { get; set; }
        public string PoolDimensions { get; set; }
        public bool HasSpa { get; set; }
        public string SpaType { get; set; }
        public string SpaDimensions { get; set; }
        public bool HasGarage { get; set; }
        public bool HasFireFeature { get; set; }
        public bool HasOutdoorShower { get; set; }
        public bool HasOutdoorKitchen { get; set; }
        public bool HasCabana { get; set; }
        public bool HasPuttingGreen { get; set; }
        public bool HasWaterFeatures { get; set; }
        public bool HasPingPongTable { get; set; }
        public bool HasPoolTable { get; set; }
        public bool HasCoveredPatio { get; set; }
        public bool HasSystemsVideo { get; set; }
        public string CasitaDetails { get; set; }
        public string OtherAmenities { get; set; }
    }
}