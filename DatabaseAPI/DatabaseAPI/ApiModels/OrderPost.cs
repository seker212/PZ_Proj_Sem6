using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels
{
    public class OrderPost : IValidatableObject
    {
        public Guid CashierId { get; }
        public double Price { get; }
        public IEnumerable<Product> Products { get; }
        public IEnumerable<DiscountBasic> Discounts { get; }
        public DateTime CreatedAt { get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var nullProduct = Products.Any(x => x == null);
            if(nullProduct)
            {
                yield return new ValidationResult("No products in the order");
            }
            var emptyProductId = Products.Any(x => x.Id == Guid.Empty);
            if(emptyProductId)
            {
                yield return new ValidationResult("Product ID is empty");
            }
            var invalidProductCounter = Products.Any(x => x.Count <= 0);
            if(invalidProductCounter)
            {
                yield return new ValidationResult("Invalid product counter");
            }
            var nullDiscount = Discounts.Any(x => x == null);
            if(!nullDiscount)
            {
                var emptyDiscountId = Discounts.Any(x => x.Id == Guid.Empty);
                if(emptyDiscountId)
                {
                    yield return new ValidationResult("Discount ID is empty");
                }
                var invalidDiscountCounter = Discounts.Any(x => x.Count <= 0);
                if(invalidDiscountCounter)
                {
                    yield return new ValidationResult("Invalid discount counter");
                }
            }
        }
    }
}
