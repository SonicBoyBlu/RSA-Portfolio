using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Ticketing
{
    public class TicketCount
    {
        public int ActiveTicketCount { get; set; }
        public int WaitingTicketCount { get; set; }
    }
}