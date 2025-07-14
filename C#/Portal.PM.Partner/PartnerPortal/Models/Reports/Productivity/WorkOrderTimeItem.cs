using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Reports.Productivity
{
    public class WorkOrderTimeItem
    {
        public string CompletedBy { get; set; }
        public string CompletedByDepartment { get; set; }
        public string CompletedByJobTitle { get; set; }
        public string CompletedByPhotoUrl { get; set; }
        public string WorkOrderType { get; set; }
        public int TotalHours { get; set; }
        public int TotalMinutes { get; set; }
        public int TotalSeconds { get; set; }
        public string TotalTimeBilled { get; set; }
        public double TotalTimeChart { get; set; }
    }
}