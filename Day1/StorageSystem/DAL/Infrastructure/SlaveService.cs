using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;

namespace DAL.Infrastructure
{
    public class SlaveService:IUserService
    {
        UserRepository userRepo;

        public SlaveService(IRepository<User> repo, UserService service)
        {
            userRepo = (UserRepository)repo;
            service.Message += SlaveListener;
        }

        public void SlaveListener(Object sender, ActionEventArgs eventArgs)
        {
            NLogger.Logger.Info("SlaveService received notice: " + eventArgs.Message);
        }

        public int AddUser(Entities.User user)
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
