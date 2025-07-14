using Acme.Models.Breezeway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models
{
    public class WorkOrdersMonthly
    {
        public DateTime MonthYearDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
        public double TotalAmount { get; set; }
        public List<TaskSummary> Invoices { get; set; }
    }
}