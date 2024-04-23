namespace BlazorComms
{
    public class Settings
    {
        public static class Hub
        {
            public static string Url = IsTest ? GlobalSettings.Urls.BlazorServer.Debug : GlobalSettings.Urls.BlazorServer.Production;
        }
        public static bool IsTest = System.Diagnostics.Debugger.IsAttached;

    }
}
