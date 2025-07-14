using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedPotentials")]
    public class PotentialWeekly : _basePotentials
    {
        public DateTime? DateWeeklyAppt { get; set; }
        public string WeeklyNotes { get; set; }
    }
}