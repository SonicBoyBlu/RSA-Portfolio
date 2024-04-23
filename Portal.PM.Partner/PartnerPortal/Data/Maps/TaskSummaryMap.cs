using Dapper.FluentMap.Mapping;
using Acme.Models.Breezeway;

namespace Acme.Data.Maps
{
    internal class TaskSummaryMap : EntityMap<TaskSummary>
    {
        internal TaskSummaryMap()
        {
            Map(c => c.QuickBooksCustomerName).ToColumn("FullyQualifiedName");
            Map(c => c.UnitCode).ToColumn("PropertyMarketingID");
        }
    }
}