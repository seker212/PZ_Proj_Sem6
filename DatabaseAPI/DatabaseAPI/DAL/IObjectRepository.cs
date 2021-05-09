using DatabaseAPI.DatabaseModels;
using System;

namespace DatabaseAPI.DAL
{
    public interface IObjectRepository<T> : IRepository<T> where T : IDbModel
    {
        T Get(Guid primaryKey);
        T Get(string primaryKeyGuid);
    }
}