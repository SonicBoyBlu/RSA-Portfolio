using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Breezeway
{
    public class BW_Workorders
    {
    }

    [Serializable]
    public class TaskSummary {
        public int SummaryID { get; set; }
        public string Type { get; set; }
        public int ID { get; set; }
        public string Property { get; set; }
        public int PropertyID { get; set; }
        public string UnitCode { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Started { get; set; }
        public DateTime Assigned { get; set; }
        public string AssignedTo { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime Completed { get; set; }
        public string CompletedBy { get; set; }
        public string EstimateTimeToComplete { get; set; }
        public string TimeToComplete { get; set; }
        public double RatePaid { get; set; }
        public string RateType { get; set; }
        public string Description { get; set; }
        public double TotalCost { get; set; }
        public double TotalAmount { get { return TotalCost; } }
        public string CostDescription { get; set; }
        public string BillTo { get; set; }
        public string ReportSummary { get; set; }
        public string RequestedBy { get; set; }
        public bool IsInvoicable { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateQboUploaded { get; set; }
        public int QuickBooksCustomerID { get; set; }
        public string QuickBooksCustomerName { get; set; }
        public int QuickBooksInvoiceID { get; set; }
        public string QuickBooksInvoiceNum { get; set; }
        public double QuickBooksTotalCost { get; set; }
        public DateTime QuickBooksTxnDate { get; set; }
        public bool IsIgnore { get; set; }

        public int UploadedByUserID { get; set; }
        public string UploadedByUserName { get; set; }
    }

    [Serializable]
    public class TaskSupplies
    {
        public int SupplyID { get; set; }
        public string Type { get; set; }
        public int ID { get; set; }
        public string Property { get; set; }
        public string Name { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime Completed { get; set; }
        public string Supply { get; set; }
        public string Description { get; set; }
        public string ReportSummary { get; set; }
        public string LineItemDetail { get { return ReportSummary + " - " + Description; } }
        public int Quantity { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public bool IsTaxable { get; set; }

    }

    [Serializable]
    public class TaskCost
    {
        public int CostID { get; set; }
        public string Type { get; set; }
        public int ID { get; set; }
        public string Property { get; set; }
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime Assigned { get; set; }
        public string AssignedTo { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime Completed { get; set; }
        public string CompletedBy { get; set; }
        public string TimeToComplete { get; set; }
        public string Description { get; set; }
        public string ReportSummary { get; set; }
        public string LineItemDetail { get { return ReportSummary + " - " + Description; } }
        public int Quantity { get; set; }
        public string ItemType { get; set; }
        public string ItemDescription { get; set; }
        public double ItemCost { get; set; }
        public string BillTo { get; set; }
        public double ItemTotal { get; set; }
        public double LineTotal { get; set; }
        public bool IsTaxable { get; set; }

    }
}