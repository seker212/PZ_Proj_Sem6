using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels.CRUD
{
    public class Cashier
    {
        public Cashier(Guid id, string fullName, double bilans)
        {
            Id = id;
            FullName = fullName;
            Bilans = bilans;
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double Bilans { get; set; }
    }
}
