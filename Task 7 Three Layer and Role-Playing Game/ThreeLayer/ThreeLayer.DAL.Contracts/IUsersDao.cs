using System;
using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.DAL.Contracts
{
    public interface IUsersDao
    {
        event EventHandler<User> UserRemoved;

        void AddUser(User user);
        IEnumerable<User> GetAllUsers();
        void RemoveUserById(int id);
    }
}
