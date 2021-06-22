using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace POSApp
{
    public class Discount
    {
        public Discount(Guid id, bool? isAvailable, double? setPrice, double? priceDropAmmount, double? priceDropPercent, IEnumerable<Product> products)
        {
            Id = id;
            IsAvailable = isAvailable;
            SetPrice = setPrice;
            PriceDropAmmount = priceDropAmmount;
            PriceDropPercent = priceDropPercent;
            Products = products;
        }

        public Guid Id { get; }
        public bool? IsAvailable { get; }
        public double? SetPrice { get; }
        public double? PriceDropAmmount { get; }
        public double? PriceDropPercent { get; }
        public IEnumerable<Product> Products { get; }
        public override string ToString() {
            string text = "";
            if (SetPrice != null) {
                text = "Cena : " + SetPrice.ToString();
                return text;
            } else if (PriceDropAmmount != null) {
                text = "Obniżka : " + PriceDropAmmount.ToString();
                return text;
            } else {
                text = "Procent : " + PriceDropPercent.ToString();
            }
            /*if (Products != null) {
                foreach (var p in Products) {
                    text += " : " + p.Name;
                }
            }*/
            return text;
        }
    }
}
