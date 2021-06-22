using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public class Discount : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "is_available", "set_price", "price_drop_amount", "price_drop_percent" };
        public Discount(Guid id, bool is_available, double? set_price, double? price_drop_amount, double? price_drop_percent)
        {
            Id = id;
            IsAvailable = is_available;
            SetPrice = set_price;
            PriceDropAmmount = price_drop_amount;
            PriceDropPercent = price_drop_percent;
        }
        public Guid Id { get; }
        public bool IsAvailable { get; }
        public double? SetPrice { get; }
        public double? PriceDropAmmount { get; }
        public double? PriceDropPercent { get; }

        public object[] Data => new object[] { Id, IsAvailable, SetPrice, PriceDropAmmount, PriceDropPercent };

        public static string[] ColumnNames => _staticColumnNames;
    }
}
