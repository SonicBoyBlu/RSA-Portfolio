using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class VendorsView
    {
        public VendorsView()
        {
            Vendors = new List<Models.CRM.Vendors.Vendor>();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Vendors.Vendor> Vendors { get; set; }
    }
}