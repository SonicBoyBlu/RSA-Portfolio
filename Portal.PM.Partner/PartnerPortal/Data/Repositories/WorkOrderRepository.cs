using System;
using System.Collections.Generic;
using System.Linq;
using Acme.Models.QuickBooks;
using Dapper;
using Acme.ViewModels;
using System.Data;
using Acme.Models.Breezeway;
using Acme.Models;

namespace Acme.Data.Repositories
{

    public class WorkOrderRepository : BaseRepository, IWorkOrderRepository
    {
        public WorkOrdersImportViewModel GetWorkOrders(DateTime dateStart, DateTime dateEnd, bool isQboExport, string type, bool showCommitted = false)
        {
            return WithConnection<WorkOrdersImportViewModel>(connection =>
            {
                var model = new WorkOrdersImportViewModel();

                List<Customer> customers = new List<Customer>();
                var result = connection.QueryMultiple("spWorkOrdersGet",
                  new { dateStart = dateStart, dateEnd = dateEnd, isQboExport = isQboExport, type = type, showCommitted = showCommitted },
                  commandType: CommandType.StoredProcedure);


                model.taskSummaries = result.Read<TaskSummary>().OrderBy(s => s.QuickBooksCustomerName).ToList();
                model.taskCosts = result.Read<TaskCost>().ToList();
                model.dateOldest = model.taskSummaries.FirstOrDefault().Completed;

                return model;
            });
        }

        public WorkOrdersImportViewModel GetLockedWorkOrders(DateTime dateStart, DateTime dateEnd, bool isQboExport, string type, bool showCommitted = false)
        {
            return WithConnection<WorkOrdersImportViewModel>(connection =>
            {
                var model = new WorkOrdersImportViewModel();

                List<Customer> customers = new List<Customer>();
                var result = connection.QueryMultiple("spWorkOrdersGet",
                  new { dateStart = dateStart, dateEnd = dateEnd, isQboExport = isQboExport, type = type, showCommitted = showCommitted },
                  commandType: CommandType.StoredProcedure);


                model.taskSummaries = result.Read<TaskSummary>().OrderBy(s => s.QuickBooksCustomerName).Where(s => s.IsLocked).ToList();
                model.taskCosts = result.Read<TaskCost>().ToList();

                return model;
            });
        }

        public List<TaskSummary> GetUploadedWorkOrders(DateTime startDate, DateTime endDate)
        {
            return WithConnection<List<TaskSummary>>(connection =>
            {
                string query = "SELECT ts.*, c.CompanyName AS FullyQualifiedName FROM TaskSummaries ts INNER JOIN QuickBooksCustomers c on ts.QuickBooksCustomerID = c.ID WHERE QuickBooksInvoiceID IS NOT NULL AND QuickBooksTxnDate BETWEEN @startDate AND @endDate";
                return (connection.Query<TaskSummary>(query, new { startDate = startDate, endDate = endDate })).ToList();
            });
        }

        public List<WorkOrdersMonthly> GetGroupedUploadedWorkOrders(DateTime startDate, DateTime endDate)
        {
            var taskSummaries = GetUploadedWorkOrders(startDate, endDate);

            return (from ts in taskSummaries
                    group ts by new
                    {
                        month = ts.DateQboUploaded.Month,
                        year = ts.DateQboUploaded.Year,        
                    }
                    into wo
                    select new WorkOrdersMonthly()
                    {
                        MonthYearDate = new DateTime(wo.Key.year, wo.Key.month, 1),
                        Year = wo.Key.year,
                        Month = wo.Key.month,
                        Count = wo.Count(),
                        TotalAmount = wo.Sum(x => x.QuickBooksTotalCost),
                        Invoices = wo.ToList()
                    })
                    .OrderByDescending(g => g.MonthYearDate).ToList();
        }

        public void MarkInvoiceAsUploaded(int id, int qboInvoiceId, string qboInvoiceNum, double qboAmount, DateTime qboTxnDate)
        {
            WithConnection( c =>
            {
                const string query = "UPDATE TaskSummaries SET QuickBooksInvoiceID=@qboInvoiceId, QuickBooksInvoiceNum=@qboInvoiceNum, QuickBooksTotalCost=@qboAmount, QuickBooksTxnDate=@qboTxnDate, DateQboUploaded=@dateImported, IsLocked=0 WHERE ID=@id";
                return c.Query<int>(query, new {id = id,  qboInvoiceId = qboInvoiceId, qboInvoiceNum = qboInvoiceNum, qboAmount = qboAmount, qboTxnDate = qboTxnDate, dateImported = DateTime.Now });
            });
        }

        public void SetInvoiceAmount(int id, decimal qboInvoiceAmount)
        {
            WithConnection(c =>
            {
                const string query = "UPDATE TaskSummaries SET QuickBooksTotalCost=@qboInvoiceAmount WHERE ID=@id";
                return c.Query<int>(query, new { id = id, qboInvoiceAmount = qboInvoiceAmount });
            });
        }

        public void SetTxnDate(int id, DateTime txnDate)
        {
            WithConnection(c =>
            {
                const string query = "UPDATE TaskSummaries SET QuickBooksTxnDate=@txnDate WHERE ID=@id";
                return c.Query<int>(query, new { id = id, txnDate = txnDate });
            });
        }


    }
}