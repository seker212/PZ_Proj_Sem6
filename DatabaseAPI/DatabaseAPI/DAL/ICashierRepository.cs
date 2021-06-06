using System;

namespace DatabaseAPI.DAL
{
    public interface ICashierRepository : IObjectRepository<DatabaseModels.Cashier>
    {
        Guid GetCashierKey(string name);
    }
}