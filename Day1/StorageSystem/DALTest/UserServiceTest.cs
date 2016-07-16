using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DALTest
{
    [TestClass]
    public class UserServiceTest
    {
        //[TestMethod]
        //public void AddUser_AddThreeUsers_ReturnArrayWithThreeUsers()
        //{
        //    UserService service = new UserService();
        //    User user1 = new User {FirstName = "Lisa", LastName = "Rich"};
        //    User user2 = new User {FirstName = "Nick", LastName = "Name"};
        //    User user3 = new User {FirstName = "Tim", LastName = "Rodny"};

        //    var result = new List<int> {service.AddUser(user1), service.AddUser(user2), service.AddUser(user3)};

        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(service.Users);
        //    Assert.AreEqual(3, result.Count);
        //    Assert.AreEqual(3, service.Users.Count);
        //}

        //[TestMethod]
        //public void AddUser_AddAnExistUser_ReturnMinusOne()
        //{
        //    UserService service = new UserService();
        //    User user1 = new User { FirstName = "Lisa", LastName = "Rich", VisaRecords = new List<Records>() };
        //    User user2 = new User { FirstName = "Lisa", LastName = "Rich", VisaRecords = new List<Records>() };
        //    List<int> result = new List<int> { service.AddUser(user1) };
        //    result.Add(service.AddUser(user2));

        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(service.Users);
        //    Assert.AreEqual(-1, result[1]);
        //    Assert.AreEqual(1, service.Users.Count);
        //}
        [TestMethod]
        public void SaveAndLoad_Users()
        {
            var repo = new UserRepository();
            repo.Create(new User
            {
                FirstName = "Bob"
            });
            repo.Create(new User
            {
                FirstName = "Alice"
            });
            repo.Create(new User
            {
                FirstName = "Sam"
            });
            var service=new UserService(repo);
            service.Save();
            service.Load();

            Assert.AreEqual(3, repo.Users.Count);
        }
    }
}
