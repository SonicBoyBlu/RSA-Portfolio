using System;
using System.Collections.Generic;
using Acme.Models;
using Acme.Models.Breezeway;
using Acme.ViewModels;

namespace Acme.Data.Repositories
{
    public interface IWorkOrderRepository
    {
        WorkOrdersImportViewModel GetWorkOrders(DateTime dateStart, DateTime dateEnd, bool isQboExport, string type, bool showCommitted = false);
        WorkOrdersImportViewModel GetLockedWorkOrders(DateTime dateStart, DateTime dateEnd, bool isQboExport, string type, bool showCommitted = false);
        List<TaskSummary> GetUploadedWorkOrders(DateTime startDate, DateTime endDate);
        void MarkInvoiceAsUploaded(int id, int qboInvoiceId, string qboInvoiceNum, double qboAmount, DateTime qboTxnDate);
        void SetInvoiceAmount(int id, decimal qboInvoiceAmount);
        void SetTxnDate(int id, DateTime txnDate);
        List<WorkOrdersMonthly> GetGroupedUploadedWorkOrders(DateTime startDate, DateTime endDate);

    }
}
