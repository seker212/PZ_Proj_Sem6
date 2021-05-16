using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public class OrderDiscount : IDbModel
    {
        public OrderDiscount(Guid order_id, Guid discount_id, int quantity)
        {
            OrderId = order_id;
            DiscountId = discount_id;
            Quantity = quantity;
        }

        public Guid OrderId { get; }
        public Guid DiscountId { get; }
        public int Quantity { get; }

        public object[] Data => new object[] { OrderId, DiscountId };
        public static string[] ColumnNames => new string[] { "order_id", "discount_id", "quantity" };
    }
}
