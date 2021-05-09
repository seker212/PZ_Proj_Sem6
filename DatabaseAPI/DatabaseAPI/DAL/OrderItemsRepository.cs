using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DatabaseModels;
using SqlKata.Execution;

namespace DatabaseAPI.DAL
{
    public class OrderItemsRepository : PairRepository<OrderItems>
    {
        public OrderItemsRepository(string connectionString) : base(connectionString, "order_items", OrderItems.ColumnNames)
        {
        }

        public IEnumerable<OrderItems> Get(Guid orderKey) => Query().Where("order_id", orderKey).Get<OrderItems>();
        public IEnumerable<OrderItems> Get(Order order) => Get(order.Id);
    }
}
