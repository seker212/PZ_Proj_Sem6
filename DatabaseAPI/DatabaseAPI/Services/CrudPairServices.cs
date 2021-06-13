using DatabaseAPI.DAL;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface ICrudPairServices<T> where T : IDbModel
    {
        Task<T> Create(T entity);
        Task<bool> Delete(Guid key1, Guid key2);
        Task<IEnumerable<T>> Get();
        Task<T> GetInstance(Guid key1, Guid key2);
        Task<T> Update(T entity);
    }

    public class CrudPairServices<T> : ICrudPairServices<T> where T : DatabaseModels.IDbModel
    {
        IPairRepository<T> _pairRepository;

        public CrudPairServices(IPairRepository<T> pairRepository)
        {
            _pairRepository = pairRepository;
        }

        public Task<T> Create(T entity)
        {
            return Task.Run(() => { return _pairRepository.Insert(entity); });
        }

        public Task<T> Update(T entity)
        {
            return Task.Run(() => { return _pairRepository.Update(entity); });
        }

        public Task<T> GetInstance(Guid key1, Guid key2)
        {
            return Task.Run(() => { return _pairRepository.Get(key1, key2); });
        }

        public Task<IEnumerable<T>> Get()
        {
            return Task.Run(() => { return _pairRepository.Get(); });
        }

        public Task<bool> Delete(Guid key1, Guid key2)
        {
            return Task.Run(() => { return _pairRepository.Delete(key1, key2); });
        }
    }
}
