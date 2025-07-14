/// <summary>
/// Model objects for search utilites
/// </summary>
namespace Global.Search.Models {
    public class KeywordSearch
        {
            public KeywordSearch()
            {
                Keywords = string.Empty;
                IsActive = true;
                Market = DataTypes.Markets.All;
                DateStart = DateTime.MinValue;
                DateEnd = DateTime.MinValue;
                QuickView = "new-reg";
                IsShowInactive = false;
            }
            public string Keywords { get; set; }
            public bool? IsActive { get; set; }
            public DataTypes.Markets Market { get; set; }

            public bool IsShowInactive { get; set; }
            public string QuickView { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
        }
}