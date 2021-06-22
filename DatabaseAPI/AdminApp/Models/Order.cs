using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NpgsqlTypes;
using Npgsql;

namespace AdminApp.Models
{
    public enum OrderStatus
    {
        [PgName("Preparing")]
        Preparing,
        [PgName("Serving")]
        Serving,
        [PgName("Finished")]
        Finished,
        [PgName("Canceled")]
        Canceled
    }

    public class Order
    {
        public Guid Id { get; set; }
        public Guid CashierId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Price { get; set; }
        public int TicketNumber { get; set; }
    }
}
