using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Infrastructure
{
    public class UserValidation:IValidator
    {
        public bool Validate(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
                return false;
            return true;
        }
    }
}
