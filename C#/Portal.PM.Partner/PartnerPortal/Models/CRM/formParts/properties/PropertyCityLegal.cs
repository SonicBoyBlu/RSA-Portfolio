using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyCityLegal : _baseProperty
    {
        public string CityPropertyID { get; set; }
        public string ActiveCitations { get; set; }
        public string CitationDates { get; set; }
    }
}