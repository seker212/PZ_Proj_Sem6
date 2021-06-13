using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DAL;

namespace DatabaseAPI.Services
{
    public class DiscountServices : IDiscountServices
    {
        private IDiscountRepository _discountRepository;
        private IProductRepository _productRepository;

        public DiscountServices(IDiscountRepository discountRepository, IProductRepository productRepository)
        {
            _discountRepository = discountRepository;
            _productRepository = productRepository;
        }

        public Task<IEnumerable<ApiModels.Discount>> GetAvailable()
        {
            return Task.Run(() =>
            {
                return _discountRepository.GetAvailable()
                    .Select(x => 
                        new ApiModels.Discount(
                            x.Id, 
                            null, 
                            x.SetPrice,
                            x.PriceDropAmmount, 
                            x.PriceDropPercent,
                            _productRepository
                                .GetDiscountProducts(x)
                                .Select(y => 
                                    new ApiModels.Product(
                                        y.id, 
                                        y.name, 
                                        y.quantity
                                    )
                                )
                        )
                    );
            });
        }
    }
}
