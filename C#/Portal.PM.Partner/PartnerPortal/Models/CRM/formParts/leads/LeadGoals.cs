using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedLeads")]
    public class LeadGoals :  _baseLeads
    {
        public string Objections { get; set; }
        public string Challenges { get; set; }
        public string Expectations { get; set; }
        public float RevenueGoals { get; set; }
        public string Goals { get; set; }
        public int Prediction { get; set; }
        public string RetirementPlans { get; set; }
        public DateTime? DateProjectedLaunch { get; set; }
        public string ProjectedLaunchDateDelay { get; set; }
        public float CurrentAnnualRevenue { get; set; }
        public string CurrentlyRented { get; set; }
        public string CurrentlyRentedNotes { get; set; }

    }
}