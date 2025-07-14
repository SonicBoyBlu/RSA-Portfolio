using Global.Data.SQL;

namespace Global.Candidates
{
    public partial class Search
    {
        public static List<Global.Users.Models.User> Basic(Global.Search.Models.KeywordSearch keywordSearch)
        {
            var kw = keywordSearch;
            List<Users.Models.User> users = new List<Users.Models.User>();
            try
            {
                QueryParameters p = new();
                p.Add(new QueryParameter() { Name = "@search", Value = string.IsNullOrEmpty(kw.Keywords) ? DBNull.Value : kw.Keywords.Trim() });
                p.Add(new QueryParameter() { Name = "@isactive", Value = kw.IsActive == null ? DBNull.Value : (object)kw.IsActive });
                p.Add(new QueryParameter() { Name = "@marketid", Value = kw.Market == DataTypes.Markets.All ? DBNull.Value : (object)kw.Market });
                p.Add(new QueryParameter() { Name = "@datestart", Value = kw.DateStart == DateTime.MinValue ? DBNull.Value : (object)kw.DateStart });
                p.Add(new QueryParameter() { Name = "@dateend", Value = kw.DateEnd == DateTime.MinValue ? DBNull.Value : (object)kw.DateEnd });
                p.Add(new QueryParameter() { Name = "@isshowinactive", Value = kw.IsShowInactive });
                p.Add(new QueryParameter() { Name = "@quickview", Value = kw.QuickView });

                var data = DbContext.SqlDataSet("spSearchUsersByKeyword_v3", p);

                users =
                    DbContext.ConvertToList<Users.Models.User>(data.Tables[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return users;
        }
    }
}
