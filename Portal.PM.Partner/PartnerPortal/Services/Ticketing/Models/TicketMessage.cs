using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Ticketing
{
    public class TicketMessage
    {
        public TicketMessage()
        {
            Attachments = new List<TicketAttachment>();
        }
        public long MessageId { get; set; }
        public string Subject { get; set; }
        public string BodyPlainText { get; set; }
        public string BodyHTML { get; set; }
        public DateTime SentDate { get; set; }
        public int Direction { get; set; }
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public bool IsDraft { get; set; }
        public List<TicketAttachment> Attachments { get; set; }

        public string AvatarUrl { get; set; }
    }
}