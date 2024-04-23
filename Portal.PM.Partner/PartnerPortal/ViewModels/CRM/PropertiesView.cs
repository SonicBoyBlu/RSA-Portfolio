using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class PropertiesViewViewModel
    {
        public PropertiesViewViewModel()
        {
            Properties = new List<Models.CRM.Properties.PropertyListItem>();
            Lists = new Models.CRM.Lists.TypeAndStatus();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Properties.PropertyListItem> Properties { get; set; }
        public Models.CRM.Lists.TypeAndStatus Lists { get; set; }
    }
}