using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.Models.Reports;

namespace Acme.ViewModels.Reports
{
    public class SupplyList
    {
        public SupplyList()
        {
            Items = new List<SuppliesListItem>();
            ItemsByUser = new List<SuppliesListItem>();
            SummaryItemsByUser = new List<SuppliesListItem>();
            ItemsManuallyEntered = new List<SupplyItemManualEntry>();
        }
        public List<SuppliesListItem> Items { get; set; }
        public List<SuppliesListItem> ItemsByUser { get; set; }
        public SupplyTotals SupplyTotals { get; set; }
        public List<SuppliesListItem> SummaryItemsByUser { get; set; }
        public List<SupplyItemManualEntry> ItemsManuallyEntered { get; set; }
    }
}