using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Ticketing
{
    public class TicketReplyMessage
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public bool ToCustomer { get; set; }
        public bool SendEmail { get; set; }
    }
}