namespace DAL.Infrastructure
{
    using System;
    using DAL.Infrastructure;

    [Serializable]
    public class ServiceComunicator
    {
        public event EventHandler<ActionEventArgs> Message;

        private string message;

        public void Send(ActionEventArgs arg)
        {
            message = arg.Message;
        }

        public string GetMessage()
        {
            if (message == null)
                return "no action before";
            return message;
        }
    }
}