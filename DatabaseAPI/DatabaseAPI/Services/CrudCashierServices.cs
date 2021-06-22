using DatabaseAPI.DAL;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface ICrudCashierServices : ICrudServices<Cashier>
    {
        Task<Cashier> Read(Guid id);
    }

    public class CrudCashierServices : CrudServices<Cashier>, ICrudCashierServices
    {
        public CrudCashierServices(ICashierRepository repository) : base(repository)
        {
        }
        public override Task<Cashier> Create(Cashier entity)
        {
            return Task.Run(() =>
            {
                var cashierDb = new Cashier(Guid.NewGuid(), entity.FullName, entity.Bilans);
                return _repository.Insert(cashierDb);
            });
        }

        public Task<Cashier?> Read(Guid id)
        {
            return Task.Run(() =>
            {
                var collection = _repository.Get().Where(x => x.Id == id);
                if (collection.Count() == 1)
                    return collection.Single();
                else
                    return null;
            });
        }

    }
}
