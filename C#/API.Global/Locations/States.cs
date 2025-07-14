using Global.Data.SQL;

namespace Global.Location
{
    public partial class States
    {
        public static List<State> GetStates()
        {
            var states = new List<State>();
            try
            {
                states = DbContext.ConvertToList<State>(DbContext.SqlDataSet("spGetStates").Tables[0]);
            } 
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return states;
        }

        public static int GetStateID(string Name)
        {
            var state = GetStateByName(Name);
            var id = state.StateID;
            return id;
        }

        public static State GetStateByName(string Name)
        {
            var state = 
                GetStates()
                    .Where(s => s.StateCode.ToUpper() == Name.ToUpper() || s.StateName.ToUpper() == Name.ToUpper())
                    .FirstOrDefault()
                ??
                    new State()
                    {
                        StateID = 0,
                        StateName = Name
                    };
            return state;
        }
    }

    public partial class MarketDetail
    {
        public static MarketDetail GetMarketByStateID(int StateID) {

            MarketDetail market = new();
            try
            {
                QueryParameters p = new();
                p.Add(new QueryParameter() { Name = "@stateID", Value = StateID });

                var data = DbContext.SqlDataSet("spGetMarketDefaultByStateID", p);

                market = 
                    DbContext.ConvertToList<MarketDetail>(data.Tables[0]).FirstOrDefault()
                    ??
                    new MarketDetail();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return market;
        }
    }
}