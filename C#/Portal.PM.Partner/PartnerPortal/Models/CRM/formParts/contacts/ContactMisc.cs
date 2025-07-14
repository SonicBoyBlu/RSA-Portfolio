using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("People")]
    public class ContactMisc : _baseContacts
    {
        public DateTime? DateOfBirth { get; set; }
        public string ReferralGift { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
    }
}