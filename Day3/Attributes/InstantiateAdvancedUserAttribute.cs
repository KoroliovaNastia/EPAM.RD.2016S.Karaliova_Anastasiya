using System;
namespace Attributes
{
    // Should be applied to assembly only.
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class InstantiateAdvancedUserAttribute : InstantiateUserAttribute
    {
        //private int id;
        //private string FirstName { get; set; }
        //private string LastName { get; set; }
        public int Number { get; set; }

        public InstantiateAdvancedUserAttribute(int id, string firstName, string lastName, int number)
            : base(id, firstName, lastName)
        {
            Number = number;
        }
    }
}
