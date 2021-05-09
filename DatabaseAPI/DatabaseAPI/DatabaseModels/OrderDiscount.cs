using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public class OrderDiscount : IDbModel
    {
        public OrderDiscount(Guid orderId, Guid discountId)
        {
            OrderId = orderId;
            DiscountId = discountId;
        }

        public Guid OrderId { get; }
        public Guid DiscountId { get; }

        public object[] Data => new object[] { OrderId, DiscountId };
        public static string[] ColumnNames => new string[] { "order_id", "discount_id" };
    }
}
