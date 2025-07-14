namespace Global.Candidates.Models
{
    public class PreRegDetails
    {
        public PreRegDetails() {
            EventName = "Career Fair";
        }
        public int PreRegID { get; set; }
        public string EventName { get; set; }
        public DataTypes.Markets Market { get; set; }
        public int ReferralTypeID { get; set; }
    }
}
