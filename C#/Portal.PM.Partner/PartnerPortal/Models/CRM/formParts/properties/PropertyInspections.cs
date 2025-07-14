using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyInspections : _baseProperty
    {
        public bool IsDoNotInspect { get; set; }
        public string InspectorName { get; set; }
        public DateTime? DateLastInspection { get; set; }
    }
}