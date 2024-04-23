using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class CallsView
    {
        public CallsView()
        {
            Calls = new List<Models.CRM.Calls.Call>();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Calls.Call> Calls { get; set; }
    }
}