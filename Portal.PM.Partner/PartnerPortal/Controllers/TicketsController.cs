using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class TicketsController : Controller
    {
        private UserIdentity me { get { return Identity.Current; } }
        // GET: Tickets
        public ActionResult Index()
        {
            //Acme.SmartTrackerWebService.svcTicketsSoapClient svc = new SmartTrackerWebService.svcTicketsSoapClient();
            //var result = svc.GetTicketCounts_Global("ADMIN_ST", "Acme100");
            //SmartTrackerWebService.ArrayOfString arr = new SmartTrackerWebService.ArrayOfString();
            //arr.Add("UserId=14");
            //var tickets = svc.GetTicketsBySearch("ADMIN_ST", "Acme100", arr);

            //Acme.SmartTrackerOrganizationWebService.svcOrganizationSoapClient osvc = new SmartTrackerOrganizationWebService.svcOrganizationSoapClient();
            //var agents = osvc.GetAllAgents("API_USER", "d*7FC8jFfi]Q.Ze1uou_]#rVFYf#3-#5#CbF-ceaEJd7:)E7Y");
            //var counts = result.TicketCounts.Where(t => t.UserId == 2).ToList();

            return View();
        
        }

        public ActionResult FetchTickets()
        {
            var model = new ViewModels.TicketsViewModel();

            int CatID = 0;
            int ParentID = 8;

            switch (Identity.Current.UserType)
            {
                case DataTypes.UserType.Owner: ParentID = 8; break;
                default: ParentID = 2; break;
            }

            // Education
            var education = Methods.Education.GetLessons(CatID, ParentID);
            foreach (var e in education.Categories)
            {
                model.KB.Add(new Models.KnowledgeBaseCategoryCount()
                {
                    CategoryID = e.Key,
                    Title = e.Value,
                    Total = education.Lessons.Where(i => i.CategoryID == e.Key).Count()
                });
            }

            // Ticket summary with counts
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spTicketSummaryGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userid", me.SmarterTrackUserID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            model.Summary.Statuses.Add(new Models.TicketStatusSummaryItem()
                            {
                                StatusID = (DataTypes.TicketStatus)((int)reader["StatusID"]),
                                Status = (string)reader["Status"],
                                TicketCount = (int)reader["TicketCount"]
                            });
                        }
                        catch (Exception ex) { }
                    }
                }
            }

            //var ticketcounts = Methods.Tickets.GetTicketCounts().Tickets;
            //model.Summary.Statuses.Where(t => t.StatusID == DataTypes.TicketStatus.Open).FirstOrDefault().TicketCount = ticketcounts.TotalOpen;
            //model.Summary.Statuses.Where(t => t.StatusID == DataTypes.TicketStatus.Waiting).FirstOrDefault().TicketCount = ticketcounts.TotalWaiting;
            //model.Summary.Statuses.Where(t => t.StatusID == DataTypes.TicketStatus.Open).FirstOrDefault().TicketCount = 55;
            //model.Summary.Statuses.Where(t => t.StatusID == DataTypes.TicketStatus.Waiting).FirstOrDefault().TicketCount = 60;
            //model.Summary.Statuses.Where(t => t.StatusID == DataTypes.TicketStatus.Closed).FirstOrDefault().TicketCount = ticketcounts.TotalClosed;


            // Ticket history list
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spTicketsListGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userid", me.SmarterTrackUserID);
                cmd.Parameters.AddWithValue("@datelastlogin", me.DateLastLogin == DateTime.MinValue ? DBNull.Value : (object)me.DateLastLogin);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            model.Tickets.Add(new Models.TicketListItem()
                            {
                                TicketID = (int)reader["TicketID"],
                                Status = (DataTypes.TicketStatus)((int)reader["TicketStatusID"]),
                                DepartmentID = (int)reader["DepartmentID"],
                                Department = (string)reader["Department"],
                                Subject = (string)reader["Subject"],
                                HasAttachment = (bool)reader["HasAttachment"],
                                IsNew = (bool)reader["IsNew"],
                                DateOpened = (DateTime)reader["DateOpened"],
                                DateFollowup = reader["DateFollowup"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DateFollowup"],
                                DateClosed = reader["DateClosed"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DateClosed"]
                            });
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            return PartialView("partials/_tickets", model);
        }

        public ActionResult FetchTicket(int id)
        {
            var model = new Models.Ticket();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spTicketGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userid", me.SmarterTrackUserID);
                cmd.Parameters.AddWithValue("@ticketid", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            model= new Models.Ticket()
                            {
                                TicketID = (int)reader["TicketID"],
                                Status = (DataTypes.TicketStatus)((int)reader["TicketStatusID"]),
                                DepartmentID = (int)reader["DepartmentID"],
                                Department = (string)reader["Department"],
                                Subject = (string)reader["Subject"],
                                HasAttachment = (bool)reader["HasAttachment"],
                                IsNew = (bool)reader["IsNew"],
                                DateOpened = (DateTime)reader["DateOpened"],
                                DateFollowup = reader["DateFollowup"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DateFollowup"],
                                DateClosed = reader["DateClosed"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DateClosed"]
                            };
                        }
                        catch (Exception ex) { }
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        try
                        {
                            model.Messages.Add(new Models.TicketMessage()
                            {
                                //MessageID = (int)reader["MessageID"],
                                IsMe = (bool)reader["IsMe"],
                                UserID = (int)reader["UserID"],
                                DisplayName = (string)reader["DisplayName"],
                                Subject = (string)reader["Subject"],
                                Body = (string)reader["Body"],
                                DateSent = (DateTime)reader["DateSent"]
                            });
                        }
                        catch (Exception ex) { }
                    }

                    reader.NextResult();
                    while (reader.Read())
                    {
                        try
                        {
                            model.Attachments.Add(new Models.TicketAttachment()
                            {
                                AttachmentID = (int)reader["AttachmentID"],
                                IsMe = (bool)reader["IsMe"],
                                UserID = (int)reader["UserID"],
                                DisplayName = (string)reader["DisplayName"],
                                FilenameOriginal = (string)reader["FilenameOriginal"],
                                FilenameOnDisk = (string)reader["FilenameOnDisk"],
                                //FileUrl = (string)reader["FileUrl"],
                                DateUploaded = (DateTime)reader["DateUploaded"]
                            });

                        }
                        catch (Exception ex) { }
                    }
                }
            }


            /*
            if (id > 0)
                model = new Models.Ticket()
                {
                    Subject = "Ticket #" + id,
                    Messages = new List<Models.TicketMessage>()
                    {
                        new Models.TicketMessage()
                        {
                            Body = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean pellentesque porta purus, quis viverra orci laoreet id.</p> <p>Suspendisse turpis orci, pulvinar non mollis eu, ornare at justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse in facilisis odio. Proin viverra diam sit amet orci faucibus consectetur.</p> <p>Nulla lobortis elementum turpis vitae auctor. Vestibulum a tempor mi. Cras a rhoncus est. Donec quis tempor ligula. Proin lobortis enim non feugiat dapibus. Curabitur accumsan vel felis vitae rhoncus. Maecenas suscipit mauris nisl. Aenean eleifend nisi ac mollis bibendum. Fusce rutrum augue sit amet interdum sagittis. Donec justo mi, rhoncus et ultricies nec, lobortis in lacus. Sed malesuada vel dolor eget convallis.</p>"
                        }
                    },
                    
                    
                    DateOpened = DateTime.Now.AddDays(-3),
                    TicketID = id,
                    Priority = DataTypes.TicketPriority.Normal,
                    Status = DataTypes.TicketStatus.Open
                };
            */
            return PartialView("partials/_ticket", model);
        }
    }
}