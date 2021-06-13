using DatabaseAPI.DAL;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface ICrudOrderServices : ICrudServices<Order>
    {
        Task<Order> Read(Guid id);
    }

    public class CrudOrderServices : CrudServices<Order>, ICrudOrderServices
    {
        public CrudOrderServices(IOrderRepository repository) : base(repository)
        {
        }

        public override Task<Order> Create(Order entity)
        {
            return Task.Run(() =>
            {
                var orderDb = new Order(Guid.NewGuid(), entity.CashierId, entity.Status, entity.CreatedAt, entity.Price, entity.TicketNumber);
                return _repository.Insert(orderDb);
            });
        }

        public Task<Order?> Read(Guid id)
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
