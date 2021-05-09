using SqlKata.Execution;
using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DAL
{
    public class ObjectRepository<T> : Repository<T>, IObjectRepository<T> where T : IDbModel
    {
        public ObjectRepository(string connectionString, string tableName, string[] columnNames) : base(connectionString, tableName, columnNames)
        {
        }

        public T Get(Guid primaryKey) => base.Get(primaryKey);
        public T Get(string primaryKeyGuid) => base.Get(new Guid(primaryKeyGuid));

        public override T Get(object primaryKey)
        {
            if (primaryKey.GetType() == typeof(Guid))
            {
                var key = primaryKey as Guid? ?? throw new ArgumentNullException(); //TODO: Use more descriptive exception
                return Get(key);
            }
            else if (primaryKey.GetType() == typeof(string))
            {
                var key = primaryKey as string ?? throw new ArgumentNullException(); //TODO: Use more descriptive exception
                return Get(key);
            }
            else
                throw new Exception(); //TODO: Use more descriptive exception
        }
    }
}
