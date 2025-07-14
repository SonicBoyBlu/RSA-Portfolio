namespace Acme.ViewModels.CRM
{
    public class DashboardViewModel
    {
        public Models.CRM.Dashboard.PipelineTotalsDates DateRanges { get; set; }
        public Models.CRM.Dashboard.PipelineTotals PipelineTotals { get; set; }
        public Models.CRM.Dashboard.PipelineTotals PipelineTotalsCurrent { get; set; }
        public Models.CRM.Dashboard.PipelineTotals PipelineTotalsPrevious { get; set; }
    }
}