namespace SocketClient
{
using DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Interfaces;
using DomainConfig;

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
            AsynchronousClient client = new AsynchronousClient(infos);
            client.StartClient();
            RunMaster(master, client);
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                    break;
            }
            master.Save();

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
                        Console.Write(result.FirstName + " " + result.LastName);
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
                            //client.Message = master.Comunicator.GetMessage();
                            //client.StartClient();
                            client.Send(master.Comunicator.GetMessage());
                            //client.sendDone.WaitOne();
                            //client.Recieve();
                        }

                        Thread.Sleep(rand.Next(1000, 5000));
                        if (userToDelete != null)
                        {
                            int deleteChance = rand.Next(0, 3);
                            if (deleteChance == 0)
                                master.Delete(userToDelete);
                            //client.Message = master.Comunicator.GetMessage();
                            //client.StartClient();
                            client.Send(master.Comunicator.GetMessage());
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
   //     public class IDGSocketClient

   //{

   //    Socket clientSocket;
   //    private String response = String.Empty;

   //   // NetworkStream networkStream;

   //    public void Connect(string ipAddress, int port)

   //    {
   //        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
   //        clientSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
   //        clientSocket.Connect(ipAddress, port);
   //    }

   //    // public void Send(string data)

   //    //{

   //    //    //Write code here to send data

   //    //}

   //    public void Send(string data)
   //    {
   //        // Convert the string data to byte data using ASCII encoding.
   //        byte[] byteData = Encoding.ASCII.GetBytes(data);

   //        // Begin sending the data to the remote device.
   //        clientSocket.BeginSend(byteData, 0, byteData.Length, 0,
   //            new AsyncCallback(SendCallback), clientSocket);

   //    }

   //    private void SendCallback(IAsyncResult ar)
   //    {
   //        try
   //        {
   //            // Retrieve the socket from the state object.
   //            Socket client = (Socket)ar.AsyncState;

   //            // Complete sending the data to the remote device.
   //            int bytesSent = client.EndSend(ar);
   //            Console.WriteLine("Sent {0} bytes to server.", bytesSent);

   //            // Signal that all bytes have been sent.
   //            //sendDone.Set();
   //        }
   //        catch (Exception e)
   //        {
   //            Console.WriteLine(e.ToString());
   //        }
   //    }

   //    public void Close()

   //    {

   //        clientSocket.Close();

   //    }

   //    //public string Receive()

   //    //{

   //    //    //Write code here to receive data from the server

   //    //}
   //    public void Receive()
   //    {
   //        try
   //        {
   //            // Create the state object.
   //            StateObject state = new StateObject();
   //            state.workSocket = clientSocket;

   //            // Begin receiving the data from the remote device.
   //            clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
   //                new AsyncCallback(ReceiveCallback), state);
   //            Console.WriteLine("Response received : {0}", response);
   //        }
   //        catch (Exception e)
   //        {
   //            Console.WriteLine(e.ToString());
   //        }
   //    }

   //    private void ReceiveCallback(IAsyncResult ar)
   //    {
   //        try
   //        {
   //            // Retrieve the state object and the client socket 
   //            // from the asynchronous state object.
   //            StateObject state = (StateObject)ar.AsyncState;
   //            Socket client = state.workSocket;

   //            // Read data from the remote device.
   //            int bytesRead = client.EndReceive(ar);



   //            if (bytesRead > 0)
   //            {
   //                Array.Resize(ref state.buffer, bytesRead);
   //                // There might be more data, so store the data received so far.
   //                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

   //                Array.Resize(ref state.buffer, client.ReceiveBufferSize);

   //                // Get the rest of the data.
   //                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
   //                    new AsyncCallback(ReceiveCallback), state);
   //                Console.WriteLine("Response received : {0}", response);
   //            }
   //            else
   //            {
   //                // All the data has arrived; put it in response.
   //                if (state.sb.Length > 1)
   //                {
   //                    response = state.sb.ToString();
   //                }
   //                // Signal that all bytes have been received.
   //                //receiveDone.Set();
   //            }
   //        }
   //        catch (Exception e)
   //        {
   //            Console.WriteLine(e.ToString());
   //        }
   //    }


   //}

   //     public  static void Main(string[] args)

   //     {

   //         IDGSocketClient client = new IDGSocketClient();

   //         client.Connect("127.0.0.1", 11000);

   //         //client.Send("Hello");

   //         //client.Receive();

   //         //client.Send("I am machine");

   //         //client.Receive();

   //         //client.Send("I am machine");
   //         //Console.Read();
   //         Random rand = new Random();
   //         UserService master = new UserService();
   //         ThreadStart masterSearch = () =>
   //         {
   //             while (true)
   //             {
   //                 var serachresult = master.SearchForUsers(u => u.FirstName != null);
   //                 Console.Write("Master search results: ");
   //                 foreach (var result in serachresult)
   //                     Console.Write(result.FirstName + " " + result.LastName);
   //                 Console.WriteLine();
   //                 //AsynchronousClient.Send(master.Comunicator.GetMessage());
   //                 Thread.Sleep(rand.Next(1000, 5000));
   //             }
   //         };

   //         ThreadStart masterAddDelete = () =>
   //             {
   //             var users = new List<User>
   //                 {
   //                     new User {FirstName = "Alick", LastName = "Nero"},
   //                     new User {FirstName = "Alice", LastName = "Cooper"}
   //                 };
   //                 //string message;
   //                 User userToDelete = null;
   //                 while (true)
   //                 {
   //                     foreach (var user in users)
   //                     {
   //                         int addChance = rand.Next(0, 3);
   //                         if (addChance == 0)
   //                         {
   //                             master.AddUser(user);
   //                             //client.Message = master.Comunicator.GetMessage();
   //                             //client.StartClient();
   //                             // client.Send(master.Comunicator.GetMessage());
   //                             client.Send("User added");
   //                             //client.sendDone.WaitOne();
   //                             //client.Recieve();
   //                         }

   //                         Thread.Sleep(rand.Next(1000, 5000));
   //                         if (userToDelete != null)
   //                         {
   //                             int deleteChance = rand.Next(0, 3);
   //                             if (deleteChance == 0)
   //                             {
   //                                 master.Delete(userToDelete);
   //                                 //client.Message = master.Comunicator.GetMessage();
   //                                 //client.StartClient();
   //                                 // client.Send(master.Comunicator.GetMessage());
   //                                 client.Send("User deleted");
   //                             }
   //                             //client.sendDone.WaitOne();
   //                             //client.Recieve();
   //                             //Console.WriteLine(master.Comunicator.GetMessage());
   //                         }

   //                         userToDelete = user;
   //                         Thread.Sleep(rand.Next(1000, 5000));
   //                         Console.WriteLine();
   //                     }
   //                 }
                
   //         };
   //         Thread masterSearchThread = new Thread(masterSearch) { IsBackground = true };
   //         Thread masterAddThread = new Thread(masterAddDelete) {IsBackground = true};
   //         masterAddThread.Start();
   //         masterSearchThread.Start();

   //         while (true)
   //         {
   //             var quit = Console.ReadKey();
   //             if (quit.Key == ConsoleKey.Escape)
   //                 break;
   //         }
   //     }
    }
}
