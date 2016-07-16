using System;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTest
{
    [TestClass]
    public class MasterSlaveTest
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void SlaveRepo_FailedAddUser()
        {
            UserRepository repository=new UserRepository();
            UserService service=new UserService(repository);
            SlaveService slave=new SlaveService(repository,service);
            slave.AddUser(new User() { FirstName = "Lisa", LastName = "Rich" });
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void SlaveRepo_FailedDeleteUser()
        {
            UserRepository repository = new UserRepository();
            User user = new User { FirstName = "Lisa", LastName = "Rich" };
            var res = repository.Create(user);
            UserService service = new UserService(repository);
            SlaveService slave = new SlaveService(repository, service);
            slave.Delete(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SlaveRepo_CreateFiveSlaves_ResultError()
        {
            UserRepository repository = new UserRepository();
            UserService service = new UserService(repository);
            SlaveService slave1 = new SlaveService(repository, service);
            SlaveService slave2 = new SlaveService(repository, service);
            SlaveService slave3 = new SlaveService(repository, service);
            SlaveService slave4 = new SlaveService(repository, service);
            SlaveService slave = new SlaveService(repository, service);
            
        }
    }
}
