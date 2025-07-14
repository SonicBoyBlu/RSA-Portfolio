using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Acme.Methods
{
    public class SystemMessages
    {
        public static Models.SystemMessage GetMessage(int MessageID)
        {
            var model = new Models.SystemMessage();
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
                {
                    SqlCommand cmd = new SqlCommand("spSystemMessageGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@messageid", MessageID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                model = new Models.SystemMessage()
                                {
                                    MessageID = (int)r["MessageID"],
                                    Title = (string)r["Title"],
                                    Message = (string)r["Message"]
                                };
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            catch(Exception ex) { }
            return model;

        }

    }
}