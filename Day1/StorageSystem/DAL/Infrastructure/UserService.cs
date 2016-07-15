using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Repository;

namespace DAL.Infrastructure
{
    public class UserService:IUserService
    {
        UserRepository userRepo;

        public UserService(IRepository<User> repository)
        {
            userRepo = (UserRepository)repository;
        }

        
        public int AddUser(User user)
        {
            return userRepo.Create(user);
        }

        public IEnumerable<User> SearchForUsers(Func<User, bool> predicate)
        {
            return userRepo.Find(predicate);
        }

        public bool Delete(User user)
        {
            return userRepo.Delete(user);
        }
    }
}
