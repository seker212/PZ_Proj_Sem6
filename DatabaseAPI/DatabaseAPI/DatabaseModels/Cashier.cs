using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    public class Cashier
    {
        public Cashier(Guid id, string fullName, double bilans)
        {
            Id = id;
            FullName = fullName;
            Bilans = bilans;
        }

        public Guid Id { get; }
        public string FullName { get; }
        public double Bilans { get; }
    }
}
