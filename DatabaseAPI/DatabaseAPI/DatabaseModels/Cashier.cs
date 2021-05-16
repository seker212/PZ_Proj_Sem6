using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public class Cashier : IDbModel
    {
        public Cashier(Guid id, string full_name, double bilans)
        {
            Id = id;
            FullName = full_name;
            Bilans = bilans;
        }

        public Guid Id { get; }
        public string FullName { get; }
        public double Bilans { get; set; }
        public object[] Data => new object[] { Id, FullName, Bilans };
        public static string[] ColumnNames => new string[] { "id", "full_name", "bilans" };
    }
}
