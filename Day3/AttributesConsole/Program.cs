using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Attributes;

namespace AttributesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           // Type userType = typeof(User);
           // InstantiateUserAttribute[] instantiateUserAttribute =
           //     (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userType, typeof(InstantiateUserAttribute));
           // MatchParameterWithPropertyAttribute[] matchParameterWithPropertyAttribute =
           //     (MatchParameterWithPropertyAttribute[])
           //         Attribute.GetCustomAttributes(userType, typeof(MatchParameterWithPropertyAttribute));
           // //PropertyInfo[] prop = userType.GetProperties();
           //Console.WriteLine(instantiateUserAttribute[0].FirsName);
           //Console.WriteLine(instantiateUserAttribute[1].FirsName);
           //Console.WriteLine(instantiateUserAttribute[2].FirsName);
           // List<User> users = new List<User>();

           // PropertyInfo[] props = typeof(User).GetProperties();
           // foreach (PropertyInfo prop in props)
           // {
           //     object[] attrs = prop.GetCustomAttributes(true);
           //     foreach (object attr in attrs)
           //     {
           //         InstantiateUserAttribute userAttr = attr as InstantiateUserAttribute;
           //         if (userAttr != null)
           //         {
           //             //string propName = prop.P;
           //             //string firstName = userAttr.FirsName;
           //             //string lastName = userAttr.LastName;
           //             //int id = userAttr.id;
           //             users.Add(new User(userAttr.id) { FirstName = userAttr.FirsName, LastName = userAttr.LastName });
           //         }
           //     }
           // }
           // foreach (var user in users)
           // {
           //     Console.WriteLine("User ID:" + user.Id);
           //     Console.WriteLine("First name:"+user.FirstName);
           //     Console.WriteLine("Last name:" + user.LastName);
           //     Console.WriteLine("-----------------------------");
           // }

            Type type = typeof(InstantiateUserAttribute);

            //iterating through the attribtues of the Rectangle class
            foreach (Object attributes in type.GetCustomAttributes(false))
            {
                InstantiateUserAttribute dbi = (InstantiateUserAttribute)attributes;
                if (null != dbi)
                {
                    Console.WriteLine("ID: {0}", dbi.id);
                    Console.WriteLine("First Name: {0}", dbi.FirsName);
                    Console.WriteLine("Last Name: {0}", dbi.LastName);
                    Console.WriteLine("-----------------------------");
                }
            }
            Console.ReadKey();
        }
    }
}
