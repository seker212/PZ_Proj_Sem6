using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public class DiscountSetItems : IDbModel
    {
        public DiscountSetItems(Guid discountId, Guid productId, int quantity)
        {
            DiscountId = discountId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid DiscountId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        public object[] Data => new object[] { DiscountId, ProductId };
        public static string[] ColumnNames => new string[] { "id", "product_id", "quantity" };
    }
}
