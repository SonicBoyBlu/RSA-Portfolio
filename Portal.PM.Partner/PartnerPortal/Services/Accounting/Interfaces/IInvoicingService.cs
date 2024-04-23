using System;
using System.Collections.Generic;
using Acme.Models;
using Acme.Models.Breezeway;

namespace Acme.Services.Accounting
{
    public interface IInvoicingService
    {
        List<TaskSummary> GetUploadedInvoices(DateTime startDate, DateTime endDate);
        List<WorkOrdersMonthly> GetGroupedUploadedWorkOrders(DateTime startDate, DateTime endDate);
    }
}