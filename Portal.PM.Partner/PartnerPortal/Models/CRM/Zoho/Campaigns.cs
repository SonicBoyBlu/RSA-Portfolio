using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Campaigns
{
    public class Campaign
    {
        public string CampaignID { get; set; }
        public string ParentCampaignID { get; set; }
        public string CampaignOwner { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }

        public decimal ExpectedRevenue { get; set; }
        public decimal BudgetedCost { get; set; }
        public decimal ActualCost { get; set; }

        public int ExpectedResponse { get; set; }
        public int NumSent { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SmOwnerID { get; set; }
    }
}