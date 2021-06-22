﻿using DatabaseAPI.DatabaseModels;
using System.Collections.Generic;

namespace DatabaseAPI.DAL
{
    public interface IRepository<T> where T : IDbModel
    {
        bool Delete(T entity);
        void Dispose();
        IEnumerable<T> Get();
        T Get(object primaryKey);
        T Insert(T entity);
        T Update(T entity);
    }
}