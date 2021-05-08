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
        public Order(Guid id, Guid cashierId, OrderStatus status, DateTime createdAt, double price, int ticketNumber)
        {
            Id = id;
            CashierId = cashierId;
            Status = status;
            CreatedAt = createdAt;
            Price = price;
            TicketNumber = ticketNumber;
        }

        public Order(Guid id, Guid cashier_id, string status, DateTime created_at, double price, int ticket_number) : 
            this(id, cashier_id, CastToOrderStatus(status) ?? throw new ArgumentException("Status should be one of Preparing, Serving, Finished, Canceled"), created_at, price, ticket_number) { }

        public Guid Id { get; }
        public Guid CashierId { get; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; }
        public double Price { get; }
        public int TicketNumber { get; }

        private static OrderStatus? CastToOrderStatus(string status)
        {
            switch (status)
            {
                case "Preparing":
                    return OrderStatus.Preparing;
                case "Serving":
                    return OrderStatus.Serving;
                case "Finished":
                    return OrderStatus.Finished;
                case "Canceled":
                    return OrderStatus.Canceled;
                default:
                    return null;
            }
        }
    }
}
