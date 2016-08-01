namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;

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
