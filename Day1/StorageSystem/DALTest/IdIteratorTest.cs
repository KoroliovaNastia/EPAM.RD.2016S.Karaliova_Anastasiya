using System;
using System.Collections.Generic;
using DAL.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class IdIteratorTest
    {
        [TestMethod]
        public void GetEnumerator_LimitIsZero_ReturnAnEmptyArray()
        {

            List<int> result = new List<int>();
            foreach (var item in new IdIterator(0).GetIdEnumerator(0))
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
            foreach (var item in new IdIterator(-1).GetIdEnumerator())
            {
                result.Add(item);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEnumerator_StartIsMinusOne_ThrowAnException()
        {
            List<int> result = new List<int>();
            foreach (var item in new IdIterator().GetIdEnumerator(-1))
            {
                result.Add(item);
            }
        }

        [TestMethod]
        public void GetEnumerator_LimitIsFive_ReturnArrayWhithCapacityIsFive()
        {
            List<int> result = new List<int>();
            foreach (var item in new IdIterator(5).GetIdEnumerator())
            {
                result.Add(item);
            }
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Capacity);
        }

    }
}
