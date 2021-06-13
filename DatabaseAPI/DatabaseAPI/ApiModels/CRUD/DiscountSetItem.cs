using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels.CRUD
{
    public class DiscountSetItem
    {
        public DiscountSetItem(Guid discountId, Guid productId, int quantity)
        {
            DiscountId = discountId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid DiscountId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }
    }
}
