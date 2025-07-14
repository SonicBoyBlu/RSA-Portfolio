using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Runtime.Caching;

namespace Acme.Methods
{
    public class WorkOrders
    {
        public static List<Models.WorkOrderSummaryID> GetSummaryIDs()
        {
            var ids = new List<Models.WorkOrderSummaryID>();
            using (var conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                var cmd = new SqlCommand("spSummaryIdsGet", conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        try
                        {
                            ids.Add(new Models.WorkOrderSummaryID() {
                                    ID = (int)r["ID"],
                                    IsApproved = (bool)r["IsApproved"],
                                    IsLocked = (bool)r["IsLocked"]
                                }
                            );
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            return ids;
        }

        public static ViewModels.WorkOrdersImportViewModel GetWorkOrders(DateTime DateStart, DateTime DateEnd, bool IsQboExport, string Type, bool ShowCommitted = false)
        {
            var model = new ViewModels.WorkOrdersImportViewModel();
            int errorCount = 0;
            DateTime minMax = DateTime.Now.AddMinutes(-1);
            if (System.Diagnostics.Debugger.IsAttached) minMax = DateTime.Now.AddMinutes(3);

            string CacheKeyWorkOrders = "WorkOrderCache";
            ObjectCache cache = MemoryCache.Default;
            if (cache.Contains(CacheKeyWorkOrders))
                model = (ViewModels.WorkOrdersImportViewModel)cache.Get(CacheKeyWorkOrders);
            else
            {
                using (var conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                {
                    var cmd = new SqlCommand("spWorkOrdersGet", conn) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@dateStart", DateStart);
                    cmd.Parameters.AddWithValue("@DateEnd", DateEnd);
                    cmd.Parameters.AddWithValue("@IsQboExport", IsQboExport);
                    cmd.Parameters.AddWithValue("@showCommitted", ShowCommitted);
                    cmd.Parameters.AddWithValue("@type", string.IsNullOrEmpty(Type) ? DBNull.Value : (object)Type);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        //var test = new Helpers.GenericPopulator<Models.Breezeway.TaskSummary>().CreateList(r);
                        while (r.Read())
                        {
                            try
                            {
                                model.taskSummaries.Add(new Models.Breezeway.TaskSummary()
                                {
                                    SummaryID = (int)r["SummaryID"],
                                    Type = (string)r["Type"],
                                    ID = (int)r["ID"],
                                    Property = (string)r["Property"],
                                    PropertyID = r["PropertyID"] == DBNull.Value ? 0 : (int)r["PropertyID"],
                                    Name = (string)r["Name"],
                                    CreatedBy = (string)r["CreatedBy"],
                                    Status = (string)r["Status"],
                                    Created = r["Created"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Created"],
                                    Updated = r["Updated"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Updated"],
                                    Started = r["Started"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Started"],
                                    Assigned = r["Assigned"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Assigned"],
                                    AssignedTo = (string)r["AssignedTo"],
                                    AssignedDate = r["AssignedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["AssignedDate"],
                                    Completed = r["Completed"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Completed"],
                                    CompletedBy = (string)r["CompletedBy"],
                                    EstimateTimeToComplete = (string)r["EstimateTimeToComplete"],
                                    TimeToComplete = (string)r["TimeToComplete"],
                                    RatePaid = (double)r["RatePaid"],
                                    RateType = (string)r["RateType"],
                                    Description = (string)r["Description"],
                                    TotalCost = (double)r["TotalCost"],

                                    CostDescription = (string)r["CostDescription"],
                                    BillTo = (string)r["BillTo"],
                                    ReportSummary = (string)r["ReportSummary"],
                                    RequestedBy = (string)r["RequestedBy"],
                                    IsInvoicable = (bool)r["IsInvoicable"],
                                    //*
                                    DateQboUploaded = r["DateQboUploaded"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateQboUploaded"],
                                    QuickBooksCustomerID = r["QuickBooksCustomerID"] == DBNull.Value ? 0 : (int)r["QuickBooksCustomerID"],
                                    QuickBooksCustomerName = r["FullyQualifiedName"] == DBNull.Value ? string.Empty : (string)r["FullyQualifiedName"],
                                    // How to handle Properties not in Escapia??
                                    UnitCode = r["PropertyMarketingID"] == DBNull.Value ? string.Empty : (string)r["PropertyMarketingID"],

                                    IsIgnore = (bool)r["IsIgnore"],
                                    IsLocked = Identity.Current.UserType == DataTypes.UserType.Employee ? (bool)r["IsLocked"] : true
                                    //*/
                                }); ;
                            }
                            catch (Exception ex) { errorCount++; }
                        }
                        var count = errorCount;
                        /*
                        r.NextResult();
                        while (r.Read())
                        {
                            try
                            {
                                model.taskSupplies.Add(new Models.Breezeway.TaskSupplies()
                                {
                                    SupplyID = (int)r["SupplyID"],
                                    Type = (string)r["Type"],
                                    ID = (int)r["ID"],
                                    Property = (string)r["Property"],
                                    Name = (string)r["Name"],
                                    AssignedDate = r["AssignedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["AssignedDate"],
                                    Completed = r["Completed"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Completed"],
                                    Supply = (string)r["Supply"],
                                    Description = (string)r["Description"],
                                    Quantity = (int)r["Quantity"],
                                    UnitCost = (double)r["UnitCost"],
                                    TotalCost = (double)r["TotalCost"],
                                    IsTaxable = (bool)r["IsTaxable"],
                                    ReportSummary = (string)r["ReportSummary"],
                                    //LineItemDetail = (string)r["LineItemDetail"]
                                });
                            }
                            catch (Exception ex) { }
                        }
                        */

                        r.NextResult();
                        while (r.Read())
                        {
                            try
                            {
                                model.taskCosts.Add(new Models.Breezeway.TaskCost()
                                {
                                    CostID = (int)r["CostID"],
                                    Type = (string)r["Type"],
                                    ID = (int)r["ID"],
                                    Property = (string)r["Property"],
                                    Name = (string)r["Name"],
                                    Started = r["Started"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Started"],
                                    Assigned = r["Assigned"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Assigned"],
                                    AssignedTo = (string)r["AssignedTo"],
                                    AssignedDate = r["AssignedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["AssignedDate"],
                                    Completed = r["Completed"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Completed"],
                                    CompletedBy = (string)r["CompletedBy"],
                                    TimeToComplete = (string)r["TimeToComplete"],
                                    Description = (string)r["Description"],
                                    ReportSummary = (string)r["ReportSummary"],
                                    Quantity = (int)r["SupplyQuantity"],
                                    ItemType = (string)r["ItemType"],
                                    ItemDescription = (string)r["ItemDescription"],
                                    ItemCost = (double)r["ItemCost"],
                                    BillTo = (string)r["BillTo"],
                                    ItemTotal = r["ItemTotal"] == DBNull.Value ? 0 : (double)r["ItemTotal"],
                                    LineTotal = (double)r["LineTotal"],
                                    IsTaxable = (bool)r["IsTaxable"]
                                });
                            }
                            catch (Exception ex) { }
                        }

                        // TODO: Discount rows
                        //r.NextResult();
                        /*
                        while (r.Read())
                        {
                            try
                            {
                                var stop = false;
                                if ((int)r["ID"] == 1086227 || (int)r["ID"] == 890183)
                                    stop = true;
                                model.taskSupplies.Add(new Models.Breezeway.TaskSupplies()
                                {
                                    SupplyID = (int)r["SupplyID"],
                                    Type = (string)r["Type"],
                                    ID = (int)r["ID"],
                                    Property = (string)r["Property"],
                                    Name = (string)r["Name"],
                                    AssignedDate = r["AssignedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["AssignedDate"],
                                    Completed = r["Completed"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Completed"],
                                    Supply = (string)r["Supply"],
                                    Description = (string)r["Description"],
                                    Quantity = (int)r["Quantity"],
                                    UnitCost = (double)r["UnitCost"],
                                    TotalCost = (double)r["TotalCost"],
                                    IsTaxable = (bool)r["IsTaxable"],
                                    ReportSummary = (string)r["ReportSummary"],
                                    //LineItemDetail = (string)r["LineItemDetail"]
                                });
                            }
                            catch (Exception ex) { }
                        }
                        */
                    }
                }

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = minMax;
                //cache.Add(CacheKeyWorkOrders, model, policy);
            }
            return model;
        }

        public static void ImportQuickBooksCustomers()
        {
            var Customers = new List<Models.QuickBooks.Customer>();

            try
            {
                /*
                using (var conn = new OdbcConnection(ConfigurationManager.ConnectionStrings["QuickBooks"].ConnectionString))
                {
                    OdbcCommand cmd;

                    string query = "select * FROM Customer";
                    //string query = "select * FROM Customer c WHERE(DisplayName LIKE '%PD1733')";
                    conn.Open();
                    cmd = new OdbcCommand(query, conn);
                    var r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        Customers.Add(new Models.QuickBooks.Customer()
                        {
                            ID = int.Parse((string)r["ID"]),
                            IsActive = (bool)r["Active"],
                            Title = r["Title"] == DBNull.Value ? "" : (string)r["Title"],
                            FirstName = r["GivenName"] == DBNull.Value ? "" : (string)r["GivenName"],
                            MiddleName = r["MiddleName"] == DBNull.Value ? "" : (string)r["MiddleName"],
                            LastName = r["FamilyName"] == DBNull.Value ? "" : (string)r["FamilyName"],
                            FullyQualifiedName = r["FullyQualifiedName"] == DBNull.Value ? "" : (string)r["FullyQualifiedName"],
                            CompanyName = r["CompanyName"] == DBNull.Value ? "" : (string)r["CompanyName"],
                            DisplayName = r["DisplayName"] == DBNull.Value ? "" : (string)r["DisplayName"],
                            PrimaryPhone = r["PrimaryPhone_FreeFormNumber"] == DBNull.Value ? "" : (string)r["PrimaryPhone_FreeFormNumber"],
                            AlertnatePhone = r["AlternatePhone_FreeFormNumber"] == DBNull.Value ? "" : (string)r["AlternatePhone_FreeFormNumber"],
                            MobilePhone = r["Mobile_FreeFormNumber"] == DBNull.Value ? "" : (string)r["Mobile_FreeFormNumber"],
                            Email = r["PrimaryEmailAddr_Address"] == DBNull.Value ? "" : (string)r["PrimaryEmailAddr_Address"],
                            Notes = r["Notes"] == DBNull.Value ? "" : (string)r["Notes"]
                        });
                    }
                    conn.Close();
                }
                */
                using (var conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                {
                    var cmd = new SqlCommand("spQuickbooksSync", conn) { CommandType = CommandType.StoredProcedure };
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    /*
                    var cmd = new SqlCommand("spImportQuickBooksCustomer", conn) { CommandType = CommandType.StoredProcedure };
                    conn.Open();
                    foreach (var c in Customers)
                    {
                        try
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID", c.ID);
                            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                            cmd.Parameters.AddWithValue("@MiddleName", c.MiddleName);
                            cmd.Parameters.AddWithValue("@LastName", c.LastName);
                            cmd.Parameters.AddWithValue("@CompanyName", c.CompanyName);
                            cmd.Parameters.AddWithValue("@FullyQualifiedName", c.FullyQualifiedName);
                            cmd.Parameters.AddWithValue("@DisplayName", c.DisplayName);
                            cmd.Parameters.AddWithValue("@Title", c.Title);
                            cmd.Parameters.AddWithValue("@PrimaryPhone", c.PrimaryPhone);
                            cmd.Parameters.AddWithValue("@AlertnatePhone", c.AlertnatePhone);
                            cmd.Parameters.AddWithValue("@MobilePhone", c.MobilePhone);
                            cmd.Parameters.AddWithValue("@Email", c.Email);
                            cmd.Parameters.AddWithValue("@Notes", c.Notes);
                            cmd.Parameters.AddWithValue("@IsActive", c.IsActive);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex) { }
                    }
                    */
                    conn.Close();
                }
            }
            catch (Exception ex) { }
        }
    }
}