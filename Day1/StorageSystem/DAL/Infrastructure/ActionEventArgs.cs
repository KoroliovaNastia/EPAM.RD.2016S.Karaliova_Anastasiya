using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    [Serializable]
    public class ActionEventArgs : EventArgs
    {
        private string message;

        //public ActionEventArgs()
        //{

        //}

        //public ActionEventArgs(string report)
        //{
        //    if(report==null)
        //        throw new ArgumentNullException();
        //    message = report;
        //}
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
        public string Message { get { return message; }
            set {if(value==null)
                throw new ArgumentNullException();
            message = value; } 
        }

    }
}
