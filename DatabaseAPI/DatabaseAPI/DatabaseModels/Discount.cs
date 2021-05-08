using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    public enum DiscountType
    {
        ItemsSet,
        PriceDrop,
        PercentagePriceDrop
    }
    public class Discount
    {
        public Discount(Guid id, DiscountType type, double? setPrice, double? priceDropAmmount, double? priceDropPercent)
        {
            Id = id;
            Type = type;
            SetPrice = setPrice;
            PriceDropAmmount = priceDropAmmount;
            PriceDropPercent = priceDropPercent;
        }

        public Guid Id { get; }
        public DiscountType Type { get; }
        public double? SetPrice { get; }
        public double? PriceDropAmmount { get; }
        public double? PriceDropPercent { get; }
    }
}
