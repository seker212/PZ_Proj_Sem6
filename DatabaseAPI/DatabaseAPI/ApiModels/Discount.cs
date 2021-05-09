using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels
{
    public class Discount
    {
        public Guid Id { get; }
        public DatabaseModels.DiscountType Type { get; }
        public double SetPrice { get; }
        public IEnumerable<Product> Products { get; }
    }
}
