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
using DAL.Entities;
using DomainConfig;
using DAL.Interfaces;
using SocketClient;
using SocketServer;
//using static SocketClient.AsynchronousClient;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //IList<IUserService> services = ServiceInitializer.InitializeServices().ToList();
            //for (int i = 0; i < services.Count; ++i)
            //{
            //    var service = services[i];
            //    Console.Write("Service {0} : type = ",i);
            //    Console.WriteLine("Current Domain: " + AppDomain.CurrentDomain.FriendlyName);
            //    Console.WriteLine("IsProxy: " + RemotingServices.IsTransparentProxy(service));
            //    RealProxy rp = RemotingServices.GetRealProxy(services[i]);
            //    var fieldInfo = rp.GetType().GetField("_domainID", BindingFlags.Instance | BindingFlags.NonPublic);
            //    if (fieldInfo != null)
            //    {
            //        int id = (int)fieldInfo.GetValue(rp);
            //        Console.WriteLine("Id = {0}", id);
            //    }
            //}
            //Console.ReadKey();

            IList<IUserService> services = ServiceInitializer.InitializeServices().ToList();
            //ShowServicesInfo(services);
            //Console.WriteLine("\nPress enter to start: ");
            //Console.ReadLine();
            var master = (UserService)services.Single(s => s is UserService);
            var slaves = services.OfType<SlaveService>();
            // Create the thread object, passing in the
            // serverObject.StaticMethod method using a
            // ThreadStart delegate.
            ////if (master.ServiceConfigInfo != null)
            ////{
            ////    Thread staticCaller = new Thread(
            ////        new ThreadStart(RunMaster));

            ////    // Start the thread.
            ////    staticCaller.Start();
            ////}

            ////Console.WriteLine("The Main() thread calls this after "
            ////    + "starting the new StaticCaller thread.");
            RunSlaves(slaves);
            //RunMaster(master);
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                    break;
            }
        }

        //private static void RunMaster()
        //{
        //}

        public static void RunMaster()
        {

            //AsynchronousClient.StartClient(master.ServiceConfigInfo);
            //Random rand = new Random();

            //ThreadStart masterSearch = () =>
            //{
            //    //while (true)
            //    //{
            //    //    AsynchronousClient.StartClient(master.ServiceConfigInfo);
            //    //    var serachresult = master.SearchForUsers(u => u.FirstName != null);
            //    //    Console.Write("Master search results: ");
            //    //    foreach (var result in serachresult)
            //    //        Console.Write(result + " ");
            //    //    Console.WriteLine();
            //    //    Thread.Sleep(rand.Next(1000, 5000));
            //    //}
            //};

            //ThreadStart masterAddDelete = () =>
            //{
            //    var users = new List<User>
            //    {
            //        new User { FirstName = "Alick", LastName = "Nero"},
            //        new User { FirstName = "Alice", LastName = "Cooper"},
            //    };
            //    User userToDelete = null;

            //    while (true)
            //    {
            //        foreach (var user in users)
            //        {
            //            int addChance = rand.Next(0, 3);
            //            if (addChance == 0)
            //                master.AddUser(user);

            //            Thread.Sleep(rand.Next(1000, 5000));
            //            if (userToDelete != null)
            //            {
            //                int deleteChance = rand.Next(0, 3);
            //                if (deleteChance == 0)
            //                    master.Delete(userToDelete);
            //            }
            //            userToDelete = user;
            //            Thread.Sleep(rand.Next(1000, 5000));
            //            Console.WriteLine();
            //        }
            //    }
            //};

            //Thread masterSearchThread = new Thread(masterSearch) { IsBackground = true };
            //Thread masterAddThread = new Thread(masterAddDelete) { IsBackground = true };
            //masterAddThread.Start();
            //masterSearchThread.Start();
        }

        private static void RunSlaves(IEnumerable<SlaveService> slaves)
        {
            Random rand = new Random();

            //foreach (var slave in slaves)
            //{
            //    var slaveThread = new Thread(() =>
            //    {
            //        while (true)
            //        {
            //            //AsynchronousClient client = new AsynchronousClient(slave.ServiceConfigInfo);
            //            AsynchronousSocketListener.StartListening(slave.ServiceConfigInfo);
            //            //var userIds = slave.SearchForUsers(u => !string.IsNullOrWhiteSpace(u.FirstName));
            //            //Console.Write("Slave search results: ");
            //            //foreach (var user in userIds)
            //            //    Console.Write(user + " ");
            //            //Console.WriteLine();
            //            //Thread.Sleep((int)(rand.NextDouble() * 5000));
            //        }

            //    });
            //    slaveThread.IsBackground = true;
            //    slaveThread.Start();
            //}
            var slaveThread = new Thread(() => { AsynchronousSocketListener.StartListening(slaves.First().ServiceConfigInfo); });
            slaveThread.IsBackground = true;
            slaveThread.Start();
        }

        private static void ShowServicesInfo(IEnumerable<IUserService> services)
        {
            var servicesList = services.ToList();
            Console.WriteLine("SERVICES INFO: \n");
            for (int i = 0; i < servicesList.Count; i++)
            {
                var service = servicesList[i];
                Console.Write("Service {0} : type = ",i);
                if (service is UserService)
                    Console.Write(" Master; ");
                else
                {
                    Console.Write(" Slave; ");
                }
                Console.Write("Current Domain: " + AppDomain.CurrentDomain.FriendlyName + "; ");
                Console.Write("IsProxy: " + RemotingServices.IsTransparentProxy(service) + "; ");
                Console.WriteLine();
            }
}
        }
    }

    // Because this class is derived from MarshalByRefObject, a proxy 
    // to a MarshalByRefType object can be returned across an AppDomain 
    // boundary.
    //public class MarshalByRefType : MarshalByRefObject
    //{
    //    //  Call this method via a proxy.
    //    public void SomeMethod(string callingDomainName)
    //    {
    //        // Get this AppDomain's settings and display some of them.
    //        AppDomainSetup ads = AppDomain.CurrentDomain.SetupInformation;
    //        Console.WriteLine("AppName={0}, AppBase={1}, ConfigFile={2}",
    //            ads.ApplicationName,
    //            ads.ApplicationBase,
    //            ads.ConfigurationFile
    //        );

    //        // Display the name of the calling AppDomain and the name 
    //        // of the second domain.
    //        // NOTE: The application's thread has transitioned between 
    //        // AppDomains.
    //        Console.WriteLine("Calling from '{0}' to '{1}'.",
    //            callingDomainName,
    //            Thread.GetDomain().FriendlyName
    //        );
    //    }
    //}



