using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSApp {
    public class Product {
        public Product(Guid id, string name, int count) {
            Id = id;
            Name = name;
            Count = count;
        }

        public Guid Id { get; }
        public string Name { get; }
        public int Count { get; set; }
    }
}
