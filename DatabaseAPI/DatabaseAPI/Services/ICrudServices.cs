using DatabaseAPI.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface ICrudServices<T> where T : IDbModel
    {
        Task<T> Create(T entity);
        Task<bool> Delete(T entity);
        Task<IEnumerable<T>> Read();
        Task<T> Update(T entity);
    }
}