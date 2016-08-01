//----------------------------------------------------------------------- 
// <copyright file="ServiceCollection.cs" company="No Company"> 
//     No Company. All rights reserved. 
// </copyright> 
//----------------------------------------------------------------------- 

namespace DAL.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Service Collection
    /// </summary>
    [ConfigurationCollection(typeof(ServiceElement))]
    public class ServiceCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create New Element
        /// </summary>
        /// <returns>return server element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        /// <summary>
        /// Get Element Key
        /// </summary>
        /// <param name="element"> config elements</param>
        /// <returns> return key</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)(element)).Path;
        }

        /// <summary>
        /// Service indexsator
        /// </summary>
        /// <param name="idx">service index</param>
        /// <returns>service indexes</returns>
        public ServiceElement this[int idx]
        {
            get { return (ServiceElement)BaseGet(idx); }
        }
    }
}
