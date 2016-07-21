using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Primes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating an expression tree.
            Expression<Func<int, bool>> expr = num => num < 5;

            // Compiling the expression tree into a delegate.
            Func<int, bool> result = expr.Compile();

            // Invoking the delegate and writing the result to the console.
            Console.WriteLine(expr.ToString());
            Console.WriteLine(result(4));

            //// Prints True.

            //// You can also use simplified syntax
            //// to compile and run an expression tree.
            //// The following line can replace two previous statements.
            Console.WriteLine(expr.Compile()(4));

            Func<int, bool> fun= num => num < 5;
            Console.WriteLine(fun(4));

            Console.ReadKey();
        }
    }
}
