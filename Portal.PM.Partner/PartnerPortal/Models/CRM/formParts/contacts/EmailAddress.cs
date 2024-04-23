using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    public class EmailAddress
    {
        public EmailAddress()
        {
            ID = null;
            IsPrimary = EmailTypeID == (int)DataTypes.CRM.EmailAddressType.Personal;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public int PersonID { get; set; }
        public int EmailTypeID { get; set; }
        [Column("EmailAddress")]
        public string Address { get; set; }
        public string Label { get; set; }
        public bool IsPrimary { get; set; }
    }
}