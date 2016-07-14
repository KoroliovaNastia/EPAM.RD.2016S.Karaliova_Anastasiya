using System;
using System.Runtime.InteropServices;

namespace Attributes
{
    // Should be applied to properties and fields.
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property, AllowMultiple = true)]
    public class StringValidatorAttribute : Attribute
    {
        public readonly int length;

        public StringValidatorAttribute(int length)
        {
            this.length = length;
        }
    }
}
