using DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class Program
    {
        public static void Main(string[] args)
        {


            // AsynchronousSocketListener.StartListening();
            //StartListening();
            SL();
               
        }
        public static void SL()
        {
            // Устанавливаем для сокета локальную конечную точку
            var serviceSection = ServiceRegisterConfigSection.GetConfig().ServiceItems[0];
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
        // Incoming data from the client.
        //public static string data = null;

        //public static void StartListening()
        //{
        //    // Data buffer for incoming data.
        //    byte[] bytes = new Byte[1024];

        //    // Establish the local endpoint for the socket.
        //    // Dns.GetHostName returns the name of the 
        //    // host running the application.
        //    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //    IPAddress ipAddress = ipHostInfo.AddressList[0];
        //    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        //    // Create a TCP/IP socket.
        //    Socket listener = new Socket(AddressFamily.InterNetwork,
        //        SocketType.Stream, ProtocolType.Tcp);

        //    // Bind the socket to the local endpoint and 
        //    // listen for incoming connections.
        //    try
        //    {
        //        listener.Bind(localEndPoint);
        //        listener.Listen(10);

        //        // Start listening for connections.
        //        while (true)
        //        {
        //            Console.WriteLine("Waiting for a connection...");
        //            // Program is suspended while waiting for an incoming connection.
        //            Socket handler = listener.Accept();
        //            data = null;

        //            // An incoming connection needs to be processed.
        //            while (true)
        //            {
        //                bytes = new byte[1024];
        //                int bytesRec = handler.Receive(bytes);
        //                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
        //                if (data.IndexOf("<EOF>") > -1)
        //                {
        //                    break;
        //                }
        //            }

        //            // Show the data on the console.
        //            Console.WriteLine("Text received : {0}", data);

        //            // Echo the data back to the client.
        //            byte[] msg = Encoding.ASCII.GetBytes(data);

        //            handler.Send(msg);
        //            handler.Shutdown(SocketShutdown.Both);
        //            handler.Close();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    Console.WriteLine("\nPress ENTER to continue...");
        //    Console.Read();

        //}
    }
}
