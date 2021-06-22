using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels
{
    public class Product
    {
        public Product(Guid id, string name, int count)
        {
            Id = id;
            Name = name;
            Count = count;
        }

        public Guid Id { get; }
        public string Name { get; }
        public int Count { get; }
    }
}
