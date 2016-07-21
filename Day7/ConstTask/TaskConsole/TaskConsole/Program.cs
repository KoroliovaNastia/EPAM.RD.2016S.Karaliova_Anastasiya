using System;
using System.Collections;

namespace TaskConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      int time = 20;

      if (time > MyClassLibrary.Helpers.WaitTime)
      {
        Console.WriteLine("time : {0} > Wait Time {1}", time, MyClassLibrary.Helpers.WaitTime);
      }
      else
      {
        Console.WriteLine("time : {0} <= Wait Time {1}", time, MyClassLibrary.Helpers.WaitTime);
      }

      Console.WriteLine("========================================================");

      var result = MyClassLibrary.GetResult.GetUserResult("Red");

      var work = new Work();
      work.DoSomething(result);

            Hashtable hash = new Hashtable();
            hash[1] = "One";
            hash[2] = 4;
            hash[3] = 56m;
            foreach (dynamic entry in hash)
            {
                Console.WriteLine("{0}, {1}", entry.Key, entry.Value);
                Console.WriteLine(entry.GetType());
            }

            Console.ReadLine();
    }
  }
}
