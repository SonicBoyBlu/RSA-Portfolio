using System;
using System.Data.SqlClient;

namespace Acme.Methods
{
    public class KnowledgeBase
    {
        public static Models.KnowledgeBaseItem GetKnowledgeBaseItem(int KbID)
        {
            var model = new Models.KnowledgeBaseItem();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spKbArticleID", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@kbid", KbID);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        try
                        {
                            model = new Models.KnowledgeBaseItem()
                            {
                                KbID = (int)r["KbID"],
                                CategoryID = (int)r["CategoryID"],
                                Title = (string)r["Title"],
                                Category = (string)r["CategoryName"],
                                Description = (string)r["Description"]

                            };
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            return model;
        }

    }
}