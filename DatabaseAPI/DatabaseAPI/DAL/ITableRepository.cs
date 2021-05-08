using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DAL
{
    interface ITableRepository<T>
    {
        public IEnumerable<T> Get();
        public T Get(object primaryKey);
        public void Insert(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
