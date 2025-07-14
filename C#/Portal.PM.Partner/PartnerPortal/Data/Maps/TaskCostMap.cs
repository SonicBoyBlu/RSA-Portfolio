using Dapper.FluentMap.Mapping;
using Acme.Models.Breezeway;

namespace Acme.Data.Maps
{
    internal class TaskCostMap : EntityMap<TaskCost>
    {
        internal TaskCostMap()
        {
            Map(c => c.Quantity).ToColumn("SupplyQuantity");
        }
    }
}