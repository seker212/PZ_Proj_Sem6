using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DatabaseModels;
using DatabaseAPI.Helpers;
using SqlKata.Execution;
using SqlKata.Extensions;

namespace DatabaseAPI.DAL
{
    public class OrderRepository : ObjectRepository<Order>, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString, "orders", Order.ColumnNames)
        {
        }

        public IEnumerable<Order> GetKitchenOrders() => Query().WhereRaw(EnumCaster.OrderStatus.ToQuery(OrderStatus.Preparing)).Get<Order>();
        public IEnumerable<Order> GetServiceOrders() => Query().WhereRaw(EnumCaster.OrderStatus.ToQuery(OrderStatus.Serving)).Get<Order>();
        public bool UpdateStatus(Guid id, OrderStatus newStatus) => Query().Where(_columnNames[0], id).Update(new { status = EnumCaster.OrderStatus.ToStr(newStatus) }) == 1;
    }
}
