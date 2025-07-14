using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Ticketing
{
    public class IncomingTicket
    {
        public string SenderEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}