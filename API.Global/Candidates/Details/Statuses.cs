using Global.Data.SQL;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Global.Candidates.Details
{
    public class Statues
    {
        public static Models.Status GetStatuses(int UserID, bool ShowAppCount = true)
        {
            Models.Status status = new Models.Status();
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.Settings.DataStores.DefaultConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetCandidateStatusJson", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@userid", UserID));
                    cmd.Parameters.Add(new SqlParameter("@showAppCount", ShowAppCount));
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            status = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Status>(r[0].ToString());
                            if (status.StatusID == 4) status.StatusNotes = "Hired";
                            if (status.StatusID == 5) status.StatusNotes = "Inactive";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return status;
        }
    }
}
