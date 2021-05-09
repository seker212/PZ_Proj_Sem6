using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels
{
    public class OrderProducts
    {
        public OrderProducts(Guid id, IEnumerable<Product> products, int ticketNumber)
        {
            Id = id;
            Products = products;
            TicketNumber = ticketNumber;
        }

        public Guid Id { get; }
        public IEnumerable<Product> Products { get; }
        public int TicketNumber { get; }
    }
}
