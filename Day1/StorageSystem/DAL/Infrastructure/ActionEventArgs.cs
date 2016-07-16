using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class ActionEventArgs : EventArgs
    {
        private readonly string message;
                public ActionEventArgs(string report)
        {
            if(report==null)
                throw new ArgumentNullException();
            message = report;
        }
      
        public string Message { get { return message; } }

    }
}
