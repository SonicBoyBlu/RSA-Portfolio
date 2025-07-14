namespace Global.Location
{
    public class Country
    {
        public static string GetCountryByISO(string ISO)
        {
            string c = string.Empty;
            switch (ISO)
            {
                default: c = "USA"; break;
            }
            return c;
        }
    }
}
