using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    public class PhoneNumber
    {
        public PhoneNumber()
        {
            ID = null;
            IsPrimary = PhoneNumberTypeID == (int)DataTypes.CRM.PhoneNumberType.Mobile;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public int PersonID { get; set; }
        public int PhoneNumberTypeID { get; set; }
        [Column("PhoneNumber")]
        public string Number { get; set; }
        public string Label { get; set; }
        public bool IsPrimary { get; set; }
    }
}