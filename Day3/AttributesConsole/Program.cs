using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Attributes;
using System.ComponentModel;

namespace AttributesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
             Type userType = typeof(User);
            InstantiateUserAttribute[] instantiateUserAttribute =
                (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userType, typeof(InstantiateUserAttribute));
            MatchParameterWithPropertyAttribute[] matchParameterWithPropertyAttribute =
                (MatchParameterWithPropertyAttribute[])
                    Attribute.GetCustomAttributes(userType.GetConstructors()[0], typeof(MatchParameterWithPropertyAttribute));
           
            List<User> users = new List<User>();

            // Gets the attributes for the property.
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(userType)[matchParameterWithPropertyAttribute[0].PropertyName].Attributes;

            /* Prints the default value by retrieving the DefaultValueAttribute 
             * from the AttributeCollection. */
            DefaultValueAttribute defIdAttribute =
               (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
            //Console.WriteLine("The default value of ID is: " + defIdAttribute.Value.ToString());

            for (int i = 0; i < 3; i++)
            {

                if(instantiateUserAttribute[i].id!=0)
                users.Add(new User (instantiateUserAttribute[i].id) { FirstName = instantiateUserAttribute[i].FirsName, LastName=instantiateUserAttribute[i].LastName });
                else
                    users.Add(new User((int)defIdAttribute.Value) { FirstName = instantiateUserAttribute[i].FirsName, LastName = instantiateUserAttribute[i].LastName });
                try
                {
                    IsValid(users[i]);
                }
                catch (ArgumentException e){
                    Console.WriteLine(e.Message);
                }
            }
            foreach (var user in users)
            {
                Console.WriteLine("User ID:" + user.Id);
                Console.WriteLine("First name:" + user.FirstName);
                Console.WriteLine("Last name:" + user.LastName);
                Console.WriteLine("-----------------------------");
            }

          
            Console.ReadKey();
        }
        public static void IsValid(User user)
        {
            StringBuilder exception = new StringBuilder();
            Type userType = typeof(User);
            IntValidatorAttribute[] intValid = (IntValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)[0], typeof(IntValidatorAttribute));
            if (user.Id < intValid[0].id || user.Id > intValid[0].limitId) exception.Append("Invalid bound of user ID");
            StringValidatorAttribute[] strFirstValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("FirstName"), typeof(StringValidatorAttribute));
            if (user.FirstName.Length > strFirstValid[0].StrLength) exception.Append("First Name can't be longer then {0} symbols" + strFirstValid[0].StrLength);
            StringValidatorAttribute[] strLastValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("LastName"), typeof(StringValidatorAttribute));
            if (user.LastName.Length > strLastValid[0].StrLength) exception.Append("Last Name can't be longer then {0} symbols" + strLastValid[0].StrLength);
        }
    }
    
}
