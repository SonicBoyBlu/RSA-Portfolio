/// <summary>
/// Summary description for Markets
/// </summary>
namespace Global.Location.Markets
{

    public class MarketList : List<Market>
    {
        public MarketList()
        {
        }
    }
    public class Market
    {
        public int MarketID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public int AvailablePositions { get; set; }

        public int StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }

        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public bool IsLive { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDefault { get; set; }
    }
    public class MarketSearch
    {
        public MarketSearch()
        {
            IsLive = true;
            IsPublic = true;
        }
        public int MarketID { get; set; }
        public string MarketCode { get; set; }
        public bool IsPublic { get; set; }
        public bool IsLive { get; set; }
    }

    public class MarketSubRegions : List<MarketSubRegion>
    {
        public MarketSubRegions() { }
    }

    public class MarketSubRegion
    {
        public MarketSubRegion() { RegionID = -1; RegionName = string.Empty; ParentMarketID = -1; }
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public int ParentMarketID { get; set; }
        public string MarketCode { get; set; }
    }
}