using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Ticketing
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public string DownloadUrl { get; set; }
        public DateTime FileDate { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string Sender { get; set; }
        public int SenderId { get; set; }
        public long TicketMessageId { get; set; }
    }
}