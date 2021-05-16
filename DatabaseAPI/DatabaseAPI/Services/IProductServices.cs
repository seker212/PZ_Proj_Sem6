using DatabaseAPI.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductData>> GetAvaliableProducts();
    }
}