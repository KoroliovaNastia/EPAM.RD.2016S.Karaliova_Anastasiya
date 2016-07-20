using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class ServiceRegisterConfigSection:ConfigurationSection
    {
        [ConfigurationProperty("ServiceRegister")]
        public ServiceCollection ServiceItems
        {
            get { return ((ServiceCollection)(base["ServiceRegister"])); }
        }

        public static ServiceRegisterConfigSection GetConfig()
         { 
             return (ServiceRegisterConfigSection)System.Configuration.ConfigurationManager.GetSection("RegisterRepositories") ?? new ServiceRegisterConfigSection(); 
         }

}
}
