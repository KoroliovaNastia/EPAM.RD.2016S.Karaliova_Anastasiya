using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public IEnumerable<Records> VisaRecords { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            var user = obj as User;
            if (user == null)
                return false;
            return Equals(user);
        }

        public bool Equals(User other)
        {
            if ((String.CompareOrdinal(FirstName, other.FirstName) == 0) &&
                (String.CompareOrdinal(LastName, other.LastName) == 0)
                && (DateOfBirth == other.DateOfBirth) && (VisaRecords.SequenceEqual(other.VisaRecords)) &&
                (Gender == other.Gender))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (FirstName != null ? FirstName.GetHashCode() : 777) +
                           (LastName != null ? LastName.GetHashCode() : 777);
                hash = hash*(DateOfBirth.DayOfYear ^ 777)+Gender.GetHashCode();
            return hash;
            }
            
        }
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
