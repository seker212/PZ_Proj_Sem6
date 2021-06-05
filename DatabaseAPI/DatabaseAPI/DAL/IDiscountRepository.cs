using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IDiscountRepository
    {
        IEnumerable<Discount> GetAvailable();
        public bool GetDiscountStatus(Guid id);
    }
}