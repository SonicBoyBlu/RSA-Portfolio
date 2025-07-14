using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyLogistics : _baseProperty
    {
        public string PropertyType { get; set; }
        public int PropertyTypeID { get; set; }
        public int SquareFootage { get; set; }
        public int NumBeds { get; set; }
        public int NumBaths { get; set; }
        public int NumHalfBaths { get; set; }
    }
}