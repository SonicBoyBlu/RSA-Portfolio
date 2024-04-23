using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.SmartTrackerWebService;
using Acme.SmartTrackerOrganizationWebService;
using Acme.SmartTrackerTaskWebService;


namespace Acme.Services.Ticketing
{
    /// <summary>
    /// Wrapper class for the SmartTracker Webservice.
    /// </summary>
    /// 
    public class SmarterTrackService : ITicketingService
    {
        //TBD: Read the admin creds from an external configurable source.
        private string _adminName = "API_USER";
        private string _adminPassword = "d*7FC8jFfi]Q.Ze1uou_]#rVFYf#3-#5#CbF-ceaEJd7:)E7Y";
        private const string _baseUrl = "https://help.acmehouseco.com/";
        private const int _departmentId = 1;  //Customer Service
        private const int _groupId = 11; //Customer Service

        private svcTicketsSoapClient _smarterTrackTicketsService;
        private svcOrganizationSoapClient _smarterTrackOrganizationService;
        private svcTasksSoapClient _smarterTrackTaskService;

        public enum TicketStatus { Open = 1, Waiting = 2, Closed = 3, ClosedAndLocked = 4 }

        public SmarterTrackService()
        {
            _smarterTrackTicketsService = new svcTicketsSoapClient();
            _smarterTrackOrganizationService = new svcOrganizationSoapClient();
            _smarterTrackTaskService = new svcTasksSoapClient();
        }

        /// <summary>
        /// Get the open tickets for a user
        /// </summary>
        /// <param name="userId">SmartTracker users email</param>
        /// <returns>List of open tickets for the user.</returns>
        public List<Ticket> GetOpenTickets(string userId)
        {
            //userId = "carmenmdecker@yahoo.com";
            SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
            arr.Add($"EmailAddress={userId}");
            arr.Add("IsOpen=true");
            TicketInfoArrayResult ticketsArray = _smarterTrackTicketsService.GetTicketsBySearch(_adminName, _adminPassword, arr);
            return ToModel(ticketsArray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public Ticket GetTicket(int ticketId)
        {
            var result = _smarterTrackTicketsService.GetTicketInfo(_adminName, _adminPassword, ticketId);
            return ToModel(result.Ticket);
        }

        /// <summary>
        /// Get the closed tickets for a user
        /// </summary>
        /// <param name="userId">SmartTracker users email</param>
        /// <returns>List of open tickets for the user.</returns>
        public List<Ticket> GetClosedTickets(string userId)
        {
            //userId = "carmenmdecker@yahoo.com";

            SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
            arr.Add($"EmailAddress={userId}");
            arr.Add("IsOpen=false");
            TicketInfoArrayResult tickets = _smarterTrackTicketsService.GetTicketsBySearch(_adminName, _adminPassword, arr);
            return ToModel(tickets);
        }

        public List<Ticket> GetEmployeeOpenTickets(string userId)
        {
            List<Ticket> tickets = new List<Ticket>();
            //userId = "jay@acmehouseco.com";
            var user = GetUser(userId);
            if (user != null)
            {
                SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
                arr.Add($"UserId={user.ID}");
                arr.Add("IsOpen=true");
                TicketInfoArrayResult ticketsInfo = _smarterTrackTicketsService.GetTicketsBySearch(_adminName, _adminPassword, arr);
                tickets = ToModel(ticketsInfo, true);
            }
            return tickets;
        }

        /// <summary>
        /// Pings the web service with the admin credentials to test connectivity.
        /// </summary>
        /// <returns></returns>
        public bool Ping()
        {
            IsAvailableResult result = _smarterTrackTicketsService.PingService(_adminName, _adminPassword, "");
            return result.IsAvailable;
        }

        /// <summary>
        /// Gets the number of open tickets for the user
        /// </summary>
        /// <param name="userId">User for getting tickets</param>
        /// <returns>Ticket count</returns>
        public TicketCount GetTicketCount(string userId)
        {
            TicketCount ticketCount = new TicketCount();
            SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
            arr.Add($"EmailAddress={userId}");
            arr.Add("IsOpen=true");
            var searchResult = _smarterTrackTicketsService.GetTicketsBySearch(_adminName, _adminPassword, arr);

            if(searchResult.Tickets.Length > 0)
            {
                ticketCount.ActiveTicketCount = searchResult.Tickets.Count(x => x.IsActive);
                ticketCount.WaitingTicketCount = searchResult.Tickets.Count(x => !x.IsActive);
            }

            return ticketCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public List<TicketMessage> GetTicketMessages(int ticketId)
        {
            List<TicketMessage> messages = new List<TicketMessage>();
            var result = _smarterTrackTicketsService.GetTicketConversationPartList(_adminName, _adminPassword, ticketId);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            var messageList = result.Parts.Where(p => p.Type == 1).ToList();
            var agents = GetAllAgents();

            foreach (var message in messageList)
            {
                //Don't include draft messages.
                if (!message.IsDraft)
                {
                    var ticketMessage = ToModel(message);

                    //Agent to Owner
                    if (message.Direction == 1)
                    {
                        //Strip out email contained in ().
                        int index = message.Sender.LastIndexOf("(");
                        ticketMessage.Sender = index > 0 ? message.Sender.Substring(0, index).Trim() : message.Sender;

                        //If the agent has an avatar url, set it.
                        var agent = agents.FirstOrDefault(a => a.DisplayName == ticketMessage.Sender);
                        if (agent != null)
                        {
                            string avatarUrl = GetAgentAvatarUrl(agent.ID);
                            ticketMessage.AvatarUrl = !string.IsNullOrEmpty(avatarUrl) ? avatarUrl : string.Empty;
                        }
                    }

                    var htmlBodyResult = _smarterTrackTicketsService.GetTicketMessageHtml(_adminName, _adminPassword, message.PartId);
                    if (htmlBodyResult.Result)
                    {
                        ticketMessage.BodyHTML = htmlBodyResult.messageBody;
                    }

                    var textBodyResult = _smarterTrackTicketsService.GetTicketMessagePlainText(_adminName, _adminPassword, message.PartId);
                    if (textBodyResult.Result)
                    {
                        //ticketMessage.BodyPlainText = TextToHtml(textBodyResult.messageBody);
                    }
                    messages.Add(ticketMessage);
                }
            }
            return messages;
        }


        /// <summary>
        /// Returns all attachments assigned to the ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public List<TicketAttachment> GetTicketAttachments(int ticketId)
        {
            var result = _smarterTrackTicketsService.GetTicketAttachments(_adminName, _adminPassword, ticketId);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            return result.Files.Select(x => ToModel(x)).ToList();
        }

        /// <summary>
        /// Gets all the tickets for the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TicketCount GetTickets(string userId)
        {
            TicketCount ticketCount = new TicketCount();
            SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
            //carmenmdecker@yahoo.com
            arr.Add($"EmailAddress={userId}");
            arr.Add("IsOpen=true");
            var result = _smarterTrackTicketsService.GetTicketsBySearch(_adminName, _adminPassword, arr);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }


            if (result.Tickets.Length > 0)
            {
                ticketCount.ActiveTicketCount = result.Tickets.Count(x => x.IsActive);
                ticketCount.WaitingTicketCount = result.Tickets.Count(x => !x.IsActive);
            }

            return ticketCount;
        }

        /// <summary>
        /// Generates a new ticket in the smarterTrack system
        /// </summary>
        /// <param name="senderEmail"></param>
        /// <param name="senderDisplayName"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public Ticket CreateTicket(string senderEmail, string senderDisplayName, IncomingTicket ticket)
        {
            var ticketData = new TicketCreationData()
            {
                
                body = ticket.Body,
                subject = ticket.Subject,
                groupID = _groupId,
                departmentID = _departmentId, 
                displayName = senderDisplayName,
                senderEmailAddress = senderEmail
            };

            var result = _smarterTrackTicketsService.CreateIncomingTicket(_adminName, _adminPassword, ticketData);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            return ToModel(result.Ticket);
        }

        /// <summary>
        /// Adds a reply to the existing smarterTrack ticket.
        /// </summary>
        /// <param name="reply"></param>
        public void SendReply(TicketReplyMessage reply)
        {
            var ticket = GetTicketInfo(reply.TicketNumber);
            var agent = GetUser(ticket.IdAgent);
            reply.ToAddress = agent.UserName;

            var result = _smarterTrackTicketsService.AddMessageToTicket(_adminName,_adminPassword,
                                                                        reply.TicketNumber,
                                                                        reply.FromAddress,
                                                                        reply.ToAddress,
                                                                        reply.CCAddress,
                                                                        reply.Subject,
                                                                        reply.Body,
                                                                        reply.IsHtml,
                                                                        reply.ToCustomer,
                                                                        reply.SendEmail);

            if(!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            SetTicketStatus(reply.TicketNumber, TicketStatus.Open);
        }

        private TicketInfo GetTicketInfo(string ticketNumber)
        {
            var result = _smarterTrackTicketsService.GetTicketInfoByTicketNumber(_adminName, _adminPassword, ticketNumber);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            return result.Ticket;

        }

        private void SetTicketStatus(string ticketNumber, TicketStatus ticketStatus)
        {
            SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
            arr.Add($"TicketStatusID={(int)ticketStatus}");

            var result = _smarterTrackTicketsService.SetTicketProperties(_adminName, _adminPassword, ticketNumber, arr);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }
        }

        public TicketAttachment AddAttachment(string ticketNumber, int ticketMessageId, string fileName, byte[] data, int fileLength)
        {
            var result = _smarterTrackTicketsService.AddTicketAttachment(_adminName, _adminPassword,
                                                            ticketNumber,
                                                            ticketMessageId,
                                                            fileName,
                                                            data,
                                                            fileLength,
                                                            DateTime.UtcNow);

            if(!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            return ToModel(result.File);
        }

        public TicketCount GetEmployeeTicketCount(string userId)
        {
            TicketCount ticketCount = new TicketCount();
            //userId = "jay@acmehouseco.com";
            var user = GetUser(userId);

            if(user != null)
            {
                TicketCountInfoArrayResult result = _smarterTrackTicketsService.GetTicketCounts_Global(_adminName, _adminPassword);
                var counts = result.TicketCounts.Where(t => t.UserId == user.ID).ToList();

                if (counts.Count > 0)
                {
                    ticketCount.ActiveTicketCount = counts.Sum(x => x.WaitingTicketCount);
                    ticketCount.WaitingTicketCount = counts.Sum(x => x.ActiveTicketCount);
                }
            }
            return ticketCount;
        }

        public int GetEmployeeTaskCount(string userId)
        {
            int taskCount = 0;
            var user = GetUser(userId);
            if (user != null)
            {
                var result = _smarterTrackTaskService.GetTasksByUser(_adminName, _adminPassword, user.ID);
                if (result.Tasks != null)
                {
                    taskCount = result.Tasks.Count(x => x.TaskStatusID != TaskStatus.Completed &&
                                                        x.TaskStatusID != TaskStatus.Rejected);
                }
            }
            return taskCount;
        }

        /// <summary>
        /// GetAgentAvatarUrl
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string GetAgentAvatarUrl(int userId)
        {
            string avatarUrl = string.Empty;
            SmartTrackerOrganizationWebService.ArrayOfString arr = new SmartTrackerOrganizationWebService.ArrayOfString();
            arr.Add($"AvatarUrl");
            var result = _smarterTrackOrganizationService.GetUsersProperties(_adminName, _adminPassword, userId, arr);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            if (result.RequestResults != null && result.RequestResults.Count > 0)
            {
                avatarUrl = result.RequestResults.FirstOrDefault();
                //strip out the url value ;
                int index = avatarUrl.IndexOf("/");
                avatarUrl = index > 0 ? avatarUrl.Substring(index + 1) : avatarUrl;
                //append base url.
                avatarUrl = $"{_baseUrl}{avatarUrl}";
            }
            return avatarUrl;
        }

        /// <summary>
        /// Get all smartertrack agents.
        /// </summary>
        private List<AgentInfo> GetAllAgents()
        {
            var result = _smarterTrackOrganizationService.GetAllAgents(_adminName, _adminPassword);

            if (!result.Result)
            {
                throw new Exception($"{result.ResultCode } - {result.Message}");
            }

            return result.Agents.ToList();

        }

        /// <summary>
        /// Get agent by username.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private AgentInfo GetUser(string userId)
        {
            var result = _smarterTrackOrganizationService.GetAllAgents(_adminName, _adminPassword);
            return result.Agents.Where(a => a.UserName == userId).FirstOrDefault();
        }

        /// <summary>
        /// Get user by user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private AgentInfo GetUser(int userId)
        {
            var result = _smarterTrackOrganizationService.GetAllAgents(_adminName, _adminPassword);
            return result.Agents.Where(a => a.ID == userId).FirstOrDefault();
        }


        private List<Ticket> ToModel(TicketInfoArrayResult ticketInfos, bool employee=false)
        {
            return ticketInfos.Tickets.Select(t => ToModel(t,employee)).ToList();
        }

        private Ticket ToModel(TicketInfo ticketInfo, bool employee=false)
        {
            var ticket = new Ticket()
            {
                TicketId = ticketInfo.ID,
                TicketNumber = ticketInfo.TicketNumber,
                AgentId = ticketInfo.IdAgent,
                Active = ticketInfo.IsActive,
                ReplyCountIn = ticketInfo.ReplyCountIn,
                ReplyCountOut = ticketInfo.ReplyCountOut,
                LastReplyDate = ticketInfo.LastReplyDateUtc.ToLocalTime(),
                Subject = ticketInfo.Subject,
                IsAgentOwner = employee,
                IsOpen = ticketInfo.IsOpen
            };

            if(!ticket.IsOpen)
            {
                SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
                arr.Add($"TicketStatusID");

                var result = _smarterTrackTicketsService.GetTicketProperties(_adminName, _adminPassword, ticketInfo.TicketNumber, arr);
                if(result.Result)
                {
                    if(result.RequestResults != null && result.RequestResults.Count > 0)
                    {
                        ticket.IsLocked = int.Parse(GetResultValue(result.RequestResults.FirstOrDefault())) == (int)TicketStatus.ClosedAndLocked;
                    }
                }
                
            }

            return ticket;
        }

        private TicketMessage ToModel(TicketPartInfo ticketPartInfo)
        {
            var ticketMessage = new TicketMessage()
            {
                MessageId = ticketPartInfo.PartId,
                Subject = ticketPartInfo.Description,
                Direction = ticketPartInfo.Direction,
                IsDraft = ticketPartInfo.IsDraft,
                Recipient = ticketPartInfo.Recipient,
                Sender = ticketPartInfo.Sender,
                SentDate = ticketPartInfo.CreationDateUtc.ToLocalTime()
            };
            return ticketMessage;
        }
        private TicketAttachment ToModel(TicketAttachmentInfo attachmentInfo)
        {
            return new TicketAttachment()
            {
                DownloadUrl = _baseUrl + attachmentInfo.downloadUrl,
                FileDate = attachmentInfo.fileDateUtc,
                Id = attachmentInfo.fileId,
                FileName = attachmentInfo.fileNameOriginal,
                FileSize = attachmentInfo.fileSizeInBytes,
                Sender = attachmentInfo.sender,
                SenderId = attachmentInfo.senderUserId,
                TicketMessageId = attachmentInfo.ticketMessageId
            };
        }

        private string GetResultValue(string result)
        {
            int index = result.IndexOf("=");
            return index > 0 ? result.Substring(index + 1) : result;
        }
 
    }
}