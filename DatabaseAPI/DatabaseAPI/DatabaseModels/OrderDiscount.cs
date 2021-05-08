using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
