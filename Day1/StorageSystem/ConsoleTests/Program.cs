using DAL.Infrastructure;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting;
using DomainConfig;
using DAL.Interfaces;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get and display the friendly name of the default AppDomain.
            string callingDomainName = Thread.GetDomain().FriendlyName;
            Console.WriteLine(callingDomainName);

            // Get and display the full name of the EXE assembly.
            string exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine(exeAssembly);

            // Construct and initialize settings for a second AppDomain.
            AppDomainSetup ads = new AppDomainSetup();
            ads.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

            ads.DisallowBindingRedirects = false;
            ads.DisallowCodeDownload = true;
            ads.ConfigurationFile =
                AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            // Create the second AppDomain.
            AppDomain ad2 = AppDomain.CreateDomain("AD #2", null, ads);
            // A proxy to the object is returned.
            MarshalByRefType mbrt =
                (MarshalByRefType)ad2.CreateInstanceAndUnwrap(
                    exeAssembly,
                    typeof(MarshalByRefType).FullName
                );
            //  UserRepository repo = new UserRepository();
            //  UserService service = (UserService)ad3.CreateInstanceAndUnwrap(Assembly.GetAssembly(typeof(UserService)).FullName, typeof(UserService).FullName);
            // service = new UserService(repo);
            // Call a method on the object via the proxy, passing the 
            // default AppDomain's friendly name in as a parameter.
            mbrt.SomeMethod(callingDomainName);

            IList<IUserService> services = ServiceInitializer.InitializeServices().ToList();
            for (int i = 0; i < services.Count; ++i)
            {
                var service = services[i];
                Console.Write("Service {0} : type = ",i);
                Console.WriteLine("Current Domain: " + AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine("IsProxy: " + RemotingServices.IsTransparentProxy(service));
                RealProxy rp = RemotingServices.GetRealProxy(services[i]);
                var fieldInfo = rp.GetType().GetField("_domainID", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    int id = (int)fieldInfo.GetValue(rp);
                    Console.WriteLine("Id = {0}", id);
                }
            }



            // Unload the second AppDomain. This deletes its object and 
            // invalidates the proxy object.
           // AppDomain.Unload(ad2);
            //AppDomain.Unload(ad3);
            //AppDomain.Unload(ad4);
            try
            {
                // Call the method again. Note that this time it fails 
                // because the second AppDomain was unloaded.
                mbrt.SomeMethod(callingDomainName);
                Console.WriteLine("Sucessful call.");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("Failed call; this is expected.");
            }
            Console.ReadKey();
        }
    }

    // Because this class is derived from MarshalByRefObject, a proxy 
    // to a MarshalByRefType object can be returned across an AppDomain 
    // boundary.
    public class MarshalByRefType : MarshalByRefObject
    {
        //  Call this method via a proxy.
        public void SomeMethod(string callingDomainName)
        {
            // Get this AppDomain's settings and display some of them.
            AppDomainSetup ads = AppDomain.CurrentDomain.SetupInformation;
            Console.WriteLine("AppName={0}, AppBase={1}, ConfigFile={2}",
                ads.ApplicationName,
                ads.ApplicationBase,
                ads.ConfigurationFile
            );

            // Display the name of the calling AppDomain and the name 
            // of the second domain.
            // NOTE: The application's thread has transitioned between 
            // AppDomains.
            Console.WriteLine("Calling from '{0}' to '{1}'.",
                callingDomainName,
                Thread.GetDomain().FriendlyName
            );
        }
    }

}

