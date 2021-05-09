using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlKata.Execution;

namespace DatabaseAPI.DAL
{
    public class ProductRepository : ObjectRepository<Product>
    {
        public ProductRepository(string connectionString) : base(connectionString, "products", Product.ColumnNames)
        {
        }

        public IEnumerable<(Guid id, string name, int quantity)> GetOrderProducts(Order order) => Query().Select("products.id", "products.name", "order_items.quantity").Join("order_items", "product_id", "id").Where("order_id", order.Id).Get<(Guid id, string name, int quantity)>();
    }
}
