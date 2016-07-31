using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.Configuration;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserService
    {
        int AddUser(User user);
        IEnumerable<User> SearchForUsers(Func<User, bool> predicate);
        bool Delete(User user);
        void Load();
        void Save();
        void AddConnectionInfo(ServiceConfigInfo info);
    }
}
