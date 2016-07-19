﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class ServiceCollection: ConfigurationElementCollection

    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)(element)).FolderType;
        }

        public ServiceElement this[int idx]
        {
            get { return (ServiceElement)BaseGet(idx); }
        }

    }
}