using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Reports
{
    public class _ReportSearch
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Query { get; set; }
    }
}