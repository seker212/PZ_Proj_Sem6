using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class Discount
    {
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; }
        public double? SetPrice { get; set; }
        public double? PriceDropAmount { get; set; }
        public double? PriceDropPercent { get; set; }
    }
}
