using DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Interfaces;
using DomainConfig;

namespace SocketServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            IList<IUserService> services = ServiceInitializer.InitializeServices().ToList();
            IList<SlaveService> slaves = ServiceInitializer.GetSlaves(services).ToList();
            foreach (var slave in slaves)
            {
                var slaveThread = new Thread(() => { AsynchronousSocketListener.StartListening(slave.ServiceConfigInfo); });
                //slaveThread.IsBackground = true;
                slaveThread.Start();
            }
           // ServiceConfigInfo ser1 = new ServiceConfigInfo() { IpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000) };
           // var slaveThread1 = new Thread(() => { AsynchronousSocketListener.StartListening(ser1); });
           //// slaveThread.IsBackground = true;
           // slaveThread1.Start();
           // ServiceConfigInfo ser2 = new ServiceConfigInfo() { IpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001) };
           // var slaveThread2 = new Thread(() => { AsynchronousSocketListener.StartListening(ser2); });
           // // slaveThread.IsBackground = true;
           // slaveThread2.Start();
                //SL(2);
            
        }
        public static void SL(int i)
        {
            
            // Устанавливаем для сокета локальную конечную точку
            var serviceSection = ServiceRegisterConfigSection.GetConfig().ServiceItems[i];
            var port = serviceSection.Port;
            var ip = serviceSection.Ip;
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            //IPAddress ipAddr = ipHost.AddressList[0];
            IPAddress ipAddr = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    Console.Write("Полученный текст: " + data + "\n\n");

                    // Отправляем ответ клиенту\
                    string reply = "Спасибо за запрос в " + data.Length.ToString()
                            + " символов";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
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
    }
}
