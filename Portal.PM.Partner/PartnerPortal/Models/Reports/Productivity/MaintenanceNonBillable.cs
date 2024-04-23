using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Reports.Productivity
{
    public class MaintenanceNonBillableItem
    {
        public int UserID { get; set; }
        public string CompletedBy { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string ProfilePicUrl { get; set; }
        public int GuestServices { get; set; }
        public int GuestDamages { get; set; }
        public int PointCentral { get; set; }
        public int NoiseAware { get; set; }
        public int EventSetup { get; set; }
        public int WarrentyService { get; set; }
        public double TotalCost { get; set; }
        public string TotalHours { get; set; }
    }
}