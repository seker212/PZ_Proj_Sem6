using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    public enum ProductStatus
    {
        Available,
        Withdrawn,
        Paused
    }
    public class Product
    {
        public Product(Guid id, string name, float price, ProductStatus status)
        {
            Id = id;
            Name = name;
            Price = price;
            Status = status;
        }

        public Guid Id { get; }
        public string Name { get; }
        public float Price { get; set; }
        public ProductStatus Status { get; set; }
    }
}
