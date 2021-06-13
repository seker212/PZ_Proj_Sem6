using DatabaseAPI.DAL;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface IDiscountCrudServices : ICrudServices<Discount>
    {
        public Task<Discount?> Read(Guid id);
    }

    public class DiscountCrudServices : CrudServices<Discount>, IDiscountCrudServices
    {
        public DiscountCrudServices(IDiscountRepository repository) : base(repository)
        {
        }

        public override Task<Discount> Create(Discount entity)
        {
            return Task.Run(() =>
            {
                var discountDb = new Discount(Guid.NewGuid(), entity.IsAvailable, entity.SetPrice, entity.PriceDropAmmount, entity.PriceDropPercent);
                return _repository.Insert(discountDb);
            });
        }

        public Task<Discount?> Read(Guid id)
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
