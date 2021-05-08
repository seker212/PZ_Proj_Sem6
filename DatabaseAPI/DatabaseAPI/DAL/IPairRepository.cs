using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IPairRepository<T> : IRepository<T> where T : IDbModel
    {
        T Get(Guid primaryKey1, Guid primaryKey2);
        T Get(IEnumerable<Guid> primaryKeys);
    }
}