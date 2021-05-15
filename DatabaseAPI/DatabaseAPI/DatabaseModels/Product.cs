using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public enum ProductStatus
    {
        Available,
        Withdrawn,
        Paused
    }
    public class Product : IDbModel
    {
        public Product(Guid id, string name, double price, ProductStatus status)
        {
            Id = id;
            Name = name;
            Price = price;
            Status = status;
        }

        public Guid Id { get; }
        public string Name { get; }
        public double Price { get; set; }
        public ProductStatus Status { get; set; }

        public object[] Data => new object[] { Id, Name, Price, EnumCaster.ProductStatus.ToStr(Status) };
        public static string[] ColumnNames => new string[] { "id", "name", "price", "status" };
    }
}
