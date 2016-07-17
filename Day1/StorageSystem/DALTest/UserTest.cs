using System;
using System.Collections.Generic;
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
        public void Equals_EqualsTwoUsers_ResultFalse()
        {
            User user1 = new User { FirstName = "Alex", LastName = "Smit", Gender = Gender.Female };
            User user2 = new User { FirstName = "Alex", LastName = "Smit", Gender = Gender.Male };
            Assert.AreEqual(false, user1.Equals(user2));
        }

        [TestMethod]
        public void Equals_EqualsUserAndNotUser_ResultFalse()
        {
            User user1 = new User { FirstName = "Alex", LastName = "Smit", Gender = Gender.Female };
            var user2 = new List<int>(){};
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
        public void GetHashCode_EquastUserWithoutLastName_ResultFalse()
        {
            User user1 = new User { FirstName = "Alex", Gender = Gender.Female };
            User user2 = new User { FirstName = "Alex", Gender = Gender.Female };
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
