using DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            //try
            //{
            //    SendMessageFromSocket(11000);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //finally
            //{
            //    Console.ReadLine();
            //}
            //AsynchronousSocketClient client = new AsynchronousSocketClient();
            // AsynchronousClient.StartClient();
            //StartClient();
            SC();

        }
        public static void SC()
        {
            try
            {
                var serviceSection = ServiceRegisterConfigSection.GetConfig().ServiceItems[0];
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
            var serviceSection = ServiceRegisterConfigSection.GetConfig().ServiceItems[0];
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
        //public static void StartClient()
        //{
        //    // Data buffer for incoming data.
        //    byte[] bytes = new byte[1024];

        //    // Connect to a remote device.
        //    try
        //    {
        //        // Establish the remote endpoint for the socket.
        //        // This example uses port 11000 on the local computer.
        //        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //        IPAddress ipAddress = ipHostInfo.AddressList[0];
        //        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        //        // Create a TCP/IP  socket.
        //        Socket sender = new Socket(AddressFamily.InterNetwork,
        //            SocketType.Stream, ProtocolType.Tcp);

        //        // Connect the socket to the remote endpoint. Catch any errors.
        //        try
        //        {
        //            sender.Connect(remoteEP);

        //            Console.WriteLine("Socket connected to {0}",
        //                sender.RemoteEndPoint.ToString());

        //            // Encode the data string into a byte array.
        //            byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

        //            // Send the data through the socket.
        //            int bytesSent = sender.Send(msg);

        //            // Receive the response from the remote device.
        //            int bytesRec = sender.Receive(bytes);
        //            Console.WriteLine("Echoed test = {0}",
        //                Encoding.ASCII.GetString(bytes, 0, bytesRec));

        //            // Release the socket.
        //            sender.Shutdown(SocketShutdown.Both);
        //            sender.Close();

        //        }
        //        catch (ArgumentNullException ane)
        //        {
        //            Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        //        }
        //        catch (SocketException se)
        //        {
        //            Console.WriteLine("SocketException : {0}", se.ToString());
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}


    }
}
