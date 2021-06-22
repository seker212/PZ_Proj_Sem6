using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    public interface IDbModel
    {
        public object[] Data { get; }
        static public string[] ColumnNames { get; }
    }
}
