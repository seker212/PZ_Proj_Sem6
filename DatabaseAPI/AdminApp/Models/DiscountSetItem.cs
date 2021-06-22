using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class DiscountSetItem
    {
        public Guid DiscountId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
