using DatabaseAPI.DatabaseModels;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;

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

        [TestMethod()]
        public void TryGetOrdersTestServices()
        {
            new OrderRepository(_connectionString).GetServiceOrders().Should().HaveCount(1);
        }

        [TestMethod()]
        public void TryGetOrdersTestKitchen()
        {
            new OrderRepository(_connectionString).GetKitchenOrders().Should().HaveCount(1);
        }

        [TestMethod()]
        public void PlsKillMe()
        {
            using var con = new NpgsqlConnection(_connectionString);
            con.Open();

            var cmd=con.CreateCommand();
            cmd.CommandText = "SELECT * FROM \"orders\" WHERE \"status\" = 'Serving'";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var a = reader.GetGuid(0);
                var b = reader.GetGuid(1);
                var c = reader.GetString(2);
                var d = reader.GetDateTime(3);
                var e = reader.GetDouble(4);
                var f = reader.GetInt32(5);
            }
        }

        [TestMethod()]
        public void TryGetProdTestsafsdgdszfhgiohnegrwfjkml()
        {
            var s = new ProductRepository(_connectionString).GetOrderProducts(new Order(new Guid("00000000-0000-0000-0000-000000000009"), Guid.NewGuid(), OrderStatus.Canceled, DateTime.Now, 3.4, 3));
        }

        [TestMethod()]
        public void TryGetProdTestsafsdgdszfhgiohngrwfjkml()
        {
            var s = new ProductRepository(_connectionString).GetAvailableProducts();
        }

        [TestMethod()]
        public void TryGetOrdersTestUpdatreInsert()
        {
            var r = new OrderRepository(_connectionString);
            var ins = r.Insert(new Order(Guid.NewGuid(), new Guid("00000000-0000-0000-0000-000000000007"), OrderStatus.Preparing, DateTime.Now, 10, 4));
            r.Get(ins.Id).Status.Should().Be(ins.Status);
            r.UpdateStatus(ins.Id, OrderStatus.Canceled);
            r.Get(ins.Id).Status.Should().Be(OrderStatus.Canceled);
        }
    }
}