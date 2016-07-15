using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Infrastructure;

namespace DAL.Repository
{
    public class UserRepository:IRepository<User>
    {
        public IEnumerator<int> UserIterator { get; private set; }
        public List<User> Users { get; set; }

        public UserRepository()
        {
            UserIterator = new IdIterator(1).GetIdEnumerator(1).GetEnumerator();
            Users = new List<User>();
        }

        public UserRepository(IEnumerator<int> iterator)
        {
            UserIterator = iterator;
            Users = new List<User>();
        }
        public int Create(User user)
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

        public IEnumerable<User> Find(Func<User, bool> predicate)
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

       

        //public void Update(User item)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
