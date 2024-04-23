using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorComms
{
    public class Utilities
    {
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss MM/dd/yyyy").ToLower();
        }

        /*
        public static string FormatTimestampedEntry (string User = "Default", string Message = "Unspecified.", CampusPoint.DataTypes.Consoles.MessageType messageType = CampusPoint.DataTypes.Consoles.MessageType.Default)
        {
            Message = FormatConsoleMesssge.Default(Message, messageType);
            return string.Format("{0} [{1}]: {2}", GetTimestamp() , User, Message);
        }

        public class FormatConsoleMesssge
        {
            public static string Default(string message, CampusPoint.DataTypes.Consoles.MessageType messageType)
            {
                string css = messageType.ToString().ToLower();
                string icon = string.Empty;
                switch (messageType)
                {

                    default: break;

                };
                message = string.Format("<span class='{2}'>{1}{0}</span>", message, icon, css);
                return message;
            }
            public static async void Error(HubConnection Hub, string User = "SYSTEM", string Message = "Unknown")
            {
                await Task.Run(() =>
                {
                    // Add DB logging

                    // Possible email tech support

                    _ = Services.Console.BroadcastMessage(
                        Hub,
                        User,
                        "<strong><i class=\"fas fa-exclamation-triangle\"></i> ERROR:</strong> " + Message,
                        CampusPoint.DataTypes.Consoles.MessageType.Error);
                });
            }
            public static async void Alert(HubConnection Hub, string User = "SYSTEM", string Message = "Unknown")
            {
                await Task.Run(() =>
                {
                    // Add DB logging

                    // Possible email tech support

                    _ = Services.Console.BroadcastMessage(
                        Hub,
                        User,
                        "<strong><i class=\"fas fa-exclamation\"></i> ALERT:</strong> " + Message,
                        CampusPoint.DataTypes.Consoles.MessageType.Alert);
                });
            }
            public static async void Info(HubConnection Hub, string User = "SYSTEM", string Message = "Unknown")
            {
                await Task.Run(() =>
                {
                    // Add DB logging

                    // Possible email tech support

                    _ = Services.Console.BroadcastMessage(
                        Hub,
                        User,
                        "<strong><i class=\"fas fa-info-circle\"></i> Info:</strong> " + Message,
                        CampusPoint.DataTypes.Consoles.MessageType.Info);
                });
            }

            public static async void Query(HubConnection Hub, string User = "SYSTEM", string Message = "Unknown Query")
            {
                await Task.Run(() =>
                {
                    // Add DB logging

                    // Possible email tech support

                    _ = Services.Console.BroadcastMessage(
                        Hub,
                        User,
                        "<strong><i class=\"fas fa-question\"></i> Query:</strong> " + Message,
                        CampusPoint.DataTypes.Consoles.MessageType.Query);
                });
            }

        }
        */
    }
}
