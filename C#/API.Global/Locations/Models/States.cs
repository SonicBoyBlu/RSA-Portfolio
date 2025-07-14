using Global.Interfaces; 
/// <summary>
/// Summary description for AddressStates
/// </summary>

namespace Global.Location
{    
    public class State : IState
    {
        public State()
        {
            StateID = 0;
            StateCode = "--";
            StateName = "Unknown";
        }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
    }

    public partial class MarketDetail : State
    {
        public MarketDetail() {
            MarketCode = "--";
            MarketName = "Unknown";
        }
        public int MarketID { get; set; }
        public string MarketName { get; set; }
        public string MarketCode { get; set; }
    }
}