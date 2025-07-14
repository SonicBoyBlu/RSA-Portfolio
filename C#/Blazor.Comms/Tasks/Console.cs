using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorComms.Tasks
{
    public class Console
    {
        private static string User = "SYS";
        public static HubConnection? hubConnection;
        public static string HubID = hubConnection == null ? "" : hubConnection.ConnectionId.ToString();

        public static Task WarmUp()
        {
            try
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(Settings.Hub.Url + "CommConsoleHub")
                    .WithAutomaticReconnect()
                    .Build();
                _ = hubConnection.StartAsync();
                bool conn = false;
                while(hubConnection.State != HubConnectionState.Connected)
                {
                    conn = false;
                }
                //hubConnection.SendAsync("SendMessage", User, "1 - Console hub connected!", CampusPoint.DataTypes.Consoles.MessageType.Default);
                hubConnection.SendAsync("SendMessage", User, "Console hub connected!", CampusPoint.DataTypes.Consoles.MessageType.Default, 0);
                //*
                Services.Console.Log(new CampusPoint.Models.Hubs.TaskItem()
                {
                    User = User,
                    Message = "async: Console hub connected!",
                    ConsoleType = CampusPoint.DataTypes.Consoles.ConsoleTypes.Console, 
                    MessageType = CampusPoint.DataTypes.Consoles.MessageType.Info
                });
                //*/
            }
            catch (Exception) { }
            return Task.CompletedTask;
        }

        // System Idle Message?
    }
}
