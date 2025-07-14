using System;

namespace Acme.ViewModels
{
    public class DashboardViewModel
    {
        public Models.SystemMessage WelcomeMessage { get; set; }
        public Models.SystemMessage NewOwnerMessage { get; set; }
        public TicketCounts TicketCounts { get; set; }
    }

    public class BadgeCounts
    {
        public int ActionItemsCount { get; set; }
        public int TaskCount { get; set; }
        public string ReservationRefreshedOn { get; set; }
    }

    public class TicketCounts
    {
        public int TotalMessages { get; set; }
        public int TotalNew { get; set; }
        public int TotalOpen { get; set; }
        public int TotalClosed { get; set; }
        public int TotalWaiting { get; set; }
        public int TotalWaitingOnMe { get; set; }
        public int TotalWaitingOnAgent { get; set; }
    }
}