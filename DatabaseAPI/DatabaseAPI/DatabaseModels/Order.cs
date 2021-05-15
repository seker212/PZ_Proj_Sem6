using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public enum OrderStatus
    {
        Preparing,
        Serving,
        Finished,
        Canceled
    }

    public class Order : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "cashier_id", "status", "created_at", "price", "ticket_number" };
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
            this(id, cashier_id, EnumCaster.OrderStatus.ToEnum(status), created_at, price, ticket_number) { }

        public Guid Id { get; }
        public Guid CashierId { get; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; }
        public double Price { get; }
        public int TicketNumber { get; }

        public object[] Data => new object[] { Id, CashierId, EnumCaster.OrderStatus.ToStr(Status), CreatedAt, Price, TicketNumber };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
