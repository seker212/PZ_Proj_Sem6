using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAPI.DAL.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        [TestMethod()]
        public void TryGetTest()
        {
            new Repository().TryGet();
        }
    }
}