using Acme.Data.Repositories;
using Acme.Models.Breezeway;
using Acme.Services.Tracking;
using System;
using System.Collections.Generic;
using Acme.Models;
using System.Linq;

namespace Acme.Services.Accounting
{
    public class InvoicingService : IInvoicingService
    {
        IWorkOrderRepository _workOrderRepository;
        IUserActionService _userActionService;

        public InvoicingService(IWorkOrderRepository workOrderRepository, IUserActionService userActionService)
        {
            _workOrderRepository = workOrderRepository;
            _userActionService = userActionService;
        }
        public InvoicingService()
        {
            _workOrderRepository = new WorkOrderRepository();
            _userActionService = new UserActionService();
        }

        public List<TaskSummary> GetUploadedInvoices(DateTime startDate, DateTime endDate)
        {
            List<UserActionDetail> userActions = _userActionService.GetUserActions(UserActionTargetType.Invoice, UserActionVerb.Upload, startDate, DateTime.Now);
            List<TaskSummary> workOrders = _workOrderRepository.GetUploadedWorkOrders(startDate, endDate);

            foreach (var workOrder in workOrders)
            {
                var userAction = userActions.FirstOrDefault(action => action.TargetID == workOrder.ID);
                if (userAction != null)
                {
                    workOrder.UploadedByUserID = userAction.UserID;
                    workOrder.UploadedByUserName = userAction.FullName;
                }
            }

            return workOrders;
        }

        public List<WorkOrdersMonthly> GetGroupedUploadedWorkOrders(DateTime startDate, DateTime endDate)
        {
            var taskSummaries = GetUploadedInvoices(startDate, endDate);

            return (from ts in taskSummaries
                    group ts by new
                    {
                        month = ts.QuickBooksTxnDate.Month,
                        year = ts.QuickBooksTxnDate.Year,
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

    }
}