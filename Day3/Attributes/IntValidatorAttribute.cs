using System;

namespace Attributes
{
    // Should be applied to properties and fields.
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class IntValidatorAttribute : Attribute
    {
        public int id;
        public int limitId;
        public IntValidatorAttribute(int id, int limitId)
        {
            this.id = id;
            this.limitId = limitId;
        }
    }
}
