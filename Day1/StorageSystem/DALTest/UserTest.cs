using System;
using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Equals_EqualsTwoUsers_ResultTrue()
        {
            User user1 = new User { FirstName = "Lisa", LastName = "Rich", Gender = Gender.Female };
            User user2 = new User { FirstName = "Lisa", LastName = "Rich", Gender = Gender.Female };
            Assert.AreEqual(true, user1.Equals(user2));
        }

        [TestMethod]
        public void EqualsTest_EqualsTwoUsers_ResultFalse()
        {
            User user1 = new User { FirstName = "Alex", LastName = "Smit", Gender = Gender.Female };
            User user2 = new User { FirstName = "Alex", LastName = "Smit", Gender = Gender.Male };
            Assert.AreEqual(false, user1.Equals(user2));
        }

        [TestMethod]
        public void GetHashCode_TwoEqualsUsers_ResultTrue()
        {
            User user1 = new User { FirstName = "Lisa", LastName = "Rich", Gender = Gender.Female };
            User user2 = new User { FirstName = "Lisa", LastName = "Rich", Gender = Gender.Female };
            Assert.AreEqual(true, user1.GetHashCode() == user2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_TwoUnEqualsUsers_ResultFalse()
        {
            User user1 = new User { FirstName = "Alex", LastName = "Smit", Gender = Gender.Female };
            User user2 = new User { FirstName = "Alex", LastName = "Smit", Gender = Gender.Male };
            Assert.AreEqual(false, user1.GetHashCode() == user2.GetHashCode());
        }
    }
}
