namespace DAL.Infrastructure
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    [Serializable]
    public class ActionEventArgs : EventArgs
    {
        private string message;

        public string Message { 
            get { return message; }

            set {
                if(value == null)
                throw new ArgumentNullException();
            message = value; } 
        }

    }
}