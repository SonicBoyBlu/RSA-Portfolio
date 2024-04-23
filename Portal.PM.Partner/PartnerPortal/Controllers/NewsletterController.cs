using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult Index()
        {
            var model = new ViewModels.NewsletterViewModel();
            for(var i = 1; i <10; i++)
            {
                model.Articles.Add(new Models.NewletterItem()
                {
                    Subject = "Acme Newsletter #" + i,
                    Body = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean pellentesque porta purus, quis viverra orci laoreet id.</p> <p>Suspendisse turpis orci, pulvinar non mollis eu, ornare at justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse in facilisis odio. Proin viverra diam sit amet orci faucibus consectetur.</p> <p>Nulla lobortis elementum turpis vitae auctor. Vestibulum a tempor mi. Cras a rhoncus est. Donec quis tempor ligula. Proin lobortis enim non feugiat dapibus. Curabitur accumsan vel felis vitae rhoncus. Maecenas suscipit mauris nisl. Aenean eleifend nisi ac mollis bibendum. Fusce rutrum augue sit amet interdum sagittis. Donec justo mi, rhoncus et ultricies nec, lobortis in lacus. Sed malesuada vel dolor eget convallis.</p>",
                    DatePublished = DateTime.Now.AddMonths(-1 * i),
                    NewsItemID = i
                });
            }
            return View(model);
        }

        public ActionResult FetchNewsletterList()
        {
            var model = new ViewModels.NewsletterViewModel();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spNewslettersGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        try
                        {
                            model.Articles.Add(new Models.NewletterItem()
                            {
                                NewsItemID = (int)r["NewsItemID"],
                                Subject = (string)r["Subject"],
                                Body = (string)r["Body"],
                                DatePublished = (DateTime)r["DatePublished"]

                            });
                        }
                        catch (Exception ex) { }
                    }
                }
            }

            return PartialView("partials/_newsletters", model);
        }

        public ActionResult FetchNewsletter(int id)
        {
            var model = new Models.NewletterItem();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.SmarterTrack))
            {
                SqlCommand cmd = new SqlCommand("spNewsitemGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@newsitemid", id);
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        try
                        {
                            model = new Models.NewletterItem()
                            {
                                NewsItemID = (int)r["NewsItemID"],
                                Subject = (string)r["Subject"],
                                Body = (string)r["Body"],
                                DatePublished = (DateTime)r["DatePublished"]

                            };
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            return PartialView("partials/_newsletter", model);
        }
    }
}