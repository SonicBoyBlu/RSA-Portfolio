using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acme.Helpers
{
    public class Salutations
    {
        public static List<Models.Common.Salutation> GetSalutations()
        {
            var model = new List<Models.Common.Salutation>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                using (SqlCommand cmd = new SqlCommand("spGetSalutations", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Common.Salutation>>((string)r[0]);
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