using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using NLog;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace DAL.Infrastructure
{
    public class SlaveService:IUserService
    {
        UserRepository userRepo;
        private static int slaveCount ;
        static BooleanSwitch dataSwitch = new BooleanSwitch("Data", "DataAccess module");

        public SlaveService(UserService service)
        {
            int value = Convert.ToInt32(ConfigurationManager.AppSettings["SlaveServises"]);
            if (slaveCount >= value)
            {
                NLogger.Logger.Error("There is no way to create more than {0} instances of Slave class", value);
                throw new ArgumentException("There is no way to create more than 4 instances of Slave class");
            }
            slaveCount++;
            userRepo = service.UserRepo;
            service.Message += SlaveListener;

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
            NLogger.Logger.Info("Slave Service: request on adding user.");
            return userRepo.Find(predicate);
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
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
                    NLogger.Logger.Error("Slave Service: request to load failed:" + e.Message);
                }
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
            throw new NotImplementedException();
        }
    }
}
