using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.Models.Breezeway;

namespace Acme.ViewModels
{
    public class WorkOrdersImportViewModel
    {
        public WorkOrdersImportViewModel()
        {
            taskCosts = new List<TaskCost>();
            taskSupplies = new List<TaskSupplies>();
            taskSummaries = new List<TaskSummary>();
        }
        public List<TaskSummary> taskSummaries { get; set; }
        public List<TaskSupplies> taskSupplies { get; set; }
        public List<TaskCost> taskCosts { get; set; }
        public DateTime dateOldest { get; set; }
    }

    public class WorkOrdersPreviewViewModel{
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Query { get; set; }
        public string Type { get; set; }
        public int Filter { get; set; }
        public bool ShowCommitted { get; set; }
        public List<Models.QuickBooks.QboInvoice> Invoices { get; set; }
    }
}