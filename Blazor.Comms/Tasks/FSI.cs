using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace BlazorComms.Tasks
{
    public class FSI
    {
        internal static System.Timers.Timer Timer = null;
        internal static Thread ProcessThread;
        public static int iPID = 0;
        // <internalID, ServerID>
        public static Dictionary<int, int> Processes = new Dictionary<int, int>();

        public static string User = "FSI";
        public static HubConnection hubConnection;

        public static async Task WarmUp()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(Settings.Hub.Url + "FsiHub")
                .WithAutomaticReconnect()
                .Build();
            _ = hubConnection.On<int, int, string>("RecieveMessage", (tid, pid, message) =>
            {
                //iPID = pid;
                int id = Processes.Where(t => t.Key == tid).FirstOrDefault().Key;
                if (id == 0) Processes.Add(tid, pid);
            });
            await hubConnection.StartAsync();

            await Task.Run(() =>
            {
                _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
                {
                    Hub = Tasks.FSI.hubConnection,
                    User = Tasks.FSI.User,
                    Message = "FSI Warming Up...",
                    MessageType = CampusPoint.DataTypes.Consoles.MessageType.Info,
                });

                //_ = Services.Console.Log(hubConnection, User, , CampusPoint.DataTypes.Consoles.MessageType.Info, -1);


                ProcessThread = new Thread(Tasks.FSI.ProcessQueueAsync)
                {
                    Name = "ProcessorThread_FSI"//, IsBackground = true
                };

                DateTime now = DateTime.Now;
                // Set to schedule at the next 15 min mark
                int next15 = 0;
                int min = now.Minute;
                if(min == 0 || min == 15 || min == 30 || min == 45)
                {
                    next15 = 0;
                } else
                {
                    switch (now.Minute)
                    {
                        case < 15: next15 = 15 - min; break;
                        case < 30: next15 = 30 - min; break;
                        case < 45: next15 = 45 - min; break;
                        case <= 59: next15 = 59 - min; break;
                    }

                }
                int delay = 0;
                if(next15 > 0)
                    delay = ((next15 * 60) - now.Second) * 1000;

                _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
                {
                    Hub = Tasks.FSI.hubConnection,
                    User = Tasks.FSI.User,
                    Message = string.Format("Delay FSI Scheduler for: {0}ns ({1}s)...", delay, delay / 1000),
                    MessageType = CampusPoint.DataTypes.Consoles.MessageType.Warn
                });
                /*
                Services.Console.Log(
                    hubConnection, 
                    User, 
                    string.Format("Delay FSI Scheduler for: {0}ns ({1}s)...", delay, delay / 1000), 
                    CampusPoint.DataTypes.Consoles.MessageType.Warn);
                */
                if (delay > 0)
                {
                    Timer = new System.Timers.Timer(delay)
                    {
                        AutoReset = false,
                        Enabled = true
                    };
                    Timer.Elapsed += new System.Timers.ElapsedEventHandler(SetTimer);
                    Timer.Start();
                }

                ProcessQueueAsync();
            });
            //*/
        }


        public async static void ProcessQueueAsync()
        {
            await Task.Run(() =>
            {
                ProcessQueue();
            });
        }
        public static void ProcessQueue(object sender = null, System.Timers.ElapsedEventArgs e = null)
        {
            _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
            {
                Hub = Tasks.FSI.hubConnection,
                User = Tasks.FSI.User,
                Message = "Begin sending FSI reminders.",
            });
            //_ = Services.Console.Log(hubConnection, User, "Begin sending FSI reminders.");
            Actions.FsiNotifications.SendNotificationsAsync(Settings.IsTest ? GlobalSettings.Defaults.TestUserID : 0);
            _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
            {
                Hub = Tasks.FSI.hubConnection,
                User = Tasks.FSI.User,
                Message = "Finish sending FSI reminders."
            });
            //_ = Services.Console.Log(hubConnection, User, "Finish sending FSI reminders.");
       }
        private static void SetTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            _ = Services.Console.LogAsync(new CampusPoint.Models.Hubs.TaskItem()
            {
                Hub = Tasks.FSI.hubConnection,
                User = Tasks.FSI.User,
                Message = "Scheduling FSI reminder runs."
            });
            //_ = Services.Console.Log(hubConnection, User, "Scheduling FSI reminder runs.");
            Timer.Stop();

            Timer = new System.Timers.Timer(GlobalSettings.Time.minute * 15)
            {
                AutoReset = true,
                Enabled = true
            };
            Timer.Elapsed += new System.Timers.ElapsedEventHandler(ProcessQueue);
            Timer.Start();
            ProcessQueueAsync();
        }

        /*
        private static void ProcessQueue()
        {
            _ = Services.Telemetry.BroadcastMessage(hubConnection, User, "Test...");
            Thread.Sleep(4000);
        }
        */
    }
}
