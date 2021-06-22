using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IDiscountRepository : IObjectRepository<Discount>
    {
        IEnumerable<Discount> GetAvailable();
        public bool GetDiscountStatus(Guid id);
    }
}