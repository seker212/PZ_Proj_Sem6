using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IProductRepository
    {
        IEnumerable<(Guid id, string name, int quantity)> GetOrderProducts(Order order);
    }
}