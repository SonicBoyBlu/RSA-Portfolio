using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Acme.Methods
{
    public class Tickets
    {

        private static UserIdentity me = Identity.Current;
        public static ViewModels.BadgeCounts GetTicketCounts()
        {
            var model = new ViewModels.BadgeCounts();
//            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
//            {
//                SqlCommand cmd = new SqlCommand("spBadgeCountsGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
//                cmd.Parameters.AddWithValue("@userid", Identity.Current.SmarterTrackUserID);
//                cmd.Parameters.AddWithValue("@datelastlogin", me.DateLastLogin == DateTime.MinValue? DBNull.Value : (object) me.DateLastLogin);
//                conn.Open();
//                using (var r = cmd.ExecuteReader())
//                {
//                    while (r.Read())
//                    {
//                        try
//                        {
//                            model.Tickets.TotalNew = (int) r["TotalNew"];
//                            model.Tickets.TotalMessages = (int) r["TotalMessages"];
//                            model.Tickets.TotalOpen = (int) r["TotalOpen"];
//                            model.Tickets.TotalWaiting = (int) r["TotalWaiting"];
//                            model.Tickets.TotalWaitingOnAgent = (int) r["TotalWaitingOnAgent"];
//                            model.Tickets.TotalWaitingOnMe = (int) r["TotalWaitingOnMe"];
//                            model.Tickets.TotalClosed = (int) r["TotalClosed"];
//}
//                        catch (Exception ex) { }
//                    }
//                }
//            }
            return model;
        }
    }
}