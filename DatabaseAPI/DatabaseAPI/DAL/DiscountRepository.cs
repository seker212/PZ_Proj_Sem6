using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DatabaseModels;
using DatabaseAPI.Helpers;
using SqlKata.Execution;

namespace DatabaseAPI.DAL
{
    public class DiscountRepository : ObjectRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(string connectionString) : base(connectionString, "discounts", Discount.ColumnNames)
        {
        }

        public IEnumerable<Discount> GetAvailable() => Query().Where("is_available", true).Get<Discount>();
        public bool GetDiscountStatus(Guid id) => Query().Select("is_available").Where("id", id).First<bool>();
    }
}
