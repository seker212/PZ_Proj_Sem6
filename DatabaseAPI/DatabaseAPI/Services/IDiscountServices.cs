using DatabaseAPI.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface IDiscountServices
    {
        Task<IEnumerable<Discount>> GetAvailable();
    }
}