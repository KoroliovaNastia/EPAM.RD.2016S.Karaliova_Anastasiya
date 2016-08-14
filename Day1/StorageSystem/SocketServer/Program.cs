namespace SocketServer
{
    using DAL.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using DAL.Infrastructure;
    using DAL.Interfaces;
    using DomainConfig;

    class Program
    {
        public static void Main(string[] args)
        {
            IList<IUserService> services = ServiceInitializer.InitializeServices().ToList();
            IList<SlaveService> slaves = ServiceInitializer.GetSlaves(services).ToList();
            //AsynchronousSocketListener listener = new AsynchronousSocketListener(slaves.FirstOrDefault().ServiceConfigInfo);
            //    var slaveThread = new Thread(() => {listener.StartListening(); });
            //    slaveThread.Start();
            foreach (var slave in slaves)
            {
                //var slave1 = slave;
                var slaveThread = new Thread(() =>
                {
                    // AsynchronousSocketListener listener = new AsynchronousSocketListener(slave1.ServiceConfigInfo);
                    AsynchronousSocketListener.StartListening(slave.ServiceConfigInfo);
                });
                slaveThread.Start();
            }
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                    break;
            }
        }

        //public class SocketServer
        //{

        //    private static Socket serverSocket;
        //    private static StateObject state = new StateObject();

        //    public static void StartServer()
        //    {

        //        //IPHostEntry ipHostEntry = Dns.Resolve(Dns.GetHostName());

        //        //IPAddress ipAddress = ipHostEntry.AddressList[0];

        //        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);

        //        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //        serverSocket.Bind(ipEndPoint);
        //        serverSocket.Listen(1);

        //        Console.WriteLine("Asynchonous server socket is listening at: " + ipEndPoint.Address.ToString());


        //        WaitForClients();


        //    }




        //    private static void WaitForClients()
        //    {
        //        serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
        //    }


        //    //private static void OnClientConnected(IAsyncResult asyncResult)

        //    //{



        //    //    Socket listener = (Socket)asyncResult.AsyncState;
        //    //    Socket handler = listener.EndAccept(asyncResult); //client

        //    //    StateObject state = new StateObject();
        //    //    state.workSocket = handler;

        //    //        if (handler != null)

        //    //            Console.WriteLine("Received connection request from: " +
        //    //                              handler.RemoteEndPoint.ToString());

        //    //        HandleClientRequest(handler, asyncResult);




        //    //    WaitForClients();

        //    //}


        //    private static void AcceptCallback(IAsyncResult ar)
        //    {
        //        try
        //        {

        //            // Signal the main thread to continue.
        //            //allDone.Set();

        //            // Get the socket that handles the client request.
        //            Socket listener = (Socket)ar.AsyncState;
        //            Socket handler = listener.EndAccept(ar); //client

        //            //StateObject state = new StateObject();
        //            state.workSocket = handler;

        //            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //           new AsyncCallback(ReadCallback), state);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.ToString());
        //        }

        //        // Create the state object.
        //        //StateObject state = new StateObject();
        //        //state.workSocket = handler;
        //        //handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //        //    new AsyncCallback(ReadCallback), state);

        //        WaitForClients();
        //    }

        //    public static void ReadCallback(IAsyncResult ar)
        //    {
        //        //StateObject state = new StateObject();
        //        state =(StateObject)ar.AsyncState;
        //        Socket handler = state.workSocket;
        //        String content = String.Empty;

        //        int bytesRead = handler.EndReceive(ar);

        //        try
        //        {
        //            if (bytesRead > 0)
        //            {
        //                //Array.Resize(ref state.buffer, bytesRead);
        //                state.sb.Append(Encoding.ASCII.GetString(
        //                    state.buffer, 0, bytesRead));

        //                content = state.sb.ToString();

        //                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
        //                    content.Length, content);
        //                // Echo the data back to the client.
        //                Send(handler, content);
        //            }
        //            //else
        //            //{
                    

        //                // Not all data received. Get more.
        //                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //                new AsyncCallback(ReadCallback), state);
        //            //}
        //            state.buffer = new byte[StateObject.BufferSize];
                    
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.ToString());
        //        }
        //        //handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //        //         new AsyncCallback(ReadCallback), state);
        //        //state.buffer = null;
        //    }
        //    private static void HandleClientRequest(Socket clientSocket, IAsyncResult asyncResult)
        //    {
        //        StateObject state = (StateObject)asyncResult.AsyncState;
        //        Socket handler = state.workSocket;
        //        String content = String.Empty;

        //        int bytesRead = handler.EndReceive(asyncResult);

        //        try
        //        {
        //            if (bytesRead > 0)
        //            {
        //                state.sb.Append(Encoding.ASCII.GetString(
        //                    state.buffer, 0, bytesRead));

        //                content = state.sb.ToString();

        //                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
        //                    content.Length, content);
        //                // Echo the data back to the client.
        //                Send(clientSocket, content);
        //                //Write your code here to process the data
        //                // Send(clientSocket.Client, "message resieve");

        //            }
        //        }
        //        catch
        //        {

        //            throw;

        //        }

        //    }

        //    private static void Send(Socket handler, String data)
        //    {
        //        // Convert the string data to byte data using ASCII encoding.
        //        byte[] byteData = Encoding.ASCII.GetBytes(data);

        //        // Begin sending the data to the remote device.
        //        handler.BeginSend(byteData, 0, byteData.Length, 0,
        //            new AsyncCallback(SendCallback), handler);
        //        WaitForClients();
        //    }

        //    private static void SendCallback(IAsyncResult ar)
        //    {
        //        try
        //        {
        //            // Retrieve the socket from the state object.
        //            Socket handler = (Socket)ar.AsyncState;

        //            // Complete sending the data to the remote device.
        //            int bytesSent = handler.EndSend(ar);
        //            Console.WriteLine("Sent {0} bytes to client.", bytesSent);



        //            //handler.Shutdown(SocketShutdown.Both);
        //            //handler.Close();
        //            //handler.EndAccept(new AsyncCallback(AcceptCallback), handler);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.ToString());
        //        }

        //    }
        //}

        //public static void Main(string[] args)
        //{

        //    SocketServer.StartServer();

        //    Console.Read();

        //}
    }
}
