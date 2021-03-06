﻿using System;

namespace Attributes
{
    // Should be applied to .ctors.
    // Matches a .ctor parameter with property. Needs to get a default value from property field, and use this value for calling .ctor.

    [AttributeUsage(AttributeTargets.Constructor| AttributeTargets.Method, AllowMultiple = true)]
    public class MatchParameterWithPropertyAttribute : Attribute
    {
        public string Property { get; set; }
        public string PropertyName { get; set; }

        public MatchParameterWithPropertyAttribute(string property, string propertyName)
        {
            Property = property;
            PropertyName = propertyName;
        }
    }
}
