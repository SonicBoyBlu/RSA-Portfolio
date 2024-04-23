using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyImportantDates : _baseProperty
    {
        public DateTime? DateApplicationSubmitted { get; set; }
        public DateTime? DateOnBoard { get; set; }
        public DateTime? DateOffBoard { get; set; }
        public DateTime? DatePermitRenewal { get; set; }
        public string RenewalServiceProgram { get; set; }
        public DateTime? DatePcBatteryChange { get; set; }
        public DateTime? DateMinHouseholdItemCheck { get; set; }
    }
}