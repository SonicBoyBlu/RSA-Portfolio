using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyDetail : _baseProperty
    {
        public string PropertyName { get; set; }
        public string AccountName { get; set; }
        public int PipelineStatusID { get; set; }
        public int RentalStatusID { get; set; }
        public string RentalStatus { get; set; }
        public string UnitCode { get; set; }
        public string EscapiaPropID { get; set; }
        public string Neighborhood { get; set; }
        public string MiscComplexes { get; set; }
    }
}