using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAL.Configuration;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Repository;

namespace DAL.Infrastructure
{
    public class UserService : MarshalByRefObject, IUserService
    {
        public UserRepository UserRepo { get; private set; }
        static BooleanSwitch dataSwitch = new BooleanSwitch("Data", "DataAccess module");
        public event EventHandler<ActionEventArgs> Message;

        public UserService()
        {
            UserRepo = new UserRepository();
        }
        public UserService(IRepository<User> repository)
        {
            UserRepo = (UserRepository)repository;
            // AppDomain nd = AppDomain.CreateDomain("Master");
            // nd.CreateInstanceAndUnwrap(typeof(UserService).Assembly.FullName, typeof(UserService).FullName);
        }


        public int AddUser(User user)
        {
            NLogger.Logger.Info("Service: request to add user.");
            OnMessage(new ActionEventArgs("User added/created"));
            return UserRepo.Create(user);
        }

        public IEnumerable<User> SearchForUsers(Func<User, bool> predicate)
        {
            NLogger.Logger.Info("Service: request to search user.");
            return UserRepo.Find(predicate);
        }

        public bool Delete(User user)
        {
            NLogger.Logger.Info("Service: request to delete user.");
            OnMessage(new ActionEventArgs("User deleted"));
            return UserRepo.Delete(user);
        }

        public void Load()
        {
            var loader = new XmlSerializer(typeof(List<User>));
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
                UserRepo.Users = (List<User>)loader.Deserialize(fileStr);
                UserRepo.UserIterator = new IdIterator().GetIdEnumerator(UserRepo.Users.Last().Id).GetEnumerator();
                UserRepo.UserIterator.MoveNext();
            }
        }

        public void Save()
        {
            var saver = new XmlSerializer(typeof(List<User>));
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

        private void OnMessage(ActionEventArgs e)
        {
            if (Message != null) Message(this, e);

        }

    }
}
