using System;

namespace Acme.Models.Reports
{
    public class SuppliesListItem
    {
        public int WorkOrderTotal { get; set; }
        public int UserID { get; set; }
        public string CompletedBy { get; set; }
        public string CompletedByDepartment { get; set; }
        public string CompletedByJobTitle { get; set; }
        public string CompletedByPhotoUrl { get; set; }
        public string Supply { get; set; }
        public int Quantity { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public double TotalBilled { get; set; }
        public bool IsLabor { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEnteredSupply { get; set; }
    }

    public class SupplyTotals
    {
        public double TotalCost { get; set; }
        public double TotalOwnerBillable { get; set; }
        public double TotalOwnerBilled { get; set; }
        public double TotalNonOwnerBillable { get; set; }
        public double TotalNetBilled { get; set; }
        public double TotalGrossBilled { get; set; }
    }

    public class SupplyItemManualEntry
    {
        public int ID { get; set; }
        public string ItemDescription { get; set; }
        public string BillTo { get; set; }
        public string ItemType { get; set; }
        public string Type { get; set; }
        public string CompletedBy { get; set; }
        public DateTime Completed { get; set; }
        public string TimeToComplete { get; set; }
        public int SupplyQuantity { get; set; }
        public double ItemCost { get; set; }
        public double ItemTotal { get; set; }
        public double LineTotal { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsLocked { get; set; }
        public bool IsLabor { get; set; }
    }
}