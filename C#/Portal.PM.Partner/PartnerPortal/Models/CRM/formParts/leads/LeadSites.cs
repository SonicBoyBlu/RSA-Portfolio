using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("ExtendedLeads")]
    public class LeadSites : _baseLeads
    {
        public string UrlAirBnB { get; set; }
        public string UrlFlipkey { get; set; }
        public string UrlHomeaway { get; set; }
        public string UrlTripAdvisor { get; set; }
        public string UrlVacationRentals { get; set; }
        public string UrlVRBO { get; set; }
        public string UrlOtherListing { get; set; }
    }
}