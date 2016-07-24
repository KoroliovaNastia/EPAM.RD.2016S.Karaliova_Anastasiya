using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Infrastructure;
using System.Configuration;
using System.IO;

namespace DAL.Repository
{
    [Serializable]
    public class UserRepository:IRepository<User>
    {
        public IEnumerator<int> UserIterator { get; set; }
        public List<User> Users { get; set; }

        public UserRepository()
        {
            UserIterator = new IdIterator().GetEnumerator();
            Users = new List<User>();
        }

        public UserRepository(IEnumerator<int> iterator)
        {
            UserIterator = iterator;
            Users = new List<User>();
        }
        public int Create(User user)
        {
            NLogger.Logger.Info("Repository: request to add user.");
            if (!Users.Contains(user))
            {

                user.Id = UserIterator.Current;
                UserIterator.MoveNext();
                Users.Add(user);
                NLogger.Logger.Info("Repository: user added.");
                return user.Id;
            }
            NLogger.Logger.Info("Repository: this user already exist.");
            return -1;
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            NLogger.Logger.Info("Repository: finding users on search parameter.");
            return Users.Where(predicate).ToList();
        }

        public bool Delete(User user)
        {
            NLogger.Logger.Info("Repository: request to delete user.");
            if (Users.Contains(user))
            {
                Users.Remove(user);
                NLogger.Logger.Info("Repository: user removed.");
                return true;
            }
            NLogger.Logger.Info("Repository: this user doesn't exist.");
            return false;
        }

       


    }
}
