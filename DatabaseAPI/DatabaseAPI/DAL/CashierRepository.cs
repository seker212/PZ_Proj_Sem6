using DatabaseAPI.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DAL
{
    public class CashierRepository : ObjectRepository<Cashier>, ICashierRepository
    {
        public CashierRepository(string connectionString) : base(connectionString, "cashiers", Cashier.ColumnNames)
        {
        }

        public Guid GetCashierKey(string name) => Query().Where("full_name", name).First<Cashier>().Id;
    }
}
