using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Repository;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DAL.Infrastructure;

namespace DALTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void Create_CreateThreeUsers_ReturnArrayWithThreeUsers()
        {
            UserRepository repository = new UserRepository();
            User user1 = new User { FirstName = "Lisa", LastName = "Rich" };
            User user2 = new User { FirstName = "Nick", LastName = "Name" };
            User user3 = new User { FirstName = "Tim", LastName = "Rodny" };

            var result = new List<int> { repository.Create(user1), repository.Create(user2), repository.Create(user3) };

            Assert.IsNotNull(result);
            Assert.IsNotNull(repository.Users);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3, repository.Users.Count);
        }

        [TestMethod]
        public void Create_CreateAnExistUser_ReturnMinusOne()
        {
            UserRepository repository = new UserRepository();
            User user1 = new User { FirstName = "Lisa", LastName = "Rich", VisaRecords = new List<Records>() };
            User user2 = new User { FirstName = "Lisa", LastName = "Rich", VisaRecords = new List<Records>() };
            List<int> result = new List<int> { repository.Create(user1) };
            result.Add(repository.Create(user2));

            Assert.IsNotNull(result);
            Assert.IsNotNull(repository.Users);
            Assert.AreEqual(-1, result[1]);
            Assert.AreEqual(1, repository.Users.Count);
        }

        [TestMethod]
        public void Find_FindUserByFirstName_ResultTwoUsers()
        {
            UserRepository repository = new UserRepository();
            User user1 = new User { FirstName = "Lisa", LastName = "Rich" };
            User user2 = new User { FirstName = "Nick", LastName = "Name" };
            User user3 = new User { FirstName = "Tim", LastName = "Rodny" };
            User user4 = new User {FirstName = "Tim", LastName = "Rich"};
            var result1 = new List<int> { repository.Create(user1), repository.Create(user2), repository.Create(user3), repository.Create(user4) };
            var result2 = repository.Find(u => u.FirstName == "Tim");
            Assert.AreEqual(4,result1.Count);
            Assert.AreEqual(2,result2.Count());
        }

        [TestMethod]
        public void UserRepository_Iterator()
        {
            IEnumerator<int> iterator = new IdIterator(5).GetIdEnumerator().GetEnumerator();
            UserRepository repository=new UserRepository(iterator);
            repository.UserIterator.MoveNext();
            repository.UserIterator.MoveNext();
            repository.UserIterator.MoveNext();
            repository.UserIterator.MoveNext();
            Assert.AreEqual(5,repository.UserIterator.Current);
        }
        [TestMethod]
        public void Delete_DeleteUnExistUser_ReturnFalse()
        {
            UserRepository repository = new UserRepository();
            User user1 = new User { FirstName = "Lisa", LastName = "Rich" };
            User user2 = new User { FirstName = "Nick", LastName = "Name" };
            var result1 = new List<int> {repository.Create(user1)};
            Assert.AreEqual(1,result1.Count);
            Assert.AreEqual(false, repository.Delete(user2));
        }
    }
}
