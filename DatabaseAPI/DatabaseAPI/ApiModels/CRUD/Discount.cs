using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels.CRUD
{
    public class Discount
    {
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
    }
}
