using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primes;

namespace PrimesTest
{
    [TestClass]
    public class PrimesTest
    {
        [TestMethod]
        public void GetEnumerator_IsNull_ReturnMinusOne()
        {
            Primes.Primes list = new Primes.Primes();
            var result = 
            Assert.IsNull(result);
        }
    }
}
