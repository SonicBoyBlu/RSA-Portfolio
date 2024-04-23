using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class CampaignsView
    {
        public CampaignsView()
        {
            Campaigns = new List<Models.CRM.Campaigns.Campaign>();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Campaigns.Campaign> Campaigns { get; set; }
    }
}