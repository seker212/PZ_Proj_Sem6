using SqlKata.Execution;
using SqlKata.Compilers;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.DAL
{
    public class Repository
    {
        public void TryGet()
        {
            var cs = "Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=postgres";
            var conn = new NpgsqlConnection(cs);
            var compiler = new PostgresCompiler();
            var qf = new QueryFactory(conn, compiler);
            var test = qf.Query("orders").Get();
            IEnumerable<Order> orders = qf.Query("orders").Get<Order>();
            
        }
    }
}
