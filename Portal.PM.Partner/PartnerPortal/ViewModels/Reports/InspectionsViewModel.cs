using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.ViewModels.Reports
{
    public class InspectionsViewModel
    {
        public InspectionsViewModel()
        {
            Summary = new Models.Reports.Productivity.Inspections.InspectionsSummary();
        }
        public Models.Reports.Productivity.Inspections.InspectionsSummary Summary { get; set; }
    }
}