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
        public Discount(Guid id, bool isAvailable, double? setPrice, double? priceDropAmmount, double? priceDropPercent)
        {
            Id = id;
            IsAvailable = isAvailable;
            SetPrice = setPrice;
            PriceDropAmmount = priceDropAmmount;
            PriceDropPercent = priceDropPercent;
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
