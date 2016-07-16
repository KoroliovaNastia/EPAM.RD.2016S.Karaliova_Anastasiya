using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Repository;

namespace DAL.Infrastructure
{
    public class UserService:IUserService
    {
        UserRepository userRepo;
        public event EventHandler<ActionEventArgs> Message; 

        public UserService(IRepository<User> repository)
        {
            userRepo = (UserRepository)repository;
        }

        
        public int AddUser(User user)
        {
            NLogger.Logger.Info("Service: request to add user.");
            OnMessage(new ActionEventArgs("User added/created"));
            return userRepo.Create(user);
        }

        public IEnumerable<User> SearchForUsers(Func<User, bool> predicate)
        {
            NLogger.Logger.Info("Service: request to search user.");
            return userRepo.Find(predicate);
        }

        public bool Delete(User user)
        {
            NLogger.Logger.Info("Service: request to delete user.");
            OnMessage(new ActionEventArgs("User deleted"));
            return userRepo.Delete(user);
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
                NLogger.Logger.Error("Service: request to load failed:"+e.Message);
                throw;
            }

            using (var fileStr = new FileStream(file, FileMode.OpenOrCreate))
            {
                userRepo.Users = (List<User>)loader.Deserialize(fileStr);
                userRepo.UserIterator = new IdIterator().GetIdEnumerator(userRepo.Users.Last().Id).GetEnumerator();
                userRepo.UserIterator.MoveNext();
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
                NLogger.Logger.Error("Service: request on saving failed:" + e.Message);
                throw;
            }

            using (var fileStr = new FileStream(file, FileMode.OpenOrCreate))
            {
                saver.Serialize(fileStr, userRepo.Users);
            }
        }

        private void OnMessage(ActionEventArgs e)
        {
            if (Message != null) Message(this, e);

        }

    }
}
