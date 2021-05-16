using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlKata.Execution;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DAL
{
    public class ProductRepository : ObjectRepository<Product>, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString, "products", Product.ColumnNames)
        {
        }

        public IEnumerable<(Guid id, string name, int quantity)> GetOrderProducts(Order order) => Query().Select("products.id", "products.name", "order_items.quantity").Join("order_items", "product_id", "id").Where("order_id", order.Id).Get<(Guid id, string name, int quantity)>();
        public IEnumerable<(Guid id, string name, int quantity)> GetDiscountProducts(Discount discount) => Query().Select("products.id", "products.name", "discounts_set_items.quantity").Join("discounts_set_items", "product_id", "id").Where("discounts_set_items", discount.Id).Get<(Guid id, string name, int quantity)>();
        public IEnumerable<Product> GetAvailableProducts() => Query().Where("status", EnumCaster.ProductStatus.ToStr(ProductStatus.Available)).Get<Product>();
        public double GetProductPrice(Guid id) => Query().Select("products.price").Where("id", id).First<double>();
        public IEnumerable<double> GetOrderProductPrices(Order order) => Query().Select("products.price").Join("order_items", "product_id", "id").Where("order_id", order.Id).Get<double>();
    }
}
