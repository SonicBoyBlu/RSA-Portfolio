using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acme.Helpers
{
    public class States
    {
        public static List<Models.Common.State> GetStates()
        {
            var model = new List<Models.Common.State>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                using (SqlCommand cmd = new SqlCommand("spGetListStates", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Common.State>>((string)r[0]);
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return model;
        }
    }
}