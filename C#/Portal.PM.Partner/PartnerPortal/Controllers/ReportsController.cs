using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;
using Acme;

namespace PartnerPortal.Controllers
{
    public class ReportsController : Controller
    {
        private bool IsSpa
        {
            get
            {
                bool spa = false;
                //HttpContext.Current.Session["IsSPA"];
                return spa;
            }
        }

        private Acme.Services.Vendors.BambooHR.EmployeeList Team
        {
            get {
                if (_team == null){
                    var api = new Acme.Services.Vendors.BambooHR.Api(); // Vendors.BambooHR.Api();
                    var x = api.GetEmployees();
                    _team = x;
                }
                return _team;
            }
        }
        private Acme.Services.Vendors.BambooHR.EmployeeList _team;

        //private List<Acme.Methods.User> Users
        //{
        //    get
        //    {
        //        if (_users == null)
        //            _users = Acme.Methods.Users.GetUsers();

        //        return _users;
        //    }
        //}
        private List<Acme.Models.User> _users;
        // GET: Reports

        #region Productivity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Productivity(DateTime? DateStart = null, DateTime? DateEnd = null)
        {
            if(DateStart == null)
                DateStart = new DateTime(2019, 05, 01);
            if (DateEnd == null)
                DateEnd = new DateTime(2019, 05, 31);

            // testing payroll
            var api = new Acme.Services.Vendors.BambooHR.Api();
            var test = api.GetPayrollHours();

            // Mock data
            Dictionary<string, Dictionary<string, dynamic>> model = new Dictionary<string, Dictionary<string, dynamic>>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spReportPcTechProductivity", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@datestart", DateStart);
                        cmd.Parameters.AddWithValue("@dateend", DateEnd);
                        conn.Open();
                        using (var r = cmd.ExecuteReader())
                        {
                            List<string> sections = new List<string>()
                            {
                                "Statuses",
                                "Assigned To",
                                "Completed By",
                                "Requested By",
                                "Created By",
                            };
                            var items = new Dictionary<string, dynamic>();
                            foreach(var s in sections)
                            {
                                items = new Dictionary<string, dynamic>();
                                r.NextResult();
                                while (r.Read())
                                {
                                    items.Add((string)r[0], r[1]);
                                }
                                model.Add(s, items);
                            }
                            r.NextResult();
                            items = new Dictionary<string, dynamic>();
                            while (r.Read())
                            {
                                items.Add((string)r[0], string.Format("{0}h {1}m {2}s", (int)r[1], (int)r[2], (int)r[3]));
                            }
                            model.Add("Billed Hours", items);

                        }
                    }
                    catch (Exception ex) { }
                }
            }

            if (IsSpa)
                return PartialView(model);
            else
                return View(model);
        }


        [CompressContent]
        [MinifyHtml]
        public ActionResult TabRawData(DateTime? DateStart = null, DateTime? DateEnd = null)
        {
            if (DateStart == null)
                DateStart = new DateTime(2019, 05, 01);
            if (DateEnd == null)
                DateEnd = new DateTime(2019, 05, 31);

            // testing payroll
            var api = new Acme.Services.Vendors.BambooHR.Api();
            var test = api.GetPayrollHours();

            // Mock data
            Dictionary<string, Dictionary<string, dynamic>> model = new Dictionary<string, Dictionary<string, dynamic>>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spReportPcTechProductivity", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@datestart", DateStart);
                        cmd.Parameters.AddWithValue("@dateend", DateEnd);
                        conn.Open();
                        using (var r = cmd.ExecuteReader())
                        {
                            List<string> sections = new List<string>()
                            {
                                "Statuses",
                                "Assigned To",
                                "Completed By",
                                "Requested By",
                                "Created By",
                            };
                            var items = new Dictionary<string, dynamic>();
                            foreach (var s in sections)
                            {
                                items = new Dictionary<string, dynamic>();
                                r.NextResult();
                                while (r.Read())
                                {
                                    items.Add((string)r[0], r[1]);
                                }
                                model.Add(s, items);
                            }
                            r.NextResult();
                            items = new Dictionary<string, dynamic>();
                            while (r.Read())
                            {
                                items.Add((string)r[0], string.Format("{0}h {1}m {2}s", (int)r[1], (int)r[2], (int)r[3]));
                            }
                            model.Add("Billed Hours", items);

                        }
                    }
                    catch (Exception ex) { }
                }
            }

            return PartialView("tabs/_RawData", model);
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult TabPropertyCare(Acme.Models.Reports._ReportSearch s)
        {
            var model = new Acme.ViewModels.Reports.PcTechsViewModel();
            //u.PhotoUrl = api.GetEmployeePhotoUrl(u.Email);
            var api = new Acme.Services.Vendors.BambooHR.Api(); // Vendors.BambooHR.Api();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("spReportProductivityPcTechs", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", s.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", s.DateEnd);
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                var t = Team.Employees.Where(x => x.DisplayName == (string)r["TeamMember"]).FirstOrDefault();
                                model.BillableItems.Add(new Acme.Models.Reports.Productivity.MaintenanceBillableItem()
                                {
                                    UserID = t != null ? t.Id : 0,
                                    TeamMember = (string)r["TeamMember"],
                                    ProfilePicUrl = t != null ? t.PhotoURL : string.Empty,
                                    Department = t != null ? t.Department : string.Empty,
                                    JobTitle = t != null ? t.JobTitle : string.Empty,
                                    WorkOrdersCreatedBy = (int)r["WorkOrdersCreatedBy"],
                                    VendorDispatch = (int)r["VendorDispatch"],
                                    WorkOrdersAssignedTo = (int)r["WorkOrdersAssignedTo"],
                                    WorkOrdersCompletedBy = (int)r["WorkOrdersCompletedBy"],
                                    TotalBillable = (double)r["TotalBillable"],
                                    TotalHours = (string)r["TotalHours"]
                                });
                            }
                            catch (Exception ex) { model.Message = ex.Message; }
                        }
                        model.BillableItems = model.BillableItems.OrderByDescending(x => x.JobTitle).ThenByDescending(x => x.TotalBillable).ToList();
                        r.NextResult();

                        while (r.Read())
                        {
                            try
                            {
                                //var u = Users.Where(x => x.FullName == (string)r["CompletedBy"]).FirstOrDefault();
                                var t = Team.Employees.Where(x => x.DisplayName == (string)r["CompletedBy"]).FirstOrDefault();

                                model.NonBillableItems.Add(new Acme.Models.Reports.Productivity.MaintenanceNonBillableItem()
                                {
                                    CompletedBy = (string)r["CompletedBy"],
                                    //ProfilePicUrl = api.GetEmployeePhotoUrl("boris@acmehouseco.com"),
                                    //ProfilePicUrl = u != null ? u.PhotoUrl : string.Empty,
                                    ProfilePicUrl = t != null ? t.PhotoURL : string.Empty,
                                    Department = t != null ? t.Department : string.Empty,
                                    JobTitle = t != null ? t.JobTitle : string.Empty,
                                    GuestServices = (int)r["GuestServices"],
                                    GuestDamages = (int)r["GuestDamage"],
                                    PointCentral = (int)r["PointCentral"],
                                    NoiseAware = (int)r["NoiseAware"],
                                    EventSetup = (int)r["EventSetup"],
                                    WarrentyService = (int)r["WarrentyService"],
                                    TotalCost = (double)r["TotalCost"],
                                    TotalHours = (string)r["TotalHours"]
                                });
                            }
                            catch (Exception ex) { model.Message = ex.Message; }
                        }
                        model.NonBillableItems = model.NonBillableItems.OrderByDescending(x => x.TotalCost).ToList();
                    }
                }
                conn.Close();
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("spReportWorkOrdersSupplies", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", s.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", s.DateEnd);
                    //conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        //r.NextResult();
                        r.NextResult();
                        r.NextResult();
                        r.NextResult();
                        while (r.Read())
                        {
                            try
                            {
                                var test = false;
                                if ((string)r["CompletedBy"] == "David Bugos")
                                    test = true;
                                var t = Team.Employees.Where(x =>
                                        x.DisplayName == (string)r["CompletedBy"] ||
                                        x.PreferredName == (string)r["CompletedBy"]
                                    ).FirstOrDefault();

                                model.SummaryItemsByUser.Add(new Acme.Models.Reports.SuppliesListItem()
                                {
                                    WorkOrderTotal = (int)r["WorkOrderTotal"],
                                    CompletedBy = ((string)r["CompletedBy"]).Trim(),
                                    CompletedByDepartment = t != null ? t.Department : string.Empty,
                                    CompletedByJobTitle = t != null ? t.JobTitle : string.Empty,
                                    CompletedByPhotoUrl = t != null ? t.PhotoURL : string.Empty,
                                    UserID = t != null ? t.Id : 0,
                                    Quantity = (int)r["Quantity"],
                                    TotalCost = (double)r["TotalCost"]
                                });
                            }
                            catch (Exception ex) { model.Message = ex.Message; }
                        }
                        model.SummaryItemsByUser = model.SummaryItemsByUser.Where(x => x.CompletedByJobTitle.Contains("Property Care")).ToList();
                    }
                }
                conn.Close();
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("spWorkOrdersTimeToCompleteTotals", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", s.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", s.DateEnd);
                    using(var r = cmd.ExecuteReader())
                    {
                        for (int type = 0; type < 4; type++) {
                            var list = new List<Acme.Models.Reports.Productivity.WorkOrderTimeItem>();
                            while (r.Read())
                            {
                                try
                                {
                                    var t = Team.Employees.Where(x =>
                                        x.DisplayName == (string)r["CompletedBy"] ||
                                        x.PreferredName == (string)r["CompletedBy"]
                                    ).FirstOrDefault();

                                    list.Add(new Acme.Models.Reports.Productivity.WorkOrderTimeItem()
                                    {
                                        CompletedBy = ((string)r["CompletedBy"]).Trim(),
                                        CompletedByDepartment = t != null ? t.Department : string.Empty,
                                        CompletedByJobTitle = t != null ? t.JobTitle : string.Empty,
                                        CompletedByPhotoUrl = t != null ? t.PhotoURL : string.Empty,
                                        //WorkOrderType = (string)r["Type"],
                                        TotalHours = (int)r["TotalHours"],
                                        TotalMinutes = (int)r["TotalMins"],
                                        TotalSeconds = (int)r["TotalSecs"],
                                        TotalTimeBilled = (string)r["TotalTimeBilled"],
                                        TotalTimeChart = (double)r["TotalTimeChart"]
                                    });
                                }
                                catch(Exception ex) { }
                            }
                            switch (type)
                            {
                                case 0: model.TotalTimeBillable = list; break;
                                case 1: model.TimeNonBillable = list; break;
                                case 2: model.TimeGuestServices = list; break;
                                case 3: model.TimeBillable = list; break;
                            }
                            r.NextResult();
                        }
                    }
                }
                conn.Close();
            }
            return PartialView("tabs/_PropertyCare", model);
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult TabInspections(Acme.Models.Reports._ReportSearch s)
        {
            var model = new Acme.ViewModels.Reports.InspectionsViewModel();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spReportProductivityInspections", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", s.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", s.DateEnd);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                model.Summary = new Acme.Models.Reports.Productivity.Inspections.InspectionsSummary()
                                {
                                    Arrivals = (int)r["Arrivals"],
                                    ArrivalFails = (int)r["ArrivalFails"],
                                    Departures = (int)r["Departures"],
                                    DepartureFails = (int)r["DepartureFails"],
                                    Other = (int)r["Other"],
                                };
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return PartialView("tabs/_Inspections", model);
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult TabCleanings()
        {
            return PartialView("tabs/_Housekeeping");
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult TabSupplies(Acme.Models.Reports._ReportSearch s)
        {
            var model = new Acme.ViewModels.Reports.SupplyList();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spReportWorkOrdersSupplies", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", s.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", s.DateEnd);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                model.Items.Add(new Acme.Models.Reports.SuppliesListItem()
                                {
                                    WorkOrderTotal = (int)r["WorkOrderTotal"],
                                    Supply = (string)r["Supply"],
                                    Quantity = (int)r["Quantity"],
                                    UnitCost = (double)r["UnitCost"],
                                    TotalCost = (double)r["TotalCost"],
                                    TotalBilled = r["TotalBilled"].ToDouble(),
                                    IsLabor = r["IsLabor"].ToBoolean(),
                                    IsLocked = r["IsLocked"].ToBoolean(),
                                    IsEnteredSupply = r["IsEnteredSupply"].ToBoolean(),
                                });
                            }
                            catch (Exception ex) { }
                        }

                        r.NextResult();
                        while (r.Read())
                        {
                            try
                            {
                                var t = Team.Employees.Where(x =>
                                        x.DisplayName == (string)r["CompletedBy"] ||
                                        x.PreferredName == (string)r["CompletedBy"]
                                    ).FirstOrDefault();

                                model.ItemsByUser.Add(new Acme.Models.Reports.SuppliesListItem()
                                {
                                    WorkOrderTotal = (int)r["WorkOrderTotal"],
                                    CompletedBy = ((string)r["CompletedBy"]).Trim(),
                                    CompletedByDepartment = t != null ? t.Department : string.Empty,
                                    CompletedByJobTitle = t != null ? t.JobTitle : string.Empty,
                                    CompletedByPhotoUrl = t != null ? t.PhotoURL : GlobalSettings.Images.DefaultUser,
                                    UserID = t != null ? t.Id : 0,
                                    Supply = (string)r["Supply"],
                                    Quantity = (int)r["Quantity"],
                                    UnitCost = (double)r["UnitCost"],
                                    TotalCost = (double)r["TotalCost"],
                                    TotalBilled = r["TotalBilled"].ToDouble(),
                                    IsLabor = r["IsLabor"].ToBoolean(),
                                    IsLocked = r["IsLocked"].ToBoolean(),
                                    IsEnteredSupply = r["IsEnteredSupply"].ToBoolean(),
                                });
                            }
                            catch (Exception ex) { }
                        }

                        r.NextResult();
                        while (r.Read())
                        {
                            try
                            {
                                model.SupplyTotals = new Acme.Models.Reports.SupplyTotals()
                                {
                                    TotalCost = (double)r["TotalSupplyCost"],
                                    TotalOwnerBillable = (double)r["TotalOwnerBillable"],
                                    TotalOwnerBilled = (double)r["TotalOwnerBilled"],
                                    TotalNonOwnerBillable = (double)r["TotalNonOwnerBillable"],
                                    //TotalNetBilled = (double)r["TotalNetUnBilled"],
                                    //TotalGrossBilled = (double)r["TotalGrossUnBilled"],
                                    TotalNetBilled = (double)r["TotalOwnerBilled"] / (double)r["TotalSupplyCost"],
                                    TotalGrossBilled = (double)r["TotalOwnerBillable"] / (double)r["TotalSupplyCost"],
                                };
                            }
                            catch(Exception ex) { }
                        }

                        r.NextResult();
                        while (r.Read())
                        {
                            try
                            {
                                var t = Team.Employees.Where(x =>
                                        x.DisplayName == (string)r["CompletedBy"] ||
                                        x.PreferredName == (string)r["CompletedBy"]
                                    ).FirstOrDefault();

                                model.SummaryItemsByUser.Add(new Acme.Models.Reports.SuppliesListItem()
                                {
                                    WorkOrderTotal = (int)r["WorkOrderTotal"],
                                    CompletedBy = ((string)r["CompletedBy"]).Trim(),
                                    CompletedByDepartment = t != null ? t.Department : string.Empty,
                                    CompletedByJobTitle = t != null ? t.JobTitle : string.Empty,
                                    CompletedByPhotoUrl = t != null ? t.PhotoURL : GlobalSettings.Images.DefaultUser,
                                    UserID = t != null ? t.Id : 0,
                                    Quantity = (int)r["Quantity"],
                                    TotalCost = (double)r["TotalCost"],
                                    TotalBilled = r["TotalBilled"].ToDouble(),
                                    IsLabor = r["IsLabor"].ToBoolean()
                                });
                            }
                            catch (Exception ex) { }
                        }
                        r.NextResult();
                        while (r.Read())
                        {
                            try
                            {
                                var t = Team.Employees.Where(x =>
                                    x.DisplayName == (string)r["CompletedBy"] ||
                                    x.PreferredName == (string)r["CompletedBy"]
                                ).FirstOrDefault();
                                model.ItemsManuallyEntered.Add(new Acme.Models.Reports.SupplyItemManualEntry()
                                {
                                    ID = r["ID"].ToInt32(),
                                    ItemDescription = r["ItemDescription"].ToString(),
                                    BillTo = r["BillTo"].ToString(),
                                    ItemType = r["ItemType"].ToString(),
                                    Type = r["Type"].ToString(),
                                    Completed = r["Completed"].ToDateTime(),
                                    CompletedBy = r["CompletedBy"].ToString(),
                                    TimeToComplete = r["TimeToComplete"].ToString(),
                                    SupplyQuantity = r["SupplyQuantity"].ToInt32(),
                                    ItemCost = r["ItemCost"].ToDouble(),
                                    ItemTotal = r["ItemTotal"].ToDouble(),
                                    LineTotal = r["LineTotal"].ToDouble(),
                                    IsLocked = r["IsLocked"].ToBoolean(),
                                    IsTaxable = r["IsTaxable"].ToBoolean(),
                                    IsLabor = r["IsLabor"].ToBoolean()
                                });
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return PartialView("tabs/_Supplies", model);
       }

        [CompressContent]
        [MinifyHtml]
        public ActionResult FetchSummary()
        {
            return PartialView("tabs/_Summary");
        }
        #endregion


        #region GoZone
        public ActionResult GoZone()
        {
            return View();
        }
        #endregion GoZone


        #region Revenue Projections
        public ActionResult RevenueProjections()
        {
            return View();
        }
        #endregion Revenue Projections

        public ActionResult TestStudio()
        {
            return View();
        }
        public ActionResult AcmeMain()
        {
            return View();
        }
        public ActionResult BreezewayReporting()
        {
            return View();
        }
        public ActionResult Bugs()
        {
            return View();
        }
    }
}