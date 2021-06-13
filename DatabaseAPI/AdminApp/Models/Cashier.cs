using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class Cashier
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double Bilans { get; set; }
    }
}
