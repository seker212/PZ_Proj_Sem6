using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public class DiscountSetItem : IDbModel
    {
        public DiscountSetItem(Guid discount_id, Guid product_id, int quantity)
        {
            DiscountId = discount_id;
            ProductId = product_id;
            Quantity = quantity;
        }

        public Guid DiscountId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        public object[] Data => new object[] { DiscountId, ProductId };
        public static string[] ColumnNames => new string[] { "id", "product_id", "quantity" };
    }
}
