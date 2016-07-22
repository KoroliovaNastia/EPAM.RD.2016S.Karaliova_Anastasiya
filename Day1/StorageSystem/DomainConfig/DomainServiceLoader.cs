using DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainConfig
{
    public class DomainServiceLoader:MarshalByRefObject
    {
        public UserService LoadMaster()
        {
            return new UserService();
        }
        public SlaveService LoadSlave(UserService service)
        {
            return new SlaveService(service);
        }
    }
}
