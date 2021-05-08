using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    interface IDbModel
    {
        public object[] Data { get; }
    }
}
