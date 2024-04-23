//using Global.Jobs.Models;
using Global.Jobs.Models;
using Global.Location;
using Microsoft.Data.SqlClient;
//using System.Diagnostics;
//susing System.Threading.Tasks;

namespace Global.Jobs
{
    public class Get
    {
        public static List<Models.Job> Jobs(int? id)
        {
            var jobs = new List<Models.Job>();
            int jobid = 0;

            int.TryParse(id.ToString(), out jobid);

            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.DataStores.DefaultConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spJobSearch_v4", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@jobid", jobid == 0 ? DBNull.Value : jobid);



                    /*
                    cmd.Parameters.AddWithValue("@UserID", me.UserID); // me.UserType == DataTypes.UserType.Guest ? DBNull.Value : (object)me.UserID);
                    cmd.Parameters.AddWithValue("@usertype", (int)me.UserType);

                    cmd.Parameters.AddWithValue("@isopen", s.IsOpen == null ? DBNull.Value : (object)s.IsOpen);
                    cmd.Parameters.AddWithValue("@isactive", s.IsActive == null ? DBNull.Value : (object)s.IsActive);
                    //cmd.Parameters.AddWithValue("@ispublic", s.IsPublic == null ? DBNull.Value : (object)s.IsPublic);
                    //cmd.Parameters.AddWithValue("@isconf", s.IsConfidential == null ? DBNull.Value : (object)s.IsConfidential);
                    //cmd.Parameters.AddWithValue("@istest", s.IsTest == null ? DBNull.Value : (object)s.IsTest);

                    //cmd.Parameters.AddWithValue("@markets", DBNull.Value);
                    //cmd.Parameters.AddWithValue("@markets", DBNull.Value : (object)s.Markets);
                    cmd.Parameters.AddWithValue("@markets", string.IsNullOrEmpty(s.Markets) || s.Markets == "0" ? DBNull.Value : (object)s.Markets);
                    //cmd.Parameters.AddWithValue("@markets", string.IsNullOrEmpty(s.Markets) ? DBNull.Value : (object)s.Markets);

                    cmd.Parameters.AddWithValue("@categories", string.IsNullOrEmpty(s.Categories) || s.Categories == "0" ? DBNull.Value : (object)s.Categories);
                    cmd.Parameters.AddWithValue("@locationtypes", string.IsNullOrEmpty(s.LocationTypes) ? DBNull.Value : (object)s.LocationTypes);

                    cmd.Parameters.AddWithValue("@jobid", s.JobID == 0 ? DBNull.Value : (object)s.JobID);
                    cmd.Parameters.AddWithValue("@companyid", s.CompanyID == 0 ? DBNull.Value : (object)s.CompanyID);

                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(s.Query) ? DBNull.Value : (object)s.Query);
                    cmd.Parameters.AddWithValue("@sortby", string.IsNullOrEmpty(s.SortBy) ? DBNull.Value : (object)s.SortBy);
                    cmd.Parameters.AddWithValue("@islogged", me.IsCpUser ? false : s.IsLogged);
                    cmd.Parameters.AddWithValue("@showQuestionCount", showquestioncount);
                    */
                    conn.Open();
                    SqlDataReader r = cmd.ExecuteReader();
                    int prefid = 0;

                    // job listings
                    while (r.Read())
                    {
                        Models.Job job = new Models.Job();
                        //Models.Jobs.Job job = new() { Address = new()};
                        /*
                        CampusPoint.Models.Jobs.Job job = new Models.Jobs.Job() { 
                            Address = new Models.Location.Address()
                        };
                        */
                        try
                        {
                            string description =
                                System.Text.RegularExpressions.Regex.Replace((string)r["JobDescription"], "<.*?>", string.Empty);
                            description = description.Replace(Environment.NewLine, "<br />");

                            job = new Models.Job()
                            {
                                JobID = (int)r["JobID"],
                                Title = (string)r["JobTitle"],
                                Description = description,
                                PayRange = r["Compensation"] == DBNull.Value ? "DOE" : (string)r["Compensation"],
                                Location = (string)r["Location"],
                                LocationTypeID = (int)r["LocationTypeID"],
                                CompanyID = (int)r["CompanyID"],
                                CompanyName = (string)r["CompanyName"],
                                MarketID = (int)r["MarketID"],
                                IsActive = (bool)r["IsActive"],
                                IsOpen = (bool)r["IsOpen"],
                                IsPublic = (bool)r["IsPublic"],
                                IsRemote = (bool)r["IsRemote"],
                                IsFavorite = (bool)r["IsUserFav"],
                                DateCreated = r["DateCreated"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateCreated"],


                                IsConfidential = (bool)r["IsConfidential"],
                                //JobType = (string)r["JobTypeName"],
                                //JobTypeDescription = (string)r["JobTypeDescription"],
                                CompanyLogo = string.IsNullOrEmpty(r["CompanyLogo"].ToString()) ? "" : Settings.Urls.Logos + r["CompanyLogo"],
                                //Categories = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Jobs.Categories.CategoryList>(r["Categories"].ToString()),

                                // App Status
                                IsApplied = (bool)r["IsApplied"],
                                //NumAppQuestions = s.ShowQuestionCount ? (int)r["NumAppQuestions"] : 0,
                                NumJobApps = (int)r["NumJobApps"],
                                AppStatusTypeID = (int)r["AppStatusTypeID"],
                                //AppStatus = (string)r["AppStatus"],
                                DateApplied = r["DateApplied"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateApplied"],
                                DateAppStatus = r["DateAppStatus"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateAppStatus"],

                                IsHidden = (bool)r["IsHidden"],
                                DateHidden = r["DateHidden"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateHidden"],
                                SortOrder = (int)r["SortOrder"]
                            };
                            if (job.IsConfidential) job.CompanyLogo = Settings.Urls.Logos + "__PrivateListing.png";
                            //*
                            job.Address.City = r["City"] == DBNull.Value ? string.Empty : (string)r["City"];
                            job.Address.State = r["State"] == DBNull.Value ? string.Empty : (string)r["State"];
                            //*/
                            /*
                            job.Address.City = "Test";
                            job.Address.State = "TS";
                            */
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("GetJobs - Read Job from DB Error: " + ex.Message);
                        }
                        jobs.Add(job);
                    }
                    r.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetJobs Error: " + ex.Message);
            }


            /*

            for (int i = 0; i < 10; i++)
            {
                jobs.Add(new Models.Jobs.Job() { JobID = i });
            }
            */
            return jobs;
        }

        public static Models.Job Job(int JobID, int UserID)
        {
            return Job(JobID, false, false, UserID);
        }
        public static Models.Job Job(int JobID, bool IsAd, bool IsDash = false, int UserID = 0)
        {
            var job = new Models.Job();
            var cats = new Models.CategoryList();
            var tmp = new List<string>();

            using (SqlConnection conn = new SqlConnection(Settings.DataStores.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spJobGetByID", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserID", UserID == 0 ? DBNull.Value : UserID);
                //cmd.Parameters.AddWithValue("@UserID", me.UserType == DataTypes.UserType.Guest ? DBNull.Value : (object)me.UserID);
                cmd.Parameters.AddWithValue("@jobid", JobID);
                cmd.Parameters.AddWithValue("@isad", IsAd);

                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        try
                        {
                            tmp = r["Categories"].ToString().Split('~').ToList();
                            cats = new Models.CategoryList();
                            foreach (var t in tmp)
                            {
                                cats.Add(new Category()
                                {
                                    CategoryID = int.Parse(t.Split('|')[0]),
                                    CategoryName = t.Split('|')[1]
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        try
                        {
                            job = new Models.Job()
                            {
                                JobID = (int)r["ID"],
                                Title = (string)r["JobTitle"],
                                //Description = (string)r["JobDescription"].ToString().Replace(Environment.NewLine, "<br />"),
                                Description = (string)r["JobDescription"],
                                PayRange = r["Compensation"] == DBNull.Value ? "DOE" : (string)r["Compensation"],
                                CategoryID = (int)r["CategoryID"],
                                CategoryName = (string)r["CategoryName"],
                                CompanyID = (int)r["CompanyID"],
                                CompanyName = (string)r["CompanyName"],
                                MarketID = (int)r["MarketID"],
                                MarketCode = (string)r["MarketCode"],
                                MarketName = (string)r["MarketName"],
                                IsActive = (bool)r["IsActive"],
                                IsOpen = (bool)r["IsOpen"],
                                IsPublic = (bool)r["IsPublic"],
                                IsRemote = (bool)r["IsRemote"],
                                DateStart = r["DateStart"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateStart"],
                                DateEnd = r["DateEnd"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateEnd"],
                                DateCreated = r["DateCreated"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateCreated"],
                                DatePublished = r["DatePublished"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DatePublished"],
                                IsConfidential = (bool)r["IsConfidential"],
                                //Address = Identity.Current.UserType == DataTypes.UserType.Guest ?
                                Address = UserID == 0 ?
                                    new Address()
                                    {
                                        City = (string)r["City"],
                                        //Region = (string)r["localPrimaryLocationOtherCity"],
                                        State = (string)r["State"],
                                        Zip = (string)r["Zip"]

                                    } :
                                    new Address()
                                    {
                                        Address1 = (string)r["Address1"],
                                        Address2 = (string)r["Address2"],
                                        City = (string)r["City"],
                                        //Region = (string)r["localPrimaryLocationOtherCity"],
                                        State = (string)r["State"],
                                        Zip = (string)r["Zip"]
                                    },
                                Location = (string)r["Location"],

                                LocationTypeID = (int)r["LocationTypeID"],

                                JobAvailability = (string)r["JobAvailability"],
                                JobType = (string)r["JobType"],
                                JobTypeDescription = (string)r["JobTypeDescription"],
                                NumOpenings = Convert.ToInt32(r["NumOpenings"]),
                                ExperienceMin = (string)r["ExperienceMin"],
                                IsFavorite = r["IsUserFav"].ToString() == "1" ? true : false,
                                //CompanyLogo = string.IsNullOrEmpty(r["CompanyLogo"].ToString()) ? "/Assets/Images/LogoNotFound.png" : "https://logos.campuspoint.com/" + r["CompanyLogo"],

                                //RSA: 12-22-2023
                                //CompanyLogo = string.IsNullOrEmpty(r["CompanyLogo"].ToString()) ? string.Empty : GlobalSettings.Urls.Logos + r["CompanyLogo"],
                                //Categories = cats,
                                //AppStatus = (string)r["AppStatus"],

                                // App Status
                                IsApplied = (bool)r["IsApplied"],
                                AppStatusTypeID = (int)r["AppStatusTypeID"],
                                NumAppQuestions = (int)r["NumAppQuestions"],
                                NumJobApps = (int)r["NumJobApps"],
                                DateApplied = r["DateApplied"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateApplied"],
                                DateAppStatus = r["DateAppStatus"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateAppStatus"],

                                IsHidden = (bool)r["IsHidden"],
                                DateHidden = r["DateHidden"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateHidden"]
                            };
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                };
            }
            return job;
        }

        public static int Detail(int id)
        {
            return id;
        }


        public class Categories
        {
            public static CategoryStats CategoryStats()
            {
                CategoryStats stats = new CategoryStats();
                using (SqlConnection conn = new SqlConnection(Settings.DataStores.DefaultConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spGetJobCategoryStats", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    })
                    {

                        //cmd.Parameters.AddWithValue("@CategoryID", CategoryID == 0 ? DBNull.Value : (object)CategoryID);
                        conn.Open();
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                try
                                {
                                    stats.Categories.Add(new Category()
                                    {
                                        CategoryID = (int)r["CategoryID"],
                                        CategoryName = (string)r["CategoryName"],
                                        Description = (string)r["Description"],
                                        TotalJobs = (int)r["TotalJobs"],
                                        IsActive = (bool)r["IsActive"]
                                    });
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            r.NextResult();
                            while (r.Read())
                            {
                                try
                                {
                                    stats.CategoryPayScale.Add(new CategoryPayScale()
                                    {
                                        CategoryID = (int)r["CategoryID"],
                                        CategoryName = (string)r["CategoryName"],
                                        PayAvgLower = (double)r["PayAvgLower"],
                                        PayAvgUpper = (double)r["PayAvgUpper"],
                                        PayRangeLower = (double)r["PayRangeLower"],
                                        PayRangeUpper = (double)r["PayRangeUpper"],
                                        PayType = (string)r["PayType"],
                                    });
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            r.NextResult();
                            while (r.Read())
                            {
                                try
                                {
                                    stats.OverallPayScale.Add(new OverallPayScale()
                                    {
                                        PayAvgLower = (double)r["PayAvgLower"],
                                        PayAvgUpper = (double)r["PayAvgUpper"],
                                        PayRangeLower = (double)r["PayRangeLower"],
                                        PayRangeUpper = (double)r["PayRangeUpper"],
                                        PayType = (string)r["PayType"],
                                    });
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
                return stats;
            }
        }
    }
}
