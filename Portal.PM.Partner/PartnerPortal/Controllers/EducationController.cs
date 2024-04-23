using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class EducationController : Controller
    {
        // GET: Education
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FetchLessons(int CategoryID = 0, int ParentID = 0)
        {
            switch (Identity.Current.UserType)
            {
                case DataTypes.UserType.Owner: ParentID = 8; break;
                default: ParentID = 2; break;
            }


            var model = Methods.Education.GetLessons(CategoryID, ParentID);



            /*var model = new ViewModels.EducationViewModel();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spKbItemsGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@categoryid", CategoryID == 0 ? DBNull.Value : (object)CategoryID);
                conn.Open();
                using(var r = cmd.ExecuteReader())
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
                        catch(Exception ex) { }
                    }
                    r.NextResult();
                    while (r.Read())
                    {
                        try
                        {
                            model.Lessons.Add(new Models.EducationItem()
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
            */

            /*** Mock data *** /
            var list = new List<Models.EducationItem>();
            for (var id = 1; id < 6; id++)
            {
                list.Add(new Models.EducationItem()
                {
                    KbID = id,
                    Title = "Education Item #" + id,
                    Category = "Getting Started",
                    Description = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean pellentesque porta purus, quis viverra orci laoreet id.</p> <p>Suspendisse turpis orci, pulvinar non mollis eu, ornare at justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse in facilisis odio. Proin viverra diam sit amet orci faucibus consectetur.</p> <p>Nulla lobortis elementum turpis vitae auctor. Vestibulum a tempor mi. Cras a rhoncus est. Donec quis tempor ligula. Proin lobortis enim non feugiat dapibus. Curabitur accumsan vel felis vitae rhoncus. Maecenas suscipit mauris nisl. Aenean eleifend nisi ac mollis bibendum. Fusce rutrum augue sit amet interdum sagittis. Donec justo mi, rhoncus et ultricies nec, lobortis in lacus. Sed malesuada vel dolor eget convallis.</p>",
                    Thumbnail = "https://via.placeholder.com/80",
                    Video = "https://www.youtube.com/watch?v=oHg5SJYRHA0"
                });
            }
            for (var id = 1; id < 25; id++)
            {
                list.Add(new Models.EducationItem()
                {
                    KbID = id,
                    Title = "Education Item #" + id,
                    Category = "Property Care",
                    Description = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean pellentesque porta purus, quis viverra orci laoreet id.</p> <p>Suspendisse turpis orci, pulvinar non mollis eu, ornare at justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse in facilisis odio. Proin viverra diam sit amet orci faucibus consectetur.</p> <p>Nulla lobortis elementum turpis vitae auctor. Vestibulum a tempor mi. Cras a rhoncus est. Donec quis tempor ligula. Proin lobortis enim non feugiat dapibus. Curabitur accumsan vel felis vitae rhoncus. Maecenas suscipit mauris nisl. Aenean eleifend nisi ac mollis bibendum. Fusce rutrum augue sit amet interdum sagittis. Donec justo mi, rhoncus et ultricies nec, lobortis in lacus. Sed malesuada vel dolor eget convallis.</p>",
                    Thumbnail = "https://via.placeholder.com/80",
                    Video = "https://www.youtube.com/watch?v=oHg5SJYRHA0"
                });
            }
            for (var id = 1; id < 10; id++)
            {
                list.Add(new Models.EducationItem()
                {
                    KbID = id,
                    Title = "Education Item #" + id,
                    Category = "Accounting",
                    Description = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean pellentesque porta purus, quis viverra orci laoreet id.</p> <p>Suspendisse turpis orci, pulvinar non mollis eu, ornare at justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse in facilisis odio. Proin viverra diam sit amet orci faucibus consectetur.</p> <p>Nulla lobortis elementum turpis vitae auctor. Vestibulum a tempor mi. Cras a rhoncus est. Donec quis tempor ligula. Proin lobortis enim non feugiat dapibus. Curabitur accumsan vel felis vitae rhoncus. Maecenas suscipit mauris nisl. Aenean eleifend nisi ac mollis bibendum. Fusce rutrum augue sit amet interdum sagittis. Donec justo mi, rhoncus et ultricies nec, lobortis in lacus. Sed malesuada vel dolor eget convallis.</p>",
                    Thumbnail = "https://via.placeholder.com/80",
                    Video = "https://www.youtube.com/watch?v=oHg5SJYRHA0"
                });
            }
            /*** / Mock data *** /
            model.Lessons = list;

            string cat = string.Empty;
            foreach(var i in list)
            {
                if(i.Category != cat)
                {
                    cat = i.Category;
                    //model.Lessons.Add(list.Where(l => l.Category == cat).ToList());
                    model.Categories.Add(i.Category);
                }
            }
            */

            return PartialView("partials/_lessons", model);
        }

        public ActionResult FetchLesson(int id)
        {
            var model = new Models.KnowledgeBaseItem();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spKbArticleID", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@kbid", id);
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

            /*
            var model = new Models.EducationItem()
            {
                KbID = id,
                Title = "Education Item #" + id,
                Category = "New Property Owners",
                Description = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean pellentesque porta purus, quis viverra orci laoreet id.</p> <p>Suspendisse turpis orci, pulvinar non mollis eu, ornare at justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse in facilisis odio. Proin viverra diam sit amet orci faucibus consectetur.</p> <p>Nulla lobortis elementum turpis vitae auctor. Vestibulum a tempor mi. Cras a rhoncus est. Donec quis tempor ligula. Proin lobortis enim non feugiat dapibus. Curabitur accumsan vel felis vitae rhoncus. Maecenas suscipit mauris nisl. Aenean eleifend nisi ac mollis bibendum. Fusce rutrum augue sit amet interdum sagittis. Donec justo mi, rhoncus et ultricies nec, lobortis in lacus. Sed malesuada vel dolor eget convallis.</p>",
                Thumbnail = "data:image/jpeg;base64,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",
                Video = "<iframe width='560' height='315' src='https://www.youtube.com/embed/oHg5SJYRHA0' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>"
            };
            */
            return PartialView("partials/_lesson", model);

        }
    }
}