using System.Collections.Generic;

namespace Acme.ViewModels
{
	public class TicketsViewModel
	{
		public TicketsViewModel()
        {
            Summary = new Models.TicketStatusSummary();
            KB = new List<Models.KnowledgeBaseCategoryCount>();
            Tickets = new List<Models.TicketListItem>();
        }
        public Models.TicketStatusSummary Summary { get; set; }
        public List<Models.KnowledgeBaseCategoryCount> KB { get; set; }

        public List<Models.TicketListItem> Tickets { get; set; }
	}
}