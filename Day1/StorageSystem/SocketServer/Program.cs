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
                slaveThread.Start();
            }
        }
    }
}
