using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels.CRUD
{
    public class Product
    {
        public Product(Guid id, string name, double price, DatabaseModels.ProductStatus status)
        {
            Id = id;
            Name = name;
            Price = price;
            Status = status;
        }

        public Guid Id { get; }
        public string Name { get; }
        public double Price { get; }
        public DatabaseModels.ProductStatus Status { get; }
    }
}
