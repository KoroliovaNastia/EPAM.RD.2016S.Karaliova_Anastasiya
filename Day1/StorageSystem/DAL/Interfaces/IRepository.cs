using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //IEnumerable<T> GetAll();
        //T Get(int? id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        int Create(T item);
        //void Update(T item);
        bool Delete(T user);
    }
}
