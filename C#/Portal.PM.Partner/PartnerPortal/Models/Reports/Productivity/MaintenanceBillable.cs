using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Reports.Productivity
{
    public class MaintenanceBillableItem
    {
        public int UserID { get; set; }
        public string TeamMember { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string ProfilePicUrl { get; set; }
        public int WorkOrdersCreatedBy { get; set; }
        public int VendorDispatch { get; set; }
        public int WorkOrdersAssignedTo { get; set; }
        public int WorkOrdersCompletedBy { get; set; }
        public double TotalBillable { get; set; }
        public string TotalHours { get; set; }
    }
}