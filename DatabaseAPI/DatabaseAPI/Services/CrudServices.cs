using DatabaseAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public class CrudServices<T> where T : DatabaseModels.IDbModel
    {
        protected IRepository<T> _repository;

        protected CrudServices(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual Task<T> Create(T entity)
        {
            return Task.Run(() => _repository.Insert(entity));
        }

        public Task<bool> Delete(T entity)
        {
            return Task.Run(() => _repository.Delete(entity));
        }

        public Task<IEnumerable<T>> Read()
        {
            return Task.Run(() => _repository.Get());
        }

        public Task<T> Update(T entity)
        {
            return Task.Run(() => _repository.Update(entity));
        }

    }
}
