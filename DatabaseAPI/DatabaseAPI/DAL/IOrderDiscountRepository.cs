using DatabaseAPI.DatabaseModels;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IOrderDiscountRepository : IPairRepository<OrderDiscount>
    {
        IEnumerable<OrderDiscount> GetOrderDiscounts(Order order);
    }
}