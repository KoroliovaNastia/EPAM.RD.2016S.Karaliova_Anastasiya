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
        [ConfigurationProperty("Services")]
        public ServiceCollection ServiceItems
        {
            get { return ((ServiceCollection)(base["Services"])); }
        }

        public static ServiceRegisterConfigSection GetConfig()
        {
            return (ServiceRegisterConfigSection)ConfigurationManager.GetSection("ServiceRegister") ?? new ServiceRegisterConfigSection();
        }

    }
}
