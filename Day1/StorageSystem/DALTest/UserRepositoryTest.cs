using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Repository;
using DAL.Entities;
using System.Collections.Generic;

namespace DALTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void Create_CreateThreeUsers_ReturnArrayWithThreeUsers()
        {
            UserRepository service = new UserRepository();
            User user1 = new User { FirstName = "Lisa", LastName = "Rich" };
            User user2 = new User { FirstName = "Nick", LastName = "Name" };
            User user3 = new User { FirstName = "Tim", LastName = "Rodny" };

            var result = new List<int> { service.Create(user1), service.Create(user2), service.Create(user3) };

            Assert.IsNotNull(result);
            Assert.IsNotNull(service.Users);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3, service.Users.Count);
        }

        [TestMethod]
        public void CreateUser_CreateAnExistUser_ReturnMinusOne()
        {
            UserRepository service = new UserRepository();
            User user1 = new User { FirstName = "Lisa", LastName = "Rich", VisaRecords = new List<Records>() };
            User user2 = new User { FirstName = "Lisa", LastName = "Rich", VisaRecords = new List<Records>() };
            List<int> result = new List<int> { service.Create(user1) };
            result.Add(service.Create(user2));

            Assert.IsNotNull(result);
            Assert.IsNotNull(service.Users);
            Assert.AreEqual(-1, result[1]);
            Assert.AreEqual(1, service.Users.Count);
        }
    }
}
