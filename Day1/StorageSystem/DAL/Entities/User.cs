using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Records VisaRecords { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public struct Records
    {
        public string Country { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

}
