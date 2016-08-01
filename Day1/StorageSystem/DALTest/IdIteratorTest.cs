using System;
using System.Collections.Generic;
using DAL.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class IdIteratorTest
    {
        //[TestMethod]
        //public void GetEnumerator_LimitIsZero_ReturnAnEmptyArray()
        //{

        //    List<int> result = new List<int>();
        //    //foreach (var item in new IdIterator(0).GetEnumerator(0))
        //    //{
        //    //    result.Add(item);
        //    //}

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(0, result.Capacity);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNextId_PrevIdMinusOne_ThrowAnException()
        {
            List<int> result = new List<int>();
            //foreach (var item in new IdIterator(-1).GetEnumerator())
            //{
            //    result.Add(item);
            //}
            result.Add(IdIterator.GetNextId(-1));
        }
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void GetNextId_StartIsMinusOne_ThrowAnException()
        //{
        //    List<int> result = new List<int>();
        //    foreach (var item in new IdIterator().GetEnumerator(-1))
        //    {
        //        result.Add(item);
        //    }
        //}

        //[TestMethod]
        //public void GetNextId_LimitIsFive_ReturnArrayWhithCapacityIsFore()
        //{
        //    List<int> result = new List<int>();
        //    for (int i=0;i<=5; i++)
        //    {
        //        result.Add(IdIterator.GetNextId(i));
        //    }
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(4, result.Capacity);
        //}

    }
}
