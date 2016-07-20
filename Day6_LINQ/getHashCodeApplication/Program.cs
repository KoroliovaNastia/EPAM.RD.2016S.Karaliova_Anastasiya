using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getHashCodeApplication
{
    class Program
    {
        public delegate void Del1(int a);
        public delegate void Del2(int a);
        static void Main(string[] args)
        {
            var ab = new KeyValuePair<int, string>(1, "aaa");
            var a = new KeyValuePair<int, string>(2, "aaa");
            Console.WriteLine(ab.GetHashCode());
            Console.WriteLine(a.GetHashCode());


            Del1 del1 = new Del1(i=>Console.WriteLine("Del1"));

            Del2 del2 = new Del2(j => Console.WriteLine("Del2"));
            string str1 = "aaa";
            string str2 = "aaa";
            StringBuilder sb = new StringBuilder("g");
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            sw1.Start();
            for (int i = 0; i < 10000; i++)
            {
                str1 += "a";
            }
            sw1.Stop();
            Console.WriteLine(sw1.ElapsedTicks);
            sw1.Reset();
            sw2.Start();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append("a");

            }
            sw2.Stop();
            Console.WriteLine(sw2.ElapsedTicks);
            Console.WriteLine(str1.Equals((object)str2));
            Console.ReadKey();
        }
    }
}
