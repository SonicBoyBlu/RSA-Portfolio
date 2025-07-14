using System;
using System.Collections.Generic;

namespace Acme.Models
{
    public class TicketStatusSummary
    {
        public TicketStatusSummary()
        {
            Statuses = new List<TicketStatusSummaryItem>();
        }
        public List<TicketStatusSummaryItem> Statuses { get; set; }
    }

    public class TicketStatusSummaryItem
    {
        public DataTypes.TicketStatus StatusID { get; set; }
        public string Status { get; set; }
        public int TicketCount { get; set; }
    }

    public class TicketListItem
    {
        public int TicketID { get; set; }
        public DataTypes.TicketPriority Priority { get; set; }
        public DataTypes.TicketStatus Status { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public string Subject { get; set; }
        public bool HasAttachment { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }
        public DateTime DateFollowup { get; set; }
        public DateTime DateLastAction
        {
            get
            {
                DateTime last = DateTime.MinValue;
                if (DateClosed != DateTime.MinValue) last = DateClosed;
                else if (DateFollowup != DateTime.MinValue) last = DateFollowup;
                else last = DateOpened;
                return last;
            }
        }
    }

    public class Tickets : List<Ticket>
    {
    }

    public class Ticket : TicketListItem
    {
        public Ticket()
        {
            Messages = new List<TicketMessage>();
            Attachments = new List<TicketAttachment>();
        }
        public List<TicketMessage> Messages { get; set; }
        public List<TicketAttachment> Attachments { get; set; }
    }

    public class TicketMessage
    {
        public int MessageID { get; set; }
        public bool IsMe { get; set; }
        public int UserID { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
    }

    public class TicketAttachment
    {
        public int AttachmentID { get; set; }
        public bool IsMe { get; set; }
        public int UserID { get; set; }
        public string DisplayName { get; set; }
        public string FilenameOriginal { get; set; }
        public string FilenameOnDisk { get; set; }
        public string FileUrl { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}