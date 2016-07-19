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
        public ServiceCollection FolderItems
        {
            get { return ((ServiceCollection)(base["ServiceRegister"])); }
        }

    }
}
