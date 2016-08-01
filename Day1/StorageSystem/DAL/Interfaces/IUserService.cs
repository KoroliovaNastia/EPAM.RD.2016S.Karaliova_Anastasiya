namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Configuration;
    using Entities;
    using System.ServiceModel;

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        int AddUser(User user);
        [OperationContract]
        IEnumerable<User> SearchForUsers(Func<User, bool> predicate);
        [OperationContract]
        bool Delete(User user);
        [OperationContract]
        void Load();
        [OperationContract]
        void Save();
        [OperationContract]
        void AddConnectionInfo(ServiceConfigInfo info);
    }
}
