using System;

namespace Acme.Models.CRM.Dashboard
{
    public class PipelineTotals
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int TotalProperties { get; set; }
        public int TotalContacts { get; set; }
        public int TotalNonVendors { get; set; }
        public int TotalSalesContacts { get; set; }
        public int TotalUncategorized { get; set; }
        public int TotalLeads { get; set; }
        public int TotalPotentials { get; set; }
        public int TotalOwners { get; set; }
        public int TotalVendors { get; set; }
    }

    public class PipelineTotalsDates
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateLastStart { get; set; }
        public DateTime DateLastEnd { get; set; }
    }
}