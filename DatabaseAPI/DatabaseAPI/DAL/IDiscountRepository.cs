using DatabaseAPI.DatabaseModels;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IDiscountRepository
    {
        IEnumerable<Discount> GetAvailable();
    }
}