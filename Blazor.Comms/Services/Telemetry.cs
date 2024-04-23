using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorComms.Services
{
    public class Console
    {
        private static async Task BroadcastMessage(CampusPoint.Models.Hubs.TaskItem t)
        {
            try
            {
                HubConnection Hub;
                string url = string.Empty;
                switch (t.ConsoleType)
                {
                    case CampusPoint.DataTypes.Consoles.ConsoleTypes.All:
                        break;
                    case CampusPoint.DataTypes.Consoles.ConsoleTypes.Console:
                        url = "CommConsoleHub";
                        break;
                    case CampusPoint.DataTypes.Consoles.ConsoleTypes.FSI:
                        break;
                    case CampusPoint.DataTypes.Consoles.ConsoleTypes.Bulletins:
                        break;
                }
                Hub = new HubConnectionBuilder()
                    .WithUrl(Settings.Hub.Url + url)
                    .WithAutomaticReconnect()
                    .Build();

                _ = Hub.StartAsync();
                bool conn = false;
                while (Hub.State != HubConnectionState.Connected)
                {
                    conn = false;
                }


                /*
                string msg = FormatTimestampedEntry(new CampusPoint.Models.Hubs.TaskItem() { 
                    User = User, 
                    Message = Message,
                    MessageType = MessageType,
                    ProcessID = PID
                });
                */
                await Hub.InvokeAsync("SendMessage", t.User, t.MessageFormatted, t.ConsoleType, t.ProcessID);
            }
            catch (Exception ex)
            {
                //Utilities.FormatConsoleMesssge.Error(Hub, User, ex.Message);
                throw;
            }
        }

        public static void Log(CampusPoint.Models.Hubs.TaskItem entry)
        {
            /*
            if (entry.TaskID > -1 && entry.ProcessID == 0)
            {
                Tasks.FSI.iPID++;
                entry.TaskID = Tasks.FSI.iPID;
            }
            */
            try
            {
                //entry.Hub.InvokeAsync(entry.Command, entry);
                BroadcastMessage(entry);
            }
            catch (Exception ex)
            {
                entry.MessageType = CampusPoint.DataTypes.Consoles.MessageType.Error;
                entry.Message = ex.Message;
                entry.Hub.InvokeAsync(entry.Command, entry.Message, entry.MessageType, entry.ProcessID);
                throw;
            }
        }
        public static async Task LogAsync(CampusPoint.Models.Hubs.TaskItem entry)
        {
            await Task.Run(() =>
            {
                BroadcastMessage(entry);
                //BroadcastMessage(entry.ConsoleType, entry.User, entry.Message, entry.MessageType, 0);

                //Log(entry);
            });
        }
    }
}
