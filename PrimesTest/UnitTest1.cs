using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primes;
using System.Collections.Generic;

namespace PrimesTest
{
    [TestClass]
    public class PrimesTest
    {
        [TestMethod]
        public void GetEnumerator_LimitIsZero_ReturnAnEmptyArray()
        {

            List<int> result = new List<int>();
            foreach (var item in Primes.Primes.GetEnumerator(0))
            {
                result.Add(item);
            }
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Capacity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEnumerator_LimitIsMinusOne_ThrowAnException()
        {
            List<int> result = new List<int>();
            foreach (var item in Primes.Primes.GetEnumerator(-1))
            {
                result.Add(item);
            }
        }

        [TestMethod]
        public void GetEnumerator_LimitIsFive_ReturnArrayWhithCapacityIsFive()
        {
            List<int> result = new List<int>();
            foreach (var item in Primes.Primes.GetEnumerator(5))
            {
                result.Add(item);
            }
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Capacity);
        }
    }
}
