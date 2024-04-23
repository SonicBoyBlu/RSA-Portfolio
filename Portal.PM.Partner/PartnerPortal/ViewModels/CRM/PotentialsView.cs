using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class PotentialsView
    {
        public PotentialsView()
        {
            Potentials = new List<Models.CRM.Potentials.Potential>();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Potentials.Potential> Potentials { get; set; }
    }
}