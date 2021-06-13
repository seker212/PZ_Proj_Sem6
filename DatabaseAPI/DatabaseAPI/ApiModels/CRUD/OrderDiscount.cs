using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels.CRUD
{
    public class OrderDiscount
    {
        public OrderDiscount(Guid orderId, Guid discountId, int quantity)
        {
            OrderId = orderId;
            DiscountId = discountId;
            Quantity = quantity;
        }

        public Guid OrderId { get; }
        public Guid DiscountId { get; }
        public int Quantity { get; }
    }
}
