using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSApp {
    class OrderProducts {
        public OrderProducts(Guid id, IEnumerable<Product> products, int ticketNumber) {
            Id = id;
            Products = products;
            TicketNumber = ticketNumber;
        }

        public Guid Id { get; }
        public IEnumerable<Product> Products { get; }
        public int TicketNumber { get; }
    }
}
