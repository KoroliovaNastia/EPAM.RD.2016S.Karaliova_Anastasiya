using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    [Serializable]
    public class ServiceConfigInfo
    {
        public IPEndPoint IpEndPoint { get; set; }
        public string ServiceType { get; set; }
        public string Path { get; set; }
    }
}
