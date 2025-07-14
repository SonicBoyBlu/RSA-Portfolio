using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Reports.Productivity.Inspections
{
    public class InspectionsSummary
    {
        public int Arrivals { get; set; }
        public int ArrivalFails { get; set; }
        public int Departures { get; set; }
        public int DepartureFails { get; set; }
        public int Other { get; set; }
    }
}