namespace DAL.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Repository;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Xml.Serialization;
    using DAL.Configuration;
    using System.Threading;

    [Serializable]
    public class SlaveService: MarshalByRefObject,IUserService
    {
        public UserRepository UserRepo { get; private set; }
    
        private static int slaveCount ;
        private static BooleanSwitch dataSwitch = new BooleanSwitch("Data", "DataAccess module");
        public ServiceConfigInfo ServiceConfigInfo { get; set; }
        private ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
        /// <summary>
        /// Initialize a new instanse of the User
        /// </summary>
        public SlaveService(){ }
        public SlaveService(UserService service)
        {
            UserRepo = service.UserRepo;
            var items = ServiceRegisterConfigSection.GetConfig().ServiceItems;
            int sk = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ServiceType == "Slave")
                    sk++;
            }

            if (slaveCount >= sk)
            {
                NLogger.Logger.Error("There is no way to create more than {0} instances of Slave class", sk);
                throw new ArgumentException("There is no way to create more than 4 instances of Slave class");
            }

            slaveCount++;
            service.Comunicator.Message+= SlaveListener;
        }

        public void SlaveListener(Object sender, ActionEventArgs eventArgs)
        {
            NLogger.Logger.Info("Slave Service received notice: " + eventArgs.Message);
        }

        public int AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchForUsers(Func<User, bool> predicate)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                NLogger.Logger.Info("Slave Service: request on adding user.");
                return UserRepo.Find(predicate);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
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
                        NLogger.Logger.Error("Slave Service: request to load failed:" + e.Message);
                    }
                    throw;
                }

                using (var fileStr = new FileStream(file, FileMode.OpenOrCreate))
                {
                    UserRepo.Users = (List<User>) loader.Deserialize(fileStr);
                    UserRepo.LastId = UserRepo.Users.Last().Id;
                }
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void AddConnectionInfo(ServiceConfigInfo info)
        {
            ServiceConfigInfo = info;
        }
    }
}
