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
        public Discount(Guid id, DiscountType type, float? setPrice, float? priceDropAmmount, float? priceDropPercent)
        {
            Id = id;
            Type = type;
            SetPrice = setPrice;
            PriceDropAmmount = priceDropAmmount;
            PriceDropPercent = priceDropPercent;
        }

        public Guid Id { get; }
        public DiscountType Type { get; }
        public float? SetPrice { get; }
        public float? PriceDropAmmount { get; }
        public float? PriceDropPercent { get; }
    }
}
