using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public enum DiscountType
    {
        ItemsSet,
        PriceDrop,
        PercentagePriceDrop
    }
    public class Discount : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "type", "set_price", "price_drop_amount", "price_drop_percent" };
        public Discount(Guid id, DiscountType type, double? setPrice, double? priceDropAmmount, double? priceDropPercent)
        {
            Id = id;
            Type = type;
            SetPrice = setPrice;
            PriceDropAmmount = priceDropAmmount;
            PriceDropPercent = priceDropPercent;
        }

        public Discount(Guid id, string type, double? set_price, double? price_drop_amount, double? price_drop_percent) : this(id, EnumCaster.DiscountTypeFromString(type), set_price, price_drop_amount, price_drop_percent) { }

        public Guid Id { get; }
        public DiscountType Type { get; }
        public double? SetPrice { get; }
        public double? PriceDropAmmount { get; }
        public double? PriceDropPercent { get; }

        public object[] Data => new object[] { Id, EnumCaster.DiscountTypeToString(Type), SetPrice, PriceDropAmmount, PriceDropPercent };

        public static string[] ColumnNames => _staticColumnNames;
    }
}
