namespace Global.Location
{
    public class ActiveCandidateLocations
    {
        public string City { get; set; }
        public string State { get; set; }
        public string FormatLocale { get { return string.Format("{0}, {1}", City, State); } }
    }
}
