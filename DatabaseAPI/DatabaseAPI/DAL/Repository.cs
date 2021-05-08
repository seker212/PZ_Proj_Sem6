using SqlKata.Execution;
using SqlKata.Compilers;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using DatabaseAPI.DatabaseModels;
using System.Data.Common;

namespace DatabaseAPI.DAL
{
    public abstract class Repository<T> : IDisposable, ITableRepository<T>
    {
        private readonly DbConnection _connection;
        private readonly QueryFactory _queryFactory;
        private readonly string _tableName;
        protected readonly string[] _columnNames;

        public Repository(string connectionString, string tableName, string[] columnNames)
        {
            _connection = new NpgsqlConnection(connectionString);
            _queryFactory = new QueryFactory(_connection, new PostgresCompiler());
            _tableName = tableName;
            _columnNames = columnNames;
        }

        public void Dispose()
        {
            _queryFactory.Dispose();
            _connection.Dispose();
        }

        public IEnumerable<T> Get() => _queryFactory.Query(_tableName).Get<T>();

        protected Query Query() => _queryFactory.Query(_tableName);

        public virtual T Get(object primaryKey) => Query().Where(_columnNames[0], primaryKey).First<T>();

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
