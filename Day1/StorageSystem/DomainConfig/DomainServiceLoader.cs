namespace DomainConfig
{
    using DAL.Infrastructure;
    using System;

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
