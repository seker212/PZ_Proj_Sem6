using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DAL;

namespace DatabaseAPI.Services
{
    public class ProductServices : IProductServices
    {
        private IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<ApiModels.ProductData>> GetAvaliableProducts()
        {
            return Task.Run(() =>
            {
                return _productRepository.GetAvailableProducts().Select(x => new ApiModels.ProductData(x.Id, x.Name, x.Price));
            });
        }
    }
}
