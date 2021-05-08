using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    public enum OrderStatus
    {
        Preparing,
        Serving,
        Finished,
        Canceled
    }
    public class Order
    {
        public Order(Guid id, Guid cashierId, OrderStatus status, DateTime createdAt, float price, int ticketNumber)
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
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; }
        public float Price { get; }
        public int TicketNumber { get; }
    }
}
