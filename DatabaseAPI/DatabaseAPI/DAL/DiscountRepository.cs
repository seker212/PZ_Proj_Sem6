using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlKata.Execution;

namespace DatabaseAPI.DAL
{
    public class DiscountRepository : Repository<Discount>, ITableRepository<Discount>
    {
        DiscountRepository(string connectionString) : base(connectionString, "discounts", new string[] { "id", "type", "set_price", "price_drop_amount", "price_drop_percent" }) { }

        public void Delete(Discount entity)
        {
            throw new NotImplementedException();
        }

        public Discount Get(Guid guid) => Query().Where(_columnNames[0], guid).First<Discount>();

        public Discount Get(object primaryKey) => primaryKey.GetType() == typeof(Guid) ? base.Get(primaryKey) : throw new Exception("Wrong type of primary key");

        public void Insert(Discount entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Discount entity)
        {
            throw new NotImplementedException();
        }
    }
}
