using DAL.Configuration;
using DAL.Infrastructure;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DomainConfig
{
    public static class ServiceInitializer
    {
        public static IEnumerable<IUserService> InitializeServices()
        {
            var serviceSection = ServiceRegisterConfigSection.GetConfig();
                        Dictionary<string, string> serviceConfigurations =
                            new Dictionary<string, string>(serviceSection.ServiceItems.Count);
            


            for (int i = 0; i < serviceSection.ServiceItems.Count; i++)
                             {
                                 var serviceType = serviceSection.ServiceItems[i].ServiceType;
                                 var serviceName = serviceSection.ServiceItems[i].Path;
                                 serviceConfigurations[serviceName] = serviceType;
                             }
            


             IList<IUserService> services = new List<IUserService>();
                         foreach (var serviceConfiguration in serviceConfigurations)
                             {
                                 var domain = AppDomain.CreateDomain(serviceConfiguration.Key, null, null);
                                 var type = typeof(DomainServiceLoader);
                                 var loader = (DomainServiceLoader)domain.CreateInstanceAndUnwrap(Assembly.GetAssembly(type).FullName, type.FullName);

                UserService master = new UserService();   
                if (serviceConfiguration.Key == "master")
                {
                     master = loader.LoadMaster();
                    services.Add(master);
                        }
                else
                {
                    var service = loader.LoadSlave(master);
                    services.Add(service);
                }
                                 
                            }
            


             return services;

        }
    }
}
