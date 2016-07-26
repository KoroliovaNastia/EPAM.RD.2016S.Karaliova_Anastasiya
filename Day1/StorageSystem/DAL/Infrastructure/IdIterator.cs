using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    [Serializable]
    public class IdIterator//:IEnumerable<int>
    {
        //private int limit;

        //public IdIterator(int limit=1)
        //{
        //    if(limit<0)
        //        throw new ArgumentException();
        //    this.limit = limit;
        //}

        public static bool IsPrime(int number)
        {
            if (number == 2)
                return true;
            if (number == 0 || number % 2 == 0)
                return false;
            for (int i = 3; i < number; i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
        //public  IEnumerable<int> GetEnumerator(int start)
        //{
        //    if (start < 0)
        //    {
        //        throw new ArgumentException();
        //    }
        //    for (int i = start; i <= limit; i++)
        //    {
        //        if (IsPrime(i))
        //            yield return i;

        //    }

        //}

        //public IEnumerator<int> GetEnumerator()
        //{
        //    return GetEnumerator(1).GetEnumerator();
        //}

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        public static int GetNextId(int prevId)
        {
            if (prevId < 0)
            {
                throw new ArgumentException();
            }
            int nextId = prevId+1;
            while (!IsPrime(nextId))
            {
                nextId++;
            }
            return nextId;
        }
    }
}
