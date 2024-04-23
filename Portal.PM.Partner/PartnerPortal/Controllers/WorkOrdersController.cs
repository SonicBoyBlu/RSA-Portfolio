using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Web.Script.Serialization;
using System.Text;
using System.IO.Compression;
using WebMarkupMin.AspNet4.Mvc;
using System.Runtime.Caching;
using Acme.Services.Accounting.QuickBooks;
using Acme.Data.Repositories;
using Acme.ViewModels;
using Acme.Services.Accounting;

namespace Acme.Controllers
{
    public class WorkOrdersController : Controller
    {
        // GET: WorkOrders
        #region Private properties
        private UserIdentity me { get { return Identity.Current; } }
        private static string ErrorMessage;
        private static DataTable CsvSummaries;

        private static Models.Toast Toast { get; set; }
        private static bool IsImportAllComplete { get; set; }
        private static bool IsImportUploadComplete { get; set; }
        private static bool IsImportSummaryComplete { get; set; }
        private static bool IsImportCostComplete { get; set; }
        private static bool IsImportQboComplete { get; set; }
        private static bool IsImportPostProcessComplete { get; set; }
        private static int TotalSummaries { get; set; }
        private static int TotalCosts { get; set; }
        private static int CompletedSummaries { get; set; }
        private static int CompletedTotalCosts { get; set; }


        private bool IsSpa {
            get
            {
                bool spa = false;
                return spa;
            }
        }
        #endregion Private properties

        public ActionResult Latest()
        {
            if (me.UserType == DataTypes.UserType.Employee)
            {
                if (IsSpa)
                    return PartialView();
                else
                    return View();
            }
            else
                return null;
        }



        #region Views
        public ActionResult Index()
        {
            var srv = new WorkOrderRepository();
            DateTime now = DateTime.Now;
            var wo = srv.GetWorkOrders(now.AddYears(-1), now, false, "Property Care", false);
            DateTime oldest = wo.dateOldest;

            if (me.UserType == DataTypes.UserType.Employee)
                if (IsSpa)
                    return PartialView();
                else
                    return View(oldest);
            else return null;
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult FetchPreview(DateTime DateStart, DateTime DateEnd, string Query, string Type, bool Init = false, bool ShowCommitted = false, int Filter = 1)
        {
            if (me.UserType == DataTypes.UserType.Employee)
            {
                var model = new ViewModels.WorkOrdersPreviewViewModel()
                {
                    DateStart = DateStart,
                    DateEnd = DateEnd,
                    Query = string.IsNullOrEmpty(Query) ? string.Empty : Query,
                    Type = string.IsNullOrEmpty(Type) ? string.Empty : Type,
                    Filter = Filter,
                    ShowCommitted = ShowCommitted
                };

                if (Init)
                {
                    // Reconcile QuickBooks Customers
                    //Methods.WorkOrders.ImportQuickBooksCustomers();
                    //PostImortCleanup();
                }
                WorkOrderRepository workOrderRepository = new WorkOrderRepository();

                var wo = workOrderRepository.GetWorkOrders(DateStart, DateEnd, false, Type, ShowCommitted);

                //var wo = Methods.WorkOrders.GetWorkOrders(DateStart, DateEnd, false, Type, ShowCommitted);

                //TODO: after test, select top 1 for 294 and create live invoice
                //var test = TestQbInvoiceInsert();
                //var test = invoices.Where(i => i.CustomerRefID == 294).ToList();
                model.Invoices = MapToInvoices(wo);

                //QbInvoiceExportCSV(test);
                //QbInvoiceExportCSV(invoices);
                return PartialView("partials/_InvoicePreview", model);
            }
            else return null;
        }

        private List<Models.QuickBooks.QboInvoice> MapToInvoices(WorkOrdersImportViewModel wo)
        {
            List<Models.QuickBooks.QboInvoice> invoices = new List<Models.QuickBooks.QboInvoice>();
            foreach (var w in wo.taskSummaries)
            {
                try
                {
                    var costs = wo.taskCosts.Where(c => c.ID == w.ID);
                    var items = new List<Models.QuickBooks.QboInvoiceLineItem>();
                    int idx = 1;
                    double totalCost = costs.Sum(x => x.LineTotal);

                    foreach (var s in costs)
                    {
                        items.Add(new Models.QuickBooks.QboInvoiceLineItem()
                        {
                            LineNum = idx,
                            Description = string.IsNullOrEmpty(s.ItemDescription) ? s.ReportSummary : s.ItemDescription,
                            SalesItemLineDetail_ItemRefId = (s.IsTaxable ? 575 : 139).ToString(),
                            SalesItemLineDetail_ItemRefName = (s.IsTaxable ? "Materials" : "Labor"),
                            SalesItemLineDetail_UnitPrice = s.ItemTotal,
                            SalesItemLineDetail_Qty = s.Quantity,
                            Amount = s.ItemTotal,
                            SalesItemLineDetail_TaxCodeRefId = (s.IsTaxable ? "TAX" : "NON"),
                            SalesItemLineDetail_ServiceDate = s.Completed, //.ToString("yyyy-MM-dd"),
                            Total = s.LineTotal
                        });
                        idx++;
                    }

                    invoices.Add(new Models.QuickBooks.QboInvoice()
                    {
                        Id = w.ID.ToString(),
                        CustomerRefID = w.QuickBooksCustomerID,
                        CustomerName = !string.IsNullOrEmpty(w.QuickBooksCustomerName) ? w.QuickBooksCustomerName : string.Empty,
                        InvoiveNum = w.ID.ToString(),
                        Type = w.Type,
                        BreezewayStatus = w.Status,

                        Line = (new JavaScriptSerializer().Serialize(items)),
                        LineItems = items,
                        TxnDate =
                            items.Count == 0 ? w.Completed :
                            items.OrderByDescending(i => i.SalesItemLineDetail_ServiceDate).FirstOrDefault().SalesItemLineDetail_ServiceDate,
                        BillTo = string.IsNullOrEmpty(w.BillTo) ? "Unknown" : w.BillTo,
                        UnitCode = w.UnitCode,
                        Description = w.Description, // w.Name,
                        Resolution = w.ReportSummary,
                        TotalAmount = totalCost, //w.TotalCost,
                        IsIgnore = w.IsIgnore,
                        IsLocked = w.IsLocked,
                        DateQboCommitted = w.DateQboUploaded
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }
            return invoices;
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult FetchPreviewCards (DateTime DateStart, DateTime DateEnd, string Query, string Type)
        {
            if (me.UserType == DataTypes.UserType.Employee)
            {
                var model = new ViewModels.WorkOrdersPreviewViewModel()
                {
                    DateStart = DateStart,
                    DateEnd = DateEnd,
                    Query = string.IsNullOrEmpty(Query) ? string.Empty : Query,
                    Type = string.IsNullOrEmpty(Type) ? string.Empty : Type
                };

                WorkOrderRepository workOrderRepository = new WorkOrderRepository();
                var wo = workOrderRepository.GetWorkOrders(DateStart, DateEnd, false, Type);

                //var wo = Methods.WorkOrders.GetWorkOrders(DateStart, DateEnd, false, Type);
                return PartialView("partials/_InvoicePreviewCards", wo);
            }
            else return null;
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult FetchErrorLog()
        {
            List<Models.Breezeway.ImportErrors> errors = new List<Models.Breezeway.ImportErrors>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("spWorkOrderErrorsLogGet", conn) { CommandType = CommandType.StoredProcedure })
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                string raw = (string)r[0];
                                errors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Breezeway.ImportErrors>>(raw);
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            return PartialView("partials/_TaskImportErrorLog", errors);
        }
        #endregion Views


        #region AJAX actions
        [CompressContent]
        [MinifyHtml]
        public JsonNetResult ProgressPoll(bool Reset = false)
        {
            RenderProgressImportWorkOrders();
            return new JsonNetResult(new { res = Toast });
        }


        public JsonNetResult ImportWorkOrdersTest(bool IsForceRetry = false)
        {
            if (me.UserType == DataTypes.UserType.Employee)
            {
                Toast = new Models.Toast()
                {
                    IsProcessing = true,
                    ToastrClass = IsForceRetry ? Toast.ToastrClass : "toast-warning",
                    Title = IsForceRetry ? "Error Checking..." : "Initializing...",
                    Body = IsForceRetry ? Toast.Body : "Upload in progress"
                };
                IsImportUploadComplete = true;
                var files = Request.Files;

                DataTable csvSummary = new DataTable();
                DataTable csvSupplies = new DataTable();
                DataTable csvCost = new DataTable();
                DataTable csvProperies = new DataTable();
                DataTable csvOwners = new DataTable();

                string error = string.Empty;
                string message = string.Empty;
                bool success = false;

                if (files.Count > 0)
                {
                    var model = new ViewModels.WorkOrdersImportViewModel();

                    var file = files[0];
                    for (int f = 0; f < files.Count; f++)
                    {
                        file = files[f];
                        if (file != null && file.ContentLength > 0)
                        {
                        }
                    }

                    Toast.Title = "Processing Work Orders";
                    //bool valid = BulkCopyToSQL("TaskSummaries", csvSummary, IsForceRetry);
                    bool valid = true;
                    IsImportSummaryComplete = valid;

                    // For Supplies 1 off import
                    if (CsvSummaries == null) valid = true;

                    message += " TaskSummaries: " + valid;
                    if (valid)
                    {
                        Toast.Title = "Processing Line Items";
                        //message += "\nTaskSupplies: " + BulkCopyToSQL("TaskSupplies", csvSupplies);
                        message += "\nTakeCost: " + BulkCopyToSQL("TaskCosts", csvCost, IsForceRetry);
                        IsImportCostComplete = true;


                        Toast.Title = "Syncing with QuickBooks";
                        //Methods.WorkOrders.ImportQuickBooksCustomers();
                        IsImportQboComplete = true;
                        Toast.Title = "Post-Import Clean Up";
                        //PostImortCleanup();
                        //ExpireCache();
                        IsImportPostProcessComplete = true;
                    }
                    else
                    {
                        error = ErrorMessage;
                        Toast.ToastrClass = "toast-error";
                        Toast.Title = "An Import Error Occured";
                        Toast.Body = "Initiating Failover...";
                    }
                    message += string.Format("\nCSV loaded: {0} summary rows, {2} cost rows, {1} supply rows. ", csvSummary.Rows.Count, csvSupplies.Rows.Count, csvCost.Rows.Count);

                    //*/
                    success = valid;
                }
                else error = "No files uploaded";



                //ProcessQuickBooksInvoices();


                return new JsonNetResult(new
                {
                    error = error,
                    message = message,
                    success = success
                });
            }
            else return null;
        }

        public JsonNetResult ImportWorkOrders (bool IsForceRetry = false)
        {
            if (me.UserType == DataTypes.UserType.Employee)
            {
                Toast = new Models.Toast()
                {
                    IsProcessing = true,
                    ToastrClass = IsForceRetry ? Toast.ToastrClass : "toast-warning",
                    Title = IsForceRetry? "Error Checking..." : "Initializing...",
                    Body = IsForceRetry ? Toast.Body :  "Upload in progress"
                };
                IsImportUploadComplete = true;
                var files = Request.Files;
                DataTable csvSummary = new DataTable();
                DataTable csvSupplies = new DataTable();
                DataTable csvCost = new DataTable();
                DataTable csvProperies = new DataTable();
                DataTable csvOwners = new DataTable();

                string error = string.Empty;
                string message = string.Empty;
                bool success = false;

                if (files.Count > 0)
                {
                    var model = new ViewModels.WorkOrdersImportViewModel();

                    var file = files[0];
                    for (int f = 0; f < files.Count; f++)
                    {
                        file = files[f];
                        if (file != null && file.ContentLength > 0)
                        {
                            try
                            {
                                var stream = file.InputStream;
                                using (CsvReader r = new CsvReader(new StreamReader(stream), true))
                                {
                                    if (file.FileName.ToLower().IndexOf("summary") > -1)
                                    {
                                        csvSummary.Load(r);
                                        CsvSummaries = csvSummary;
                                    }
                                    /*
                                    else if (file.FileName.ToLower().IndexOf("supplies") > -1)
                                    {
                                        csvSupplies.Load(r);
                                    }
                                    */
                                    else if (file.FileName.ToLower().IndexOf("cost") > -1)
                                    {
                                        csvCost.Load(r);
                                    }
                                    else if (file.FileName.ToLower().IndexOf("property") > -1)
                                    {
                                        csvProperies.Load(r);
                                    }
                                    else if (file.FileName.ToLower().IndexOf("owner") > -1)
                                    {
                                        csvOwners.Load(r);
                                    }
                                }
                            }
                            catch (Exception ex) { error += ex.Message; }
                        }
                        else error += string.Format("File {0} is invalid.", f);
                    }

                    Toast.Title = "Processing Work Orders";
                    bool valid = BulkCopyToSQL("TaskSummaries", csvSummary, IsForceRetry);
                    IsImportSummaryComplete = valid;
                    
                    // For Supplies 1 off import
                    if (CsvSummaries == null) valid = true;

                    message += " TaskSummaries: " + valid;
                    if (valid)
                    {
                        Toast.Title = "Processing Line Items";
                        //message += "\nTaskSupplies: " + BulkCopyToSQL("TaskSupplies", csvSupplies);
                        message += "\nTakeCost: " + BulkCopyToSQL("TaskCosts", csvCost, IsForceRetry);
                        IsImportCostComplete = true;


                        Toast.Title = "Syncing with QuickBooks";
                        Methods.WorkOrders.ImportQuickBooksCustomers();
                        IsImportQboComplete = true;
                        Toast.Title = "Post-Import Clean Up";
                        PostImortCleanup();
                        ExpireCache();
                        IsImportPostProcessComplete = true;
                    }
                    else
                    {
                        error = ErrorMessage;
                        Toast.ToastrClass = "toast-error";
                        Toast.Title = "An Import Error Occured";
                        Toast.Body = "Initiating Failover...";
                    }
                    message += string.Format("\nCSV loaded: {0} summary rows, {2} cost rows, {1} supply rows. ", csvSummary.Rows.Count, csvSupplies.Rows.Count, csvCost.Rows.Count);

                    //*/
                    success = valid;
                }
                else error = "No files uploaded";



                //ProcessQuickBooksInvoices();


                return new JsonNetResult(new
                {
                    error = error,
                    message = message,
                    success = success
                });
            }
            else return null;
        }

        public FileResult DownloadQuickBooksCSV(DateTime DateStart, DateTime DateEnd)
        {
            if(DateStart == DateTime.MinValue || DateEnd == DateTime.MinValue)
            {
                var test = true;
            }

            var data = ProcessQuickBooksInvoices(DateStart, DateEnd);
            string zipname = DateTime.Now.ToString("yyyyMMddhhmm") + ".zip";
            return File(data, "text/csv", zipname);
            //return new JsonNetResult(new { status = status });
        }
        #endregion AJAX actions


        #region Helper methods
        private static void RenderProgressImportWorkOrders()
        {
            var tmp = Toast;
            bool isDone = IsImportUploadComplete & IsImportSummaryComplete & IsImportQboComplete & IsImportPostProcessComplete;


            string prog = string.Format(@"
                <i class='{0}'></i> Upload Files<br />
                <i class='{1}'></i> Work Orders<br />
                <i class='{2}'></i> Line Items<br />
                <i class='{3}'></i> QBO Customer Sync<br />
                <i class='{4}'></i> Post-Process Cleanup<br />
                <i class='{5}'></i> Done
                ",
                IsImportUploadComplete ? "fa fa-check-circle text-success" : "fas fa-circle-notch fa-spin",
                IsImportSummaryComplete ? "fa fa-check-circle text-success" : "fas fa-circle-notch fa-spin",
                IsImportCostComplete ? "fa fa-check-circle text-success" : "fas fa-circle-notch fa-spin",
                IsImportQboComplete ? "fa fa-check-circle text-success" : "fas fa-circle-notch fa-spin",
                IsImportPostProcessComplete ? "fa fa-check-circle text-success" : "fas fa-circle-notch fa-spin",
                isDone ? "fa fa-check-circle text-success" : "fas fa-circle-notch fa-spin"
            );
            Toast = new Models.Toast()
            {
                IsProcessing = tmp.IsProcessing,
                ToastrClass = tmp.ToastrClass,
                Title = isDone ? "Import Complete" : tmp.Title,
                Body = prog
            };
        }
        private static bool BulkCopyToSQL(string name, DataTable dataTable, bool IsForceDebug = false)
        {
            bool res = false;
            DataTable dt = dataTable.Clone();
            //dt = dataTable;
            //dt.Rows.Clear();

            if (name.ToLower() == "properties")
            {
                var props = Methods.Properties.GetProperties();

                for (int r = 0; r < dataTable.Rows.Count; r++)
                {
                    var row = dataTable.Rows[r];
                    if (props.Where(p => p.EscapiaID.ToString() == (string)row.ItemArray[0]).Count() == 0)
                        dt.ImportRow(row);
                }
            } else if (new [] { "summaries", "supplies0", "cost" }.Any(t => t.Contains(t))) //   (name.ToLower() == "tasksummaries")
            {
                // Clean up summary datatable
                if (name.ToLower() == "tasksummaries")
                {
                    var ids = Methods.WorkOrders.GetSummaryIDs();
                    var purge = new List<string>();
                    DataTable purgeIDs = new DataTable();
                    purgeIDs.Columns.Add("ID", typeof(int));



                    for (int r = 0; r < dataTable.Rows.Count; r++)
                    {
                        var row = dataTable.Rows[r];
                        int id = 0;
                        int.TryParse(row.ItemArray[1].ToString(), out id);
                        var items = ids.Where(i => i.ID == id);
                        if (items.Count() == 0)
                            dt.ImportRow(row);
                        else
                        {
                            var item = items.First();
                            // if item was not previously imported as approved, import the data row and purge the row from the database
                            if (!item.IsLocked)
                            {
                                if (!item.IsApproved)
                                {
                                    var dr = purgeIDs.NewRow();
                                    dr["ID"] = item.ID;
                                    purgeIDs.Rows.Add(dr);
                                    dt.ImportRow(row);
                                }
                            }
                        }
                    }

                    // execute SQL purge or non-approved duplicates
                    if (purgeIDs.Rows.Count > 0)
                    {
                        string purgeList = string.Empty;
                        using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                        {
                            var cmd = new SqlCommand("spWorkOrderPreProcessCleanup", conn) { CommandType = CommandType.StoredProcedure };
                            cmd.Parameters.AddWithValue("@ids", purgeIDs);
                            conn.Open();
                            try
                            {
                                var r = cmd.ExecuteReader();
                                while (r.Read())
                                {
                                    purgeList += (string.IsNullOrEmpty(purgeList) ? "" : ",") + (int)r["ID"];

                                }
                            }
                            catch (Exception ex) { ErrorMessage = ex.Message; }
                        }
                    }
                }
                else
                {
                    if (CsvSummaries != null)
                    {
                        List<string> ids = new List<string>();
                        for (var r = 0; r < CsvSummaries.Rows.Count; r++)
                        {
                            ids.Add(CsvSummaries.Rows[r].ItemArray[1].ToString());
                        }



                        for (int r = 0; r < dataTable.Rows.Count; r++)
                        {
                            var row = dataTable.Rows[r];
                            var items = ids.Where(i => i == row.ItemArray[1].ToString());
                            if (items.Count() > 0)
                            {
                                using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                                {
                                    using(SqlCommand cmd = new SqlCommand("spWorkOrderItemsCleanup", conn) { CommandType = CommandType.StoredProcedure })
                                    {
                                        cmd.Parameters.AddWithValue("@id", int.Parse(row.ItemArray[1].ToString()));
                                        conn.Open();
                                        try
                                        {
                                            using(var reader = cmd.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    /*
                                                    if(int.Parse(row.ItemArray[1].ToString()) == 1010950)
                                                    {
                                                        var test = true;
                                                    }
                                                    */
                                                    if ((bool)reader["IsValid"])
                                                        dt.ImportRow(row);
                                                }
                                            }
                                        }
                                        catch(Exception ex) { ErrorMessage = ex.Message; }
                                        conn.Close();
                                    }
                                }
                            }
                        }
                    } else
                        dt = dataTable;
                }
            }
            else dt = dataTable;

            if (dt.Rows.Count > 0)
            {
                bool debug = IsForceDebug;
                if (!debug)
                {
                    using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                    {
                        using (var copy = new SqlBulkCopy(conn))
                        {
                            try
                            {
                                conn.Open();
                                copy.DestinationTableName = name;
                                copy.BatchSize = dt.Rows.Count;
                                copy.BulkCopyTimeout = 60;
                                copy.WriteToServer(dt);
                                copy.Close();
                                res = true;
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage = ex.Message;
                            }
                        }
                    }
                }
                else
                {
                    IsImportSummaryComplete = false;
                    IsImportCostComplete = false;
                    IsImportQboComplete = false;
                    RenderProgressImportWorkOrders();
                    Toast.ToastrClass = "toast-warning";
                    Toast.Title = "An error has occurred.";


                    //res = true;
                    List<DataRow> list = dt.AsEnumerable().ToList();
                    List<object> ints = new List<object>();

                    foreach (var item in list)
                    {

                        int id = 0;
                        int pid = 0;
                        int intid = 0;
                        double rate = 0;
                        double cost = 0;

                        try
                        {
                            int.TryParse(item.ItemArray[1].ToString(), out id);
                            int.TryParse(item.ItemArray[3].ToString(), out pid);
                            if (!string.IsNullOrEmpty(item.ItemArray[5].ToString()))
                                int.TryParse(item.ItemArray[5].ToString(), out intid);
                            double.TryParse(item.ItemArray[21].ToString(), out rate);
                            double.TryParse(item.ItemArray[23].ToString(), out cost);
                            ints.Add(new { id = id, pid = pid, intid, rate, cost });
                        }
                        catch (Exception ex)
                        {
                            LogImportErrors(id, name, ex.Message);
                        }
                    }


                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        var item = dt.Rows[i];
                        using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                        {
                            using (var copy = new SqlBulkCopy(conn))
                            {
                                try
                                {
                                    conn.Open();

                                    DataTable singleDt = dt.Clone(); // new DataTable();
                                    singleDt.ImportRow(item);

                                    copy.DestinationTableName = name;
                                    copy.BatchSize = singleDt.Rows.Count;
                                    copy.BulkCopyTimeout = 60;
                                    copy.WriteToServer(singleDt);
                                    copy.Close();
                                    res = true;
                                }
                                catch (Exception ex)
                                {
                                    var errorRow = item;
                                    ErrorMessage = ex.Message;
                                    if (!ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                                        LogImportErrors(int.Parse(item.ItemArray[1].ToString()), name, ex.Message);
                                }
                            }
                        }

                    }
                }
            }
            else if (dt.Rows.Count == 0) res = true;
            return res;
        }

        private static void LogImportErrors(int ID, string Source, string Error, string Meta = "")
        {
            using(SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spLogTaskImportError", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@source", Source);
                    cmd.Parameters.AddWithValue("@error", Error);
                    cmd.Parameters.AddWithValue("@meta", string.IsNullOrEmpty(Meta) ? DBNull.Value : (object)Meta);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex) { }
                }
            }
        }

        private void ExpireCache()
        {
            string CacheKeyWorkOrders = "WorkOrderCache";
            ObjectCache cache = MemoryCache.Default;
            if (cache.Contains(CacheKeyWorkOrders))
                cache.Remove(CacheKeyWorkOrders);
        }

        private static void PostImortCleanup()
        {
            try
            {
                using (var conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                {
                    var cmd = new SqlCommand("spWorkOrderImportCleanup", conn) { 
                        CommandType = CommandType.StoredProcedure, 
                        CommandTimeout = 180
                    };
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex) { }
        }

        private static byte[] ProcessQuickBooksInvoices(DateTime DateStart, DateTime DateEnd)
        {
            var invoices = GetInvoices(DateStart, DateEnd);

            //TODO: after test, select top 1 for 294 and create live invoice
            //var test = TestQbInvoiceInsert();
            //var test = invoices.Where(i => i.CustomerRefID == 294).ToList();

            //QbInvoiceExportCSV(test);
            return QbInvoiceExportCSV(invoices);
        }

        private static List<Models.QuickBooks.QboInvoice> GetInvoices(DateTime DateStart, DateTime DateEnd)
        {
            // TODO: may need to adjust if we decide to send non-Work Orders to QBO
             //var wo = Methods.WorkOrders.GetWorkOrders(DateStart, DateEnd, true, "Maintenance");

            WorkOrderRepository workOrderRepository = new WorkOrderRepository();
            var wo = workOrderRepository.GetWorkOrders(DateStart, DateEnd, true, "Maintenance");

            List<Models.QuickBooks.QboInvoice> invoices = new List<Models.QuickBooks.QboInvoice>();
            int test = 0;
            var stop = string.Empty;
            foreach (var w in wo.taskSummaries.Where(b => b.BillTo == "Owner"))
            {
                if (test == 61)
                    stop = "look here";
                try
                {
                    var supplies = wo.taskSupplies.Where(s => s.ID == w.ID);
                    var costs = wo.taskCosts.Where(c => c.ID == w.ID);
                    var items = new List<Models.QuickBooks.QboInvoiceLineItem>();
                    int idx = 1;

                    foreach (var s in supplies)
                    {
                        items.Add(new Models.QuickBooks.QboInvoiceLineItem()
                        {
                            LineNum = idx,
                            Description = s.Description,
                            SalesItemLineDetail_ItemRefId = (s.IsTaxable ? 575 : 139).ToString(),
                            SalesItemLineDetail_ItemRefName = (s.IsTaxable ? "Materials" : "Labor"),
                            SalesItemLineDetail_UnitPrice = s.UnitCost,
                            SalesItemLineDetail_Qty = s.Quantity,
                            Amount = s.TotalCost,
                            SalesItemLineDetail_TaxCodeRefId = (s.IsTaxable ? "TAX" : "NON"),
                            SalesItemLineDetail_ServiceDate = s.Completed//.ToString("yyyy-MM-dd")
                        });
                        idx++;
                    }
                    foreach (var s in costs)
                    {
                        items.Add(new Models.QuickBooks.QboInvoiceLineItem()
                        {
                            LineNum = idx,
                            Description = s.ItemDescription, // s.Description,
                            SalesItemLineDetail_ItemRefId = (s.IsTaxable ? 575 : 139).ToString(),
                            SalesItemLineDetail_ItemRefName = (s.IsTaxable ? "Materials" : "Labor"),
                            SalesItemLineDetail_UnitPrice = s.ItemTotal,
                            SalesItemLineDetail_Qty = 1,
                            Amount = s.ItemTotal,
                            SalesItemLineDetail_TaxCodeRefId = (s.IsTaxable ? "TAX" : "NON"),
                            SalesItemLineDetail_ServiceDate = s.Completed, //.ToString("yyyy-MM-dd")
                            Total = s.LineTotal
                        });
                        idx++;
                    }

                    // why did the May set have a zero line item invoice.
                    if (items.Count > 0)
                    {
                        invoices.Add(new Models.QuickBooks.QboInvoice()
                        {
                            CustomerRefID = w.QuickBooksCustomerID,
                            CustomerName = w.QuickBooksCustomerName,
                            InvoiveNum = w.ID.ToString(),
                            Description = w.Description,
                            Resolution = w.ReportSummary,
                            // TODO: need to add email
                            //Email = 
                            Line = (new JavaScriptSerializer().Serialize(items)),
                            LineItems = items,
                            TxnDate = items.OrderByDescending(i => i.SalesItemLineDetail_ServiceDate).FirstOrDefault().SalesItemLineDetail_ServiceDate,
                            BillTo = w.BillTo,
                            UnitCode = w.UnitCode
                        });
                    }
                }
                catch (Exception ex) { }
                test++;
            }
            return invoices;
        }

        private static byte[] QbInvoiceExportCSV(List<Models.QuickBooks.QboInvoice> invoices)
        {
            invoices = invoices.Where(b => b.BillTo == "Owner").ToList();
            bool success = false;
            byte[] zipbytes = null;
            double Taxrate = 0.0925;
            string columns = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}";

            string csvHeader = string.Format(
                columns,
                // 0            1           2           3       4       5       6       7               8               9           10          11              12
                "InvoiceNo", "Customer", "InvoiceDate", "DueDate", "Terms", "Memo", "Item", "ItemDescription", "ItemQuantity", "ItemRate", "ItemAmount", "ItemTaxCode", "ItemTaxAmount", "Taxable", "TaxRate", Environment.NewLine
            );
            var sb = new StringBuilder();
            sb.Append(csvHeader);

            int idxRun = 0;
            int idxMax = 950; // QBO import limit is 1000 lines
            List<StringBuilder> csvList = new List< StringBuilder>();
            //Dictionary<string, StringBuilder> csvList = new Dictionary<string, StringBuilder>();
            List<string> billto = new List<string>()
            {
                "Owner",
                "No Charge/Internal",
                "Damage",
                string.Empty
            };

            //string invoiceNum = string.Empty;
            //List<Models.QuickBooks.QboInvoice> lines = new List<Models.QuickBooks.QboInvoice>();
            /*
            foreach (var b in billto)
            {
                var inv = invoices.Where(x => x.BillTo == b);
            */
                foreach(var i in invoices)
                {
                    int lineCount = i.LineItems.Count + 2;
                    // if new group excedes max, journal and start new string builder
                    if (idxRun + lineCount > idxMax)
                    {
                        idxRun = 0;
                        if (sb.Length > 0)
                            csvList.Add(sb);

                        sb = new StringBuilder();
                        sb.Append(csvHeader);
                    } else {
                        idxRun += lineCount;
                    }

                    try
                    {
                        // First line: Item Reported
                        sb.AppendFormat(columns,
                            i.InvoiveNum,
                            i.CustomerName, //"1733 N Palm Canyon Dr Ste D (PD1733)", //i.CustomerRefID,
                            i.TxnDate.ToString("MM/dd/yyyy"),
                            i.DueDate.ToString("MM/dd/yyyy"),
                            "Due on receipt", //i.SalesTermRefID,
                            i.CustomerMemo_Value,
                            "Item Reported",
                            string.IsNullOrEmpty(i.Description) ? string.Empty : 
                                i.Description.Replace(",", "").Replace(Environment.NewLine, " ").Replace("\n", " "),
                            1,
                            0,
                            0,
                            "NON",
                            0,
                            "N",
                            string.Empty,
                            Environment.NewLine
                        );

                        // Second line: Item Resolution
                        sb.AppendFormat(columns,
                            i.InvoiveNum,
                            "",
                            "",
                            "",
                            "",
                            "",
                            "Item Resolution",
                            i.Resolution.Replace(",", "").Replace(Environment.NewLine, " ").Replace("\n", " "),
                            1,
                            0,
                            0,
                            "NON",
                            0,
                            "N",
                            string.Empty,
                            Environment.NewLine
                        );

                        foreach (var l in i.LineItems) //.Skip(1))
                        {
                            var t = l;
                            try
                            {
                                sb.AppendFormat(columns,
                                    i.InvoiveNum,
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    t.SalesItemLineDetail_ItemRefName,
                                    //t.SalesItemLineDetail_ItemRefId,
                                    t.Description.Replace(",", "").Replace(Environment.NewLine, " ").Replace("\n", " "),
                                    t.SalesItemLineDetail_Qty,
                                    t.SalesItemLineDetail_UnitPrice,
                                    (t.SalesItemLineDetail_Qty * t.SalesItemLineDetail_UnitPrice),
                                    t.SalesItemLineDetail_TaxCodeRefId,
                                    (t.SalesItemLineDetail_TaxCodeRefId == "NON" ? 0 : ((t.SalesItemLineDetail_Qty * t.SalesItemLineDetail_UnitPrice) * Taxrate)),
                                    t.SalesItemLineDetail_TaxCodeRefId == "NON" ? "N" : "Y",
                                    t.SalesItemLineDetail_TaxCodeRefId == "NON" ? "" : Taxrate.ToString(),
                                    Environment.NewLine
                                );
                            }
                            catch (Exception ex) { }
                        }
                    }
                    catch (Exception ex) { }
                }
                // add our final group
                csvList.Add(sb);
            //}



            // zip compression
            try
            {
                string zipname = DateTime.Now.ToString("yyyyMMddhhmm") + ".zip";
                //byte[] zipbytes;
                using (var outputStream = new MemoryStream())
                {
                    using (var zip = new ZipArchive(outputStream, ZipArchiveMode.Create, true))
                    {
                        int idx = 1;
                        foreach(var c in csvList)
                        {
                            //string filename = c + "-" + idx + ".csv";
                            string filename = DateTime.Now.ToString("yyyyMMddhhmm") + "-" + idx + ".csv";
                            byte[] filebytes = ASCIIEncoding.ASCII.GetBytes(c.ToString());

                            var file = zip.CreateEntry(filename, CompressionLevel.Optimal);
                            using (var entryStream = file.Open())
                            using (var compressionStream = new MemoryStream(filebytes))
                            {
                                compressionStream.CopyTo(entryStream);
                            }
                            idx++;
                        }
                    }
                    zipbytes = outputStream.ToArray();
                }

                /*
                using (Stream outputfile = System.IO.File.OpenWrite("C:\\projects\\AcmeHouseCompany\\QuickBooksInvoiceExport\\" + zipname))
                {
                    outputfile.Write(zipbytes, 0, zipbytes.Length);
                }
                */


                /*
                StreamWriter sw = new StreamWriter("C:\\projects\\AcmeHouseCompany\\QuickBooksInvoiceExport\\" + zipname, false);
                sw.Write(zipbytes);
                sw.Close();
                */
            }
            catch (Exception ex) { }

            return zipbytes;


            /*
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", 
                // 0            1           2           3       4       5       6       7               8               9           10          11              12
                "InvoiceNo","Customer","InvoiceDate","DueDate","Terms","Memo","Item","ItemDescription","ItemQuantity","ItemRate","ItemAmount","ItemTaxCode","ItemTaxAmount", "Taxable", "TaxRate", Environment.NewLine);
            foreach (var i in invoices)
            {
                try
                {
                    var t = i.LineItems.First();
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                        i.InvoiveNum,
                        i.CustomerName, //"1733 N Palm Canyon Dr Ste D (PD1733)", //i.CustomerRefID,
                        i.TxnDate.ToString("MM/dd/yyyy"),
                        i.DueDate.ToString("MM/dd/yyyy"),
                        "Due on receipt", //i.SalesTermRefID,
                        i.CustomerMemo_Value,
                        t.SalesItemLineDetail_ItemRefId,
                        t.Description.Replace(",", "").Replace(Environment.NewLine, " ").Replace("\n", " "),
                        t.SalesItemLineDetail_Qty,
                        t.SalesItemLineDetail_UnitPrice,
                        (t.SalesItemLineDetail_Qty * t.SalesItemLineDetail_UnitPrice),
                        t.SalesItemLineDetail_TaxCodeRefId,
                        (t.SalesItemLineDetail_TaxCodeRefId == "NON" ? 0 : ((t.SalesItemLineDetail_Qty * t.SalesItemLineDetail_UnitPrice) * Taxrate)),
                        t.SalesItemLineDetail_TaxCodeRefId == "NON" ? "N" : "Y",
                        t.SalesItemLineDetail_TaxCodeRefId == "NON" ? "" : Taxrate.ToString(),
                        Environment.NewLine
                    );
                    foreach (var l in i.LineItems.Skip(1))
                    {
                        t = l;
                        sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                            i.InvoiveNum,
                            "",
                            "",
                            "",
                            "",
                            "",
                            t.SalesItemLineDetail_ItemRefId,
                            t.Description.Replace(",", "").Replace(Environment.NewLine, " ").Replace("\n", " "),
                            t.SalesItemLineDetail_Qty,
                            t.SalesItemLineDetail_UnitPrice,
                            (t.SalesItemLineDetail_Qty * t.SalesItemLineDetail_UnitPrice),
                            t.SalesItemLineDetail_TaxCodeRefId,
                            (t.SalesItemLineDetail_TaxCodeRefId == "NON" ? 0 : ((t.SalesItemLineDetail_Qty * t.SalesItemLineDetail_UnitPrice) * Taxrate)),
                            t.SalesItemLineDetail_TaxCodeRefId == "NON" ? "N" : "Y",
                            t.SalesItemLineDetail_TaxCodeRefId == "NON" ? "" : Taxrate.ToString(),
                            Environment.NewLine
                        );
                    }
                }
                catch(Exception ex) { }

                // zip compression
                try
                {
                    string filename = DateTime.Now.ToString("yyyyMMddhhmm") + ".csv";
                    byte[] filebytes = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
                    string zipname = DateTime.Now.ToString("yyyyMMddhhmm") + ".zip";
                    byte[] zipbytes;
                    using (var outputStream = new MemoryStream())
                    {
                        using(var zip = new ZipArchive(outputStream, ZipArchiveMode.Create, true))
                        {
                            var file = zip.CreateEntry(filename, CompressionLevel.Optimal);
                            using(var entryStream = file.Open())
                                using(var compressionStream = new MemoryStream(filebytes))
                                {
                                compressionStream.CopyTo(entryStream);
                                }
                        }
                        zipbytes = outputStream.ToArray();
                    }

                    using (Stream outputfile = System.IO.File.OpenWrite("C:\\projects\\AcmeHouseCompany\\QuickBooksInvoiceExport\\" + zipname))
                    {
                        outputfile.Write(zipbytes, 0, zipbytes.Length);
                    }
                    /*
                    StreamWriter sw = new StreamWriter("C:\\projects\\AcmeHouseCompany\\QuickBooksInvoiceExport\\" + zipname, false);
                    sw.Write(zipbytes);
                    sw.Close();
                    * /
                }
                catch (Exception ex) { }

                // Write the file to disk
                try
                {
                    StreamWriter sw = new StreamWriter("C:\\projects\\AcmeHouseCompany\\QuickBooksInvoiceExport\\" + DateTime.Now.ToString("yyyyMMddhhmm") + ".csv", false);
                    sw.Write(sb.ToString());
                    sw.Close();
                }
                catch(Exception ex) { }
                success = true;
            }
            */
            //return success;
        }
        private static bool QbInvoiceInsert(Models.QuickBooks.QboInvoice i)
        {
            bool success = false;
            try
            {
                string query = string.Format(@"
				    INSERT INTO Invoice 
			        (
				        DocNumber,
				        BillEmail_Address,
				        CustomerRefID, 
				        Line,
				        TxnDate, 
				        CustomerMemo_Value, --<-- work order ID
				        --TxnTaxDetail_TxnTaxCodeRefId,
				        SalesTermRefId,
				        DueDate
			        ) 
		            VALUES
			        (
				        '3812-6172',
				        'acmetest@demo.com',
				        294,
				        '[  {    ""Id"": ""1"",    ""LineNum"": 1.0,    ""Description"": ""Spiral 60W GP19 Base"",    ""Amount"": 5.0,    ""DetailType"": ""SalesItemLineDetail"",    ""SalesItemLineDetail_ItemRefId"": ""1032"",    ""SalesItemLineDetail_ItemRefName"": ""Spiral 60W GP19 Base"",    ""SalesItemLineDetail_UnitPrice"": 5.0,    ""SalesItemLineDetail_Qty"": 1.0,    ""SalesItemLineDetail_TaxCodeRefId"": ""TAX"",    ""SalesItemLineDetail_ServiceDate"": ""2019-05-03T00:00:00Z""  },  {""Amount"": 5.0,""DetailType"": ""SubTotalLineDetail"" } ]',
				        '2019-05-10',  --<-- Work Order Completed date
				        'Work Order #TEST',
				        --5,
				        5,
				        '2019-05-13' 
			        )   
                ",
                    // 0
                    i.InvoiveNum,
                    i.Email,
                    i.CustomerRefID,
                    i.Line,
                    i.TxnDate.ToString("yyyy-MM-dd"),
                    i.CustomerMemo_Value,
                    i.SalesTermRefID,
                    i.DueDate.ToString("yyyy-MM-dd")
                );



                using (var conn = new OdbcConnection(GlobalSettings.DateStores.Breezeway))
                {
                    OdbcCommand cmd;

                    conn.Open();
                    cmd = new OdbcCommand(query, conn);
                    var r = cmd.ExecuteNonQuery();
                    conn.Close();
                    success = true;
                }
            }
            catch (Exception ex) { }
            return success;
        }
        #endregion Helper methods



        
        #region QuickBook methods
        public JsonNetResult GetInvoicesForImport(DateTime startDate, DateTime endDate)
        {
            WorkOrderRepository workOrderRepository = new WorkOrderRepository();

            var wo = workOrderRepository.GetLockedWorkOrders(startDate, endDate, false, "Property Care");

            var invoices = MapToInvoices(wo);
            return new JsonNetResult(new
            {
                invoices = invoices
            });
        }

        public JsonNetResult GetUploadedInvoices(DateTime startDate, DateTime endDate)
        {
            IInvoicingService invoicingService = new InvoicingService();
            var invoices = invoicingService.GetGroupedUploadedWorkOrders(startDate, endDate);
            
            return new JsonNetResult(new
            {
                workOrders = invoices
            });
        }

        public JsonNetResult GetWorkOrders(DateTime startDate, DateTime endDate, string Type = "Property Care", bool showCommitted = false )
        {
            if (me.UserType == DataTypes.UserType.Employee)
            {
                var model = new ViewModels.WorkOrdersPreviewViewModel()
                {
                    DateStart = startDate,
                    DateEnd = endDate,
                    //Query = string.IsNullOrEmpty(Query) ? string.Empty : Query,
                    Type = string.IsNullOrEmpty(Type) ? string.Empty : Type,
                    //Filter = Filter,
                    ShowCommitted = showCommitted
                };

                //GWH where to move this.
                //if (Init)
                {
                    // Reconcile QuickBooks Customers
                    //Methods.WorkOrders.ImportQuickBooksCustomers();
                    //PostImortCleanup();
                }

                WorkOrderRepository workOrderRepository = new WorkOrderRepository();
                var wo = workOrderRepository.GetWorkOrders(startDate, endDate, false, Type, showCommitted);

                //var wo = Methods.WorkOrders.GetWorkOrders(DateStart, DateEnd, false, Type, ShowCommitted);

                var invoices = MapToInvoices(wo);

                return new JsonNetResult(new
                {
                    owners = invoices.Where(t => t.BillTo == "Owner" && t.IsIgnore == false && t.IsLocked == false).ToList(),
                    comp = invoices.Where(t => t.BillTo == "No Charge/Internal" && t.IsIgnore == false && t.IsLocked == false).ToList(),
                    other = invoices.Where(t => t.BillTo != "Owner" && t.BillTo != "No Charge/Internal" && t.IsIgnore == false && t.IsLocked == false).ToList(),
                    ignored = invoices.Where(t => t.IsIgnore == true && t.IsLocked == false).ToList(),
                    locked = invoices.Where(t => t.IsLocked == true).ToList()
                });

            }
            else
            {
                return null;
            }
        }

        public JsonNetResult QboCommit(bool IsConfirmed = false)
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spQuickbooksCommitLock", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@isConfirmed", IsConfirmed);
                        conn.Open();
                        using (var r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                total = (int)r["RecordCount"];
                            }
                        }
                        ExpireCache();
                    }
                    catch (Exception ex) { }
                }
            }
            return new JsonNetResult(new
            {
                total = total
            });
        }

        public JsonNetResult UpdateBillTo(int ID, string Type)
        {
            bool status = false;
            if (Type != "-- Select --")
            {

                using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
                {
                    using (SqlCommand cmd = new SqlCommand("spWorkOrderBillToTypeUpdate", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@invoiceNum", ID);
                            cmd.Parameters.AddWithValue("@type", Type);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            status = true;
                            ExpireCache();
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            return new JsonNetResult(new { status = status.ToString().ToLower(), Invoice = ID, Type = Type });
        }

        public JsonNetResult UpdateViewIgnore(int ID, bool IsShown)
        {
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spWorkOrderViewIgnoreUpdate", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@workorderid", ID);
                        cmd.Parameters.AddWithValue("@isignore", IsShown ? false : true);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        ExpireCache();
                    }
                    catch (Exception ex) { }
                }
            }
            return new JsonNetResult(new { Invoice = ID, IsShown = IsShown });
        }

        public JsonNetResult UpdateLock(int ID, bool IsLocked)
        {
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spWorkOrderIsLockedUpdate", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@invoicenum", ID);
                        cmd.Parameters.AddWithValue("@islocked", IsLocked);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        ExpireCache();
                    }
                    catch (Exception ex) { }
                }
            }
            return new JsonNetResult(new { Invoice = ID, IsLocked = IsLocked ? false : true });
        }

        public JsonNetResult DeleteRecord(int ID)
        {
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                using (SqlCommand cmd = new SqlCommand("spWorkOrderDelete", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@workorderid", ID);
                        cmd.Parameters.AddWithValue("@userid", me.UserID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        ExpireCache();
                    }
                    catch (Exception ex) { }
                }
            }
            return new JsonNetResult(new { Invoice = ID, UserID = me.UserID });
        }

        public JsonNetResult SyncCustomers()
        {
            // Reconcile QuickBooks Customers
            Methods.WorkOrders.ImportQuickBooksCustomers();
            PostImortCleanup();
            return new JsonNetResult();
        }

        #endregion

        public ActionResult EditInBreezeway()
        {
            return PartialView();
        }

        public JsonNetResult QboRefreshCustomers()
        {
            // Reconcile QuickBooks Customers
            try
            {
                Methods.WorkOrders.ImportQuickBooksCustomers();
                PostImortCleanup();
                return new JsonNetResult(new { message = "Customer sync completed successfully." });
            }
            catch(Exception ex)
            {
                return new JsonNetResult(new { message = $"Customer sync failed, {ex.Message}" });
            }
        }
    }
}   