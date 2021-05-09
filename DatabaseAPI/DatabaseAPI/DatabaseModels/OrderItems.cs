using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    public class OrderItems : IDbModel
    {
        public OrderItems(Guid order_id, Guid product_id, int quantity, double price)
        {
            OrderId = order_id;
            ProductId = product_id;
            Quantity = quantity;
            Price = price;
        }

        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }
        public double Price { get; }

        public object[] Data => new object[] { OrderId, ProductId, Quantity, Price };
        public static string[] ColumnNames => new string[] { "order_id", "product_id", "quantity", "price" };
    }
}
