using DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Interfaces;
using DomainConfig;

namespace SocketClient
{
    class Program
    {
        public static void Main(string[] args)
        {

            IList<IUserService> services = ServiceInitializer.InitializeServices().ToList();
            IList<SlaveService> slaves = ServiceInitializer.GetSlaves(services).ToList();
            UserService master = ServiceInitializer.GetMaster(services);
            List<ServiceConfigInfo> infos = new List<ServiceConfigInfo>();
            foreach (var server in slaves)
            {
                infos.Add(server.ServiceConfigInfo);
            }
            AsynchronousClient client=new AsynchronousClient(infos);
            //client.StartClient("no action");
            RunMaster(master,client);
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                    break;
            }
            master.Save();            //SC();

        }
        private static void RunMaster(UserService master, AsynchronousClient client)
        {
            Random rand = new Random();
            ThreadStart masterSearch = () =>
            {
                while (true)
                {
                    var serachresult = master.SearchForUsers(u => u.FirstName != null);
                    Console.Write("Master search results: ");
                    foreach (var result in serachresult)
                        Console.Write(result.FirstName + " "+result.LastName);
                    Console.WriteLine();
                    //AsynchronousClient.Send(master.Comunicator.GetMessage());
                    Thread.Sleep(rand.Next(1000, 5000));
                }
            };

            ThreadStart masterAddDelete = () =>
            {
                var users = new List<User>
                {
                    new User { FirstName = "Alick", LastName = "Nero"},
                    new User { FirstName = "Alice", LastName = "Cooper"}
                };
                //string message;
                User userToDelete = null;
                while (true)
                {
                    foreach (var user in users)
                    {
                        int addChance = rand.Next(0, 3);
                        if (addChance == 0)
                        {
                            master.AddUser(user);
                            client.StartClient(master.Comunicator.GetMessage());
                            //client.Send(master.Comunicator.GetMessage());
                            //client.sendDone.WaitOne();
                            //client.Recieve();
                        }
                        //AsynchronousClient.Message = master.Comunicator.GetMessage();
                        //AsynchronousClient.StartClient();
                        //Console.WriteLine(master.Comunicator.GetMessage());
                        Thread.Sleep(rand.Next(1000, 5000));
                        if (userToDelete != null)
                        {
                            int deleteChance = rand.Next(0, 3);
                            if (deleteChance == 0)
                                master.Delete(userToDelete);
                                    client.StartClient(master.Comunicator.GetMessage());
                            //client.Send(master.Comunicator.GetMessage());
                            //client.sendDone.WaitOne();
                            //client.Recieve();
                            //Console.WriteLine(master.Comunicator.GetMessage());
                        }
                        
                        userToDelete = user;
                        Thread.Sleep(rand.Next(1000, 5000));
                        Console.WriteLine();
                    }
                }
            };

            Thread masterSearchThread = new Thread(masterSearch) { IsBackground = true };
            Thread masterAddThread = new Thread(masterAddDelete) { IsBackground = true };
            masterAddThread.Start();
            masterSearchThread.Start();
        }
        public static void SC()
        {
            try
            {
                var serviceSection = ServiceRegisterConfigSection.GetConfig().ServiceItems[2];
                var port = serviceSection.Port;
                SendMessageFromSocket(port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
        static void SendMessageFromSocket(int port)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            var serviceSection = ServiceRegisterConfigSection.GetConfig().ServiceItems[2];
            var ip = serviceSection.Ip;
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);

            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();

            Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);

            Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            // Используем рекурсию для неоднократного вызова SendMessageFromSocket()
            if (message.IndexOf("<TheEnd>") == -1)
                SendMessageFromSocket(port);

            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
