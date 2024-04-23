using Global.Data.SQL;
using Global.Location;

namespace Global.Candidates.Details
{
    public class Overview
    {
        public static Models.Overview GetOverview(int UserID)
        {
            var _res = new Models.Overview();
            try
            {
                QueryParameters p = new();
                p.Add(new QueryParameter() { Name = "@userid", Value = UserID });
                p.Add(new QueryParameter() { Name = "@showAppCount", Value = true });
                var data = DbContext.SqlDataSet("spGetCandidateStatusJson", p);

                _res =
                    DbContext.ConvertToList<Models.Overview>(data.Tables[0])[0];
                /*
                _res =
                    DbContext.ConvertToList<Models.Overview>(data.Tables[0]).FirstOrDefault()
                    ??
                    new Models.Overview();
                //*/
            }
            catch (Exception ex) { }
            return _res;
        }
    }
}
