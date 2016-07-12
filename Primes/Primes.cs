using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primes
{
    public class Primes
    {
        public  bool IsPrime(int number)
        {
            if (number == 0 || number==2 || number % 2 == 0)
                return false;
            for(int i=3; i<number; i+=2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
        public IEnumerable<int> GetEnumerator(int limit)
        {
            if (limit <= 0)
                throw new ArgumentException();
            for (int i = 0; i <= limit; i++)
            {
                if (IsPrime(i))
                    yield return i;
                
            }
            
        }
    }
}
