using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class TeamAcmeController : Controller
    {
        // GET: TeamMembers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FetchTeamMembers()
        {
            var api = new Services.Vendors.BambooHR.Api();
            var data = api.GetEmployees();
            var model = new ViewModels.TeamMembersViewModel();

            foreach(var e in data.Employees.OrderBy(i => i.Department))
            {
                model.TeamMembers.Add(new Models.TeamMember()
                {
                    UserID = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Title = e.JobTitle,
                    Department = e.Department,
                    ProfilePic = e.PhotoURL
                });
            }

            /*
            var members = new List<Models.TeamMember>
            {
                new Models.TeamMember() { FirstName = "Mike", LastName = "Flannery", Title = "Owner/CEO", Department = "Management" },
                new Models.TeamMember() { FirstName = "Jay", LastName = "Borromei", Title = "Connect the dots guy", Department = "Management" },
                new Models.TeamMember() { FirstName = "Boris", LastName = "Start", Title = "Chief Operation Officer ", Department = "Management" },
            };

            for(var i = 0; i < 3; i++)
            {
                members.Add(new Models.TeamMember() { FirstName = "Team", LastName = "Mate" + i, Title = "Job Title", Department = "Property Care" });
            }
            */

            string dept = string.Empty;
            foreach(var m in model.TeamMembers)
            {
                if(m.Department != dept)
                {
                    dept = m.Department;
                    model.Departments.Add(m.Department);
                }
            }

            //model.TeamMembers = members;

            return PartialView("partials/_teamMembers", model);
        }

        public JsonNetResult SyncWithBamboo()
        {
            if (Identity.Current.IsAdmin)
            {
                var api = new Services.Vendors.BambooHR.Api();
                var employeeList = api.GetEmployees();
                int totalUpdated = 0;

                using(SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.BambooHR))
                {
                    using(SqlCommand cmd = new SqlCommand("spEmployeeSync", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        conn.Open();
                        foreach(var e in employeeList.Employees)
                        {
                            try
                            {
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@id", e.Id);
                                cmd.Parameters.AddWithValue("@lastChanged", e.LastChanged == DateTime.MinValue ? (DBNull.Value) : (object)e.LastChanged);
                                cmd.Parameters.AddWithValue("@status", e.Status);
                                cmd.Parameters.AddWithValue("@firstName", e.FirstName);
                                cmd.Parameters.AddWithValue("@middleName", e.MiddleName);
                                cmd.Parameters.AddWithValue("@lastName", e.LastName);
                                cmd.Parameters.AddWithValue("@nickName", e.Nickname);
                                cmd.Parameters.AddWithValue("@displayName", e.DisplayName);
                                cmd.Parameters.AddWithValue("@preferredName", e.PreferredName);
                                cmd.Parameters.AddWithValue("@photourl", e.PhotoURL);
                                cmd.Parameters.AddWithValue("@gender", e.Gender);
                                cmd.Parameters.AddWithValue("@jobTitle", e.JobTitle);
                                cmd.Parameters.AddWithValue("@department", e.Department);
                                cmd.Parameters.AddWithValue("@supervisorID", e.SupervisorId);
                                cmd.Parameters.AddWithValue("@supervisorEID", e.SupervisorEid);
                                cmd.ExecuteNonQuery();
                                totalUpdated++;
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
                return new JsonNetResult(new {
                    totals = new {
                        updated = totalUpdated,
                        total = employeeList.Employees.Count()
                    }
                });
            }
            else return null;
        }
    }
}