using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.ViewModels.Reports
{
    public class PcTechsViewModel
    {
        public PcTechsViewModel()
        {
            NonBillableItems = new List<Models.Reports.Productivity.MaintenanceNonBillableItem>();
            BillableItems = new List<Models.Reports.Productivity.MaintenanceBillableItem>();
            SummaryItemsByUser = new List<Models.Reports.SuppliesListItem>();

            TotalTimeBillable = new List<Models.Reports.Productivity.WorkOrderTimeItem>();
            TimeBillable = new List<Models.Reports.Productivity.WorkOrderTimeItem>();
            TimeNonBillable = new List<Models.Reports.Productivity.WorkOrderTimeItem>();
            TimeGuestServices = new List<Models.Reports.Productivity.WorkOrderTimeItem>();
        }
        public string Message { get; set; }
        public List<Models.Reports.Productivity.MaintenanceNonBillableItem> NonBillableItems { get; set; }

        public List<Models.Reports.Productivity.MaintenanceBillableItem> BillableItems { get; set; }
        public List<Models.Reports.SuppliesListItem> SummaryItemsByUser { get;  set; }

        public List<Models.Reports.Productivity.WorkOrderTimeItem> TotalTimeBillable { get; set; }
        public List<Models.Reports.Productivity.WorkOrderTimeItem> TimeBillable { get; set; }
        public List<Models.Reports.Productivity.WorkOrderTimeItem> TimeNonBillable { get; set; }
        public List<Models.Reports.Productivity.WorkOrderTimeItem> TimeGuestServices { get; set; }
    }
}