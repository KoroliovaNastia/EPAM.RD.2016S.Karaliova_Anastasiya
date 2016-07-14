using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Infrastructure
{
    public class UserService:IUserService
    {
        public IEnumerator<int> UserIterator { get; private set; }
        public List<User> Users { get; set; }
 
        public UserService()
        {
            UserIterator =new IdIterator(1).GetIdEnumerator(1).GetEnumerator();
            Users = new List<User>();
        }

        public UserService(IEnumerator<int> iterator)
        {
            UserIterator = iterator;
            Users = new List<User>();
        }
        public int AddUser(User user)
        {
            if (!Users.Contains(user))
            {
                user.Id = UserIterator.Current;
                UserIterator.MoveNext();
                Users.Add(user);
                return user.Id;
            }
            return -1;
        }

        public IEnumerable<User> SearchForUsers(Func<User, bool> predicate)
        {
            return Users.Where(predicate).ToList(); 
        }

        public bool Delete(User user)
        {
            if (Users.Contains(user))
            {
                Users.Remove(user);
                return true;
            }
            return false;
        }
    }
}
