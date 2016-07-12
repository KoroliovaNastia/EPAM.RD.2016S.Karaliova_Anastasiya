using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class UserRepository:IRepository<User>
    {
        //public IEnumerable<User> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public User Get(int? id)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(User item)
        {
            throw new NotImplementedException();
        }

        //public void Update(User item)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
