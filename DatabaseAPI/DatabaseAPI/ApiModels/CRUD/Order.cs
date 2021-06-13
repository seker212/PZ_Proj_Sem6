using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels.CRUD
{
    public class Order
    {
        public Order(Guid id, Guid cashierId, DatabaseModels.OrderStatus status, DateTime createdAt, double price, int ticketNumber)
        {
            Id = id;
            CashierId = cashierId;
            Status = status;
            CreatedAt = createdAt;
            Price = price;
            TicketNumber = ticketNumber;
        }

        public Guid Id { get; }
        public Guid CashierId { get; }
        public DatabaseModels.OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; }
        public double Price { get; }
        public int TicketNumber { get; }
    }
}
