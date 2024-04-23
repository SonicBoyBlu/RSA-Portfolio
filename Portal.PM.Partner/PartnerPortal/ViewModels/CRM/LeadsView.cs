using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class LeadsView
    {
        public LeadsView()
        {
            Leads = new List<Models.CRM.Leads.Lead>();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Leads.Lead> Leads { get; set; }
    }
}