namespace DAL.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Infrastructure;
    using System.Configuration;

    /// <summary>
    /// User repoository
    /// </summary>
    [Serializable]
    public class UserRepository:IRepository<User>
    {
        public int LastId { get; set; }
        public List<User> Users { get; set; }

        public UserRepository()
        {
            //UserIterator = new IdIterator().GetEnumerator();
            LastId = Convert.ToInt32(ConfigurationManager.AppSettings["Id"]);
            Users = new List<User>();
        }

        public UserRepository(int lastId)
        {
           // UserIterator = iterator;
            LastId = lastId;
            Users = new List<User>();
        }

        public int Create(User user)
        {
            NLogger.Logger.Info("Repository: request to add user.");
            if (!Users.Contains(user) && new UserValidation().Validate(user))
            {
                user.Id = IdIterator.GetNextId(LastId);
                ConfigurationManager.AppSettings["Id"] = LastId.ToString();
                // user.Id = UserIterator.Current;
                // UserIterator.MoveNext();
                Users.Add(user);
                NLogger.Logger.Info("Repository: user added.");
                return user.Id;
            }

            NLogger.Logger.Info("Repository: this user already exist.");
            return -1;
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            NLogger.Logger.Info("Repository: finding users on search parameter.");
            return Users.Where(predicate).ToList();
        }

        public bool Delete(User user)
        {
            NLogger.Logger.Info("Repository: request to delete user.");
            if (Users.Contains(user))
            {
                Users.Remove(user);
                NLogger.Logger.Info("Repository: user removed.");

                return true;
            }
            NLogger.Logger.Info("Repository: this user doesn't exist.");
            return false;
        }
    }
}
