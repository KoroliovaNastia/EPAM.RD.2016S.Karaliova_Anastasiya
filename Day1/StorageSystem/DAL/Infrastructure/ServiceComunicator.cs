using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    [Serializable]
    public class ServiceComunicator
    {
        public event EventHandler<ActionEventArgs> Message;
        //private Sender<BllUser> _sender;
        public string sendMessage;
        public void Send(ActionEventArgs args)
        {
            sendMessage = args.Message;
        }

        public string GetMessage()
        {
            if (Message == null)
                return "no action";
            return sendMessage;
        }
    }
}
