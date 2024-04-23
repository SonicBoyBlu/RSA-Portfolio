using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Ticketing
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public bool Active { get; set; }
        public int ReplyCountIn { get; set; }
        public int ReplyCountOut { get; set; }
        public DateTime LastReplyDate { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public bool IsAgentOwner { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }
    }
}