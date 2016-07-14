using System;

namespace Attributes
{
    // Should be applied to classes only.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InstantiateUserAttribute : Attribute
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public readonly int id;

        public InstantiateUserAttribute()
        {
            FirsName = default(string);
            LastName = default(string);
            id = default(int);
        }

        public InstantiateUserAttribute(string firstName, string lastName)
        {
            FirsName = firstName;
            LastName = lastName;
        }
        public InstantiateUserAttribute(int id, string firstName, string lastName)
        {
            FirsName = firstName;
            LastName = lastName;
            this.id = id;
        }
    }
}
