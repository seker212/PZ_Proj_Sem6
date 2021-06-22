using SqlKata.Execution;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DAL
{
    public class OrderDiscountRepository : PairRepository<OrderDiscount>, IOrderDiscountRepository
    {
        public OrderDiscountRepository(string connectionString) : base(connectionString, "order_discount", OrderDiscount.ColumnNames)
        {
        }

        public IEnumerable<OrderDiscount> GetOrderDiscounts(Order order) => Query().Where("order_id", order.Id).Get<OrderDiscount>();
    }
}
