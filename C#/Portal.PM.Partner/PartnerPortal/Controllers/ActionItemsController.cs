using Acme.Methods;
using Acme.Services.Ticketing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class ActionItemsController : BaseController
    {
        ITicketingService _ticketingService;
        UserService _userService;

        public ActionItemsController()
        {
            _ticketingService = new SmarterTrackService();
            _userService = new UserService();

        }

        public ActionResult Index()
        {
            return View();
        }

        [JsonErrorHandler(ErrorMessage = "An error occured on the server, failed to get action items.")]
        public ActionResult GetActionItems()
        {
            var tickets = Identity.Current.UserType == DataTypes.UserType.Employee ?
                _ticketingService.GetEmployeeOpenTickets(Identity.Current.Email) :
                _ticketingService.GetOpenTickets(Identity.Current.Email);

            if(Identity.Current.UserType == DataTypes.UserType.Owner)
            {
                tickets.AddRange(_ticketingService.GetClosedTickets(Identity.Current.Email));
            }

            return new JsonNetResult(new { ActionItems = tickets.OrderByDescending(x => x.LastReplyDate).ToList() });
        }

        [JsonErrorHandler(ErrorMessage = "An error occured on the server, failed to get action item messages.")]
        public ActionResult GetActionItemMessages(int ticketId)
        {
            //TBD: Add check to make sure current user has access to ticket.
            var messages = _ticketingService.GetTicketMessages(ticketId);

            //Replace any owner emails with their names.
            List<string> userEmails = messages.Where(m => m.Direction != 1).Select(m => m.Sender).Distinct().ToList();
            var users = _userService.GetUsersFromEmails(userEmails);
            foreach(var message in messages)
            {
                if(message.Direction != 1)
                {
                    if (message.Sender == Identity.Current.Email)
                    {
                        message.Sender = Identity.Current.FullName;
                    }
                    else
                    {
                        var user = users.FirstOrDefault(u => message.Sender.Contains(u.Email));
                        if (user != null)
                        {
                            message.Sender = user.FullName;
                        }
                    }
                }
            }

            return new JsonNetResult(new { Messages = messages });
        }

        [JsonErrorHandler(ErrorMessage = "An error occured on the server, failed to get action item attachments.")]
        public ActionResult GetActionItemAttachments(int ticketId)
        {
            //TBD: Add check to make sure current user has access to ticket.
            var attachments = _ticketingService.GetTicketAttachments(ticketId);
            return new JsonNetResult(new { Attachments = attachments });
        }

        [JsonErrorHandler(ErrorMessage = "An error occured on the server, failed to add message to action item.")]
        public ActionResult ReplyToActionItem(TicketReplyMessage reply)
        {
            //TBD: Add check to make sure current user has access to ticket.
            reply.FromAddress = Identity.Current.Email;
            if (!string.IsNullOrEmpty(reply.Body))
            {
                _ticketingService.SendReply(reply);
            }

            //Save any attachments
            SaveAttachments(reply.TicketNumber);

            var ticket = _ticketingService.GetTicket(reply.TicketId);

            return new JsonNetResult(new { ActionItem = ticket });  
        }

        [JsonErrorHandler(ErrorMessage = "An error occured on the server, failed to create action item.")]
        public ActionResult CreateActionItem(IncomingTicket ticket)
        {

            string senderEmail = Identity.Current.Email;
            string senderName = Identity.Current.FullName;

            Ticket newTicket = null;

            if (!string.IsNullOrEmpty(ticket.Body))
            {
                newTicket = _ticketingService.CreateTicket(senderEmail, senderName, ticket);
                SaveAttachments(newTicket.TicketNumber);
            }

            return new JsonNetResult(new { ActionItem = newTicket});
        }

        public void SaveAttachments(string ticketNumber)
        {
            //Save the Files.
            foreach (string key in Request.Files)
            {
                var file = Request.Files[key];
                byte[] data;
                using (Stream inputStream = file.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();

                    _ticketingService.AddAttachment(ticketNumber, 0, file.FileName, data, file.ContentLength);
                }
            }

        }

        public ActionResult NewActionItemModal()
        {
            return PartialView();
        }


    }
}