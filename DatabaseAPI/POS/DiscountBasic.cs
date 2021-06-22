using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSApp {
    public class DiscountBasic
    {
        public DiscountBasic(Guid id, int count)
        {
            Id = id;
            Count = count;
        }

        public Guid Id { get; }
        public int Count { get; set; }
    }
}
