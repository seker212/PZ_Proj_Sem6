using DatabaseAPI.DAL;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface ICrudProductServices : ICrudServices<Product>
    {
        Task<Product> Read(Guid id);
    }

    public class CrudProductServices : CrudServices<Product>, ICrudProductServices
    {
        public CrudProductServices(IProductRepository repository) : base(repository)
        {
        }

        public override Task<Product> Create(Product entity)
        {
            return Task.Run(() =>
            {
                var productDb = new Product(Guid.NewGuid(), entity.Name, entity.Price, entity.Status);
                return _repository.Insert(productDb);
            });
        }

        public Task<Product?> Read(Guid id)
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
