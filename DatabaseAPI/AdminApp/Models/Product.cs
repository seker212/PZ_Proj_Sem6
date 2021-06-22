using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public enum ProductStatus
    {
        Available,
        Withdrawn,
        Paused
    }
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
    }
}
