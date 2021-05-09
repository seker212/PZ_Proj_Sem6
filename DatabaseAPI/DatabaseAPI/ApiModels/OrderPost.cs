using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels
{
    public class OrderPost
    {
        public Guid CashierId { get; }
        public double Price { get; }
        public IEnumerable<Product> Products { get; }
        public IEnumerable<DiscountBasic> Discounts { get; }
        public DateTime CreatedAt { get; }
    }
}
