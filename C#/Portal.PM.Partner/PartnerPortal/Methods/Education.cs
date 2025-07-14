using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Acme.Methods
{
    public class Education
    {
        public static ViewModels.KnowledgeBaseViewModel GetLessons(int CategoryID = 0, int ParentID = 0)
        {
            var model = new ViewModels.KnowledgeBaseViewModel();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spKbItemsGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@categoryid", CategoryID == 0 ? DBNull.Value : (object)CategoryID);
                cmd.Parameters.AddWithValue("@parentid", ParentID == 0 ? DBNull.Value : (object)ParentID);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        try
                        {
                            model.Categories.Add(
                                (int)r["CategoryID"],
                                (string)r["CategoryName"]
                            );
                        }
                        catch (Exception ex) { }
                    }
                    r.NextResult();
                    while (r.Read())
                    {
                        try
                        {
                            model.Lessons.Add(new Models.KnowledgeBaseItem()
                            {
                                KbID = (int)r["KbID"],
                                CategoryID = (int)r["CategoryID"],
                                Title = (string)r["Title"],
                                Category = (string)r["CategoryName"],
                                Description = (string)r["Description"]

                            });
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            return model;

            //Acme.Services.
        }

    }
}