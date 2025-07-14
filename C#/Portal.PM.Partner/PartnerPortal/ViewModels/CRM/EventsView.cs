using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class EventsView
    {
        public EventsView()
        {
            Events = new List<Models.CRM.Events.Event>();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Events.Event> Events { get; set; }
    }
}