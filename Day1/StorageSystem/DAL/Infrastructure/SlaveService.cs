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

namespace DAL.Infrastructure
{
    public class SlaveService:IUserService
    {
        UserRepository userRepo;
        public static int SlaveCount { get; set; }

        public SlaveService(IRepository<User> repo, UserService service)
        {
            int value = Convert.ToInt32(ConfigurationManager.AppSettings["SlaveServises"]);
            if (SlaveCount >= value)
            {
                NLogger.Logger.Error("There is no way to create more than {0} instances of Slave class", value);
                throw new ArgumentException("There is no way to create more than 4 instances of Slave class");
            }

            SlaveCount++;
            userRepo = (UserRepository)repo;
            service.Message += SlaveListener;

        }

        public void SlaveListener(Object sender, ActionEventArgs eventArgs)
        {
            NLogger.Logger.Info("SlaveService received notice: " + eventArgs.Message);
        }

        public int AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchForUsers(Func<User, bool> predicate)
        {
            NLogger.Logger.Info("Service: request on adding user.");
            return userRepo.Find(predicate);
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }


        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
