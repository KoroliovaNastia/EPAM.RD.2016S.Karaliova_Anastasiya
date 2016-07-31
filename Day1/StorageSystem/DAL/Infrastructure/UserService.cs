using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAL.Configuration;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Repository;

namespace DAL.Infrastructure
{
    [Serializable]
    public class UserService : MarshalByRefObject, IUserService
    {
        public UserRepository UserRepo { get; private set; }
        static BooleanSwitch dataSwitch = new BooleanSwitch("Data", "DataAccess module");
        private ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
        public ServiceConfigInfo ServiceConfigInfo { get; set; }
        //public event EventHandler<ActionEventArgs> Message;
        public ServiceComunicator Comunicator { get; set; }

        public UserService()
        {
            UserRepo = new UserRepository();
        }
        public UserService(IRepository<User> repository)
        {
            UserRepo = (UserRepository)repository;
        }


        public int AddUser(User user)
        {
            readerWriterLock.EnterWriteLock();
            try
            {

                NLogger.Logger.Info("Service: request to add user.");
                OnMessage(new ActionEventArgs(){Message = "User added/created"});
                return UserRepo.Create(user);
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        public IEnumerable<User> SearchForUsers(Func<User, bool> predicate)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                NLogger.Logger.Info("Service: request to search user.");
                return UserRepo.Find(predicate);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public bool Delete(User user)
        {
            readerWriterLock.EnterWriteLock();
            try
            {

                NLogger.Logger.Info("Service: request to delete user.");
                OnMessage(new ActionEventArgs{Message = "User deleted"});
                return UserRepo.Delete(user);
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }

        }

        public void Load()
        {
            readerWriterLock.EnterReadLock();
            try
            {
                var loader = new XmlSerializer(typeof (List<User>));
                string file;
                try
                {
                    file = ConfigurationManager.AppSettings["xmlfile"];
                }
                catch (ConfigurationException e)
                {
                    if (dataSwitch.Enabled)
                    {
                        NLogger.Logger.Error("Service: request to load failed:" + e.Message);
                    }
                    throw;
                }

                using (var fileStr = new FileStream(file, FileMode.OpenOrCreate))
                {
                    UserRepo.Users = (List<User>) loader.Deserialize(fileStr);
                    UserRepo.LastId = UserRepo.Users.Last().Id;
                    // UserRepo.UserIterator.MoveNext();
                }
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public void Save()
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                var saver = new XmlSerializer(typeof (List<User>));
                string file;
                try
                {
                    file = ConfigurationManager.AppSettings["xmlfile"];
                }
                catch (ConfigurationException e)
                {
                    if (dataSwitch.Enabled)
                    {
                        NLogger.Logger.Error("Service: request on saving failed:" + e.Message);
                    }
                    throw;
                }

                using (var fileStr = new FileStream(file, FileMode.OpenOrCreate))
                {
                    saver.Serialize(fileStr, UserRepo.Users);
                }
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        private void OnMessage(ActionEventArgs e)
        {
            //if (Message != null) Message(this, e);
            if(Comunicator!=null) Comunicator.Send(e);

        }

        public void AddConnectionInfo(ServiceConfigInfo info)
        {
            ServiceConfigInfo = info;
        }
    }
}
