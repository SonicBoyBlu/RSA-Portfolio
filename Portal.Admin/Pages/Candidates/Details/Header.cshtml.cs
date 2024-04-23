using Global.Data.SQL;
using Global.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Candidates.Details
{
    public class HeaderModel : PageModel
    {
        public required Global.Candidates.Details.Models.Overview Stats {  get; set; }
        public void OnGet(int UserID)
        {
            Stats = new Global.Candidates.Details.Models.Overview()
            {
                UserID = UserID, 
            };
            try
            {
                QueryParameters p = new();
                p.Add(new QueryParameter() { Name = "@userID", Value = UserID });
                Stats = DbContext.ConvertToList<Global.Candidates.Details.Models.Overview>
                    (DbContext.SqlDataSet("spGetCandidatesStatsOverview", p).Tables[0])[0];


                /*-
                var data = DbContext.ConvertToList<string>(DbContext.SqlDataSet("spGetCandidateStatusJson", p).Tables[0]);

                var json = Global.Data.SQL.DbContext.ConvertJsonObject<Global.Candidates.Details.Models.Overview>(data[0]);
                //var json = Global.Data.SQL.DbContext.JsonObject<Global.Candidates.Details.Models.Overview>(UserID).;
                Stats = (Global.Candidates.Details.Models.Overview)json;
                */
            }
            catch (Exception ex) { }
        }
    }
}
