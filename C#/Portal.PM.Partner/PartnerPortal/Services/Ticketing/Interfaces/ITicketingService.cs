using System.Collections.Generic;

namespace Acme.Services.Ticketing
{
    public interface ITicketingService
    {
        TicketCount GetTicketCount(string userId);
        TicketCount GetEmployeeTicketCount(string userId);
        int GetEmployeeTaskCount(string userId);
        bool Ping();
        List<Ticket> GetOpenTickets(string userId);
        List<Ticket> GetClosedTickets(string userId);
        Ticket GetTicket(int ticketId);
        List<TicketMessage> GetTicketMessages(int ticketId);

        List<Ticket> GetEmployeeOpenTickets(string userId);
        void SendReply(TicketReplyMessage reply);
        TicketAttachment AddAttachment(string ticketNumber, int ticketMessageId, string fileName, byte[] data, int fileLength);
        List<TicketAttachment> GetTicketAttachments(int ticketId);
        Ticket CreateTicket(string senderEmail, string senderDisplayName, IncomingTicket ticket);
    }
}