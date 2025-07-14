using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace BlazorComms.Actions
{
    public class FsiNotifications
    {
        public async static void SendNotificationsAsync(int TestUserID = 0)
        {
            await Task.Run(() =>
            {
                SendNotifications(TestUserID);
            });
        }
        public static void SendNotifications(int TestUserID = 0)
        {
            _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
            {
                Hub = Tasks.FSI.hubConnection,
                User = Tasks.FSI.User,
                Message = "FSI: Preparing notifications...",
            });
            TestUserID = Settings.IsTest ? GlobalSettings.Defaults.TestUserID : TestUserID;
            List<BlazorComms.Models.RecipientFsi> recipients = new List<BlazorComms.Models.RecipientFsi>();
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalSettings.DataStores.DefaultConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spFsiHourlyProcessor", conn) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@span", 15);
                    if(TestUserID != 0)
                        cmd.Parameters.AddWithValue("@now", (object)"2022-09-20 11:00:00");
                        //cmd.Parameters.AddWithValue("@now", (object)"2022-09-23 10:00:00");
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                recipients.Add(new Models.RecipientFsi()
                                {
                                    UserID = (int)r["UserID"],
                                    TimeslotID = (int)r["TimeslotID"],
                                    DateTimeslot = (DateTime)r["DateTimeslot"],
                                    FirstName = (string)r["FirstName"],
                                    LastName = (string)r["LastName"],
                                    Phone = (string)r["Phone"],
                                    Email = (string)r["Email"]
                                }
                                );
                            }
                            catch (Exception ex) {
                                _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
                                {
                                    Hub = Tasks.FSI.hubConnection,
                                    User = Tasks.FSI.User,
                                    Message = "(FSI Reminder - User) " + ex.Message,
                                    MessageType = CampusPoint.DataTypes.Consoles.MessageType.Error
                                });
                                throw;
                            }
                        }
                    }
                    conn.Close();
                }
                //int testVal = 0;
                _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
                {
                    Hub = Tasks.FSI.hubConnection,
                    User = Tasks.FSI.User,
                    Message = "FSI Reminders: " + recipients.Count,
                    MessageType = CampusPoint.DataTypes.Consoles.MessageType.Error
                });
                int sent = 0;
                foreach (var r in recipients)
                {
                    try
                    {
                        //testVal = r.TimeslotID;
                        //var test = Engine.Razor.RunCompile(content, Guid.NewGuid().ToString(), null, model);
                        //await Task.Run(() => {
                            CampusPoint.Interviews.Notifications.FSI.Send.All(r.UserID, r.TimeslotID, TestUserID);
                            sent++;
                            _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
                            {
                                Hub = Tasks.FSI.hubConnection,
                                User = Tasks.FSI.User,
                                Message =  "FSI: sending :" + sent +  " - " + r.FullName + "...",
                                MessageType = CampusPoint.DataTypes.Consoles.MessageType.Error
                            //});
                        });
                    }
                    catch (Exception ex) {
                        _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
                        {
                            Hub = Tasks.FSI.hubConnection,
                            User = Tasks.FSI.User,
                            Message = "(FSI Reminder - Recipient) " + ex.Message,
                            MessageType = CampusPoint.DataTypes.Consoles.MessageType.Error
                        });
                        throw;
                    }
                }
            }
            catch (Exception ex) {
                _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
                {
                    Hub = Tasks.FSI.hubConnection,
                    User = Tasks.FSI.User,
                    Message = "(FSI Reminder - Global) " + ex.Message,
                    MessageType = CampusPoint.DataTypes.Consoles.MessageType.Error
                });
                throw;
            }
            _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
            {
                Hub = Tasks.FSI.hubConnection,
                User = Tasks.FSI.User,
                Message = "FSI: completing notifications...",
                MessageType = CampusPoint.DataTypes.Consoles.MessageType.Error
            });
        }
    }
}