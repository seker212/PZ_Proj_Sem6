using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseAPI.DAL;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DatabaseAPI.DAL.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        static string _connectionString = "Host=localhost;Username=postgres;Password=mysecretpassword;Database=postgres";

        [TestMethod()]
        public void TryGetDiscountsTest()
        {
            new ObjectRepository<Discount>(_connectionString, "discounts", Discount.ColumnNames).Get().Should().HaveCount(4);
        }

        [TestMethod()]
        public void TryGetOrdersTest()
        {
            new ObjectRepository<Order>(_connectionString, "orders", Order.ColumnNames).Get().Should().HaveCount(4);
        }

        [TestMethod()]
        public void TryGetProductsTest()
        {
            new ObjectRepository<Product>(_connectionString, "products", Product.ColumnNames).Get().Should().HaveCount(4);
        }

        [TestMethod()]
        public void TryGetDiscountSetItemsTest()
        {
            new PairRepository<DiscountSetItem>(_connectionString, "discounts_set_items", DiscountSetItem.ColumnNames).Get().Should().HaveCount(3);
        }

        [TestMethod()]
        public void TryGetOrderDiscountsTest()
        {
            new ObjectRepository<OrderDiscount>(_connectionString, "order_discount", OrderDiscount.ColumnNames).Get().Should().HaveCount(3);
        }

        [TestMethod()]
        public void TryGetOrderItmesTest()
        {
            new ObjectRepository<OrderItems>(_connectionString, "order_items", OrderItems.ColumnNames).Get().Should().HaveCount(5);
        }

        [TestMethod()]
        public void TryGetUsersTest()
        {
            new ObjectRepository<User>(_connectionString, "users", User.ColumnNames).Get().Should().HaveCount(2);
        }

        [TestMethod()]
        public void TryGetCashiersTest()
        {
            new ObjectRepository<Cashier>(_connectionString, "cashiers", Cashier.ColumnNames).Get().Should().HaveCount(2);
        }
    }
}