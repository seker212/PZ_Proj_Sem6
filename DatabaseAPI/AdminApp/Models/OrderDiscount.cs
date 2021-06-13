using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class OrderDiscount
    {
        public Guid OrderId { get; set; }
        public Guid DiscountId { get; set; }
        public int Quantity { get; set; }
    }
}
