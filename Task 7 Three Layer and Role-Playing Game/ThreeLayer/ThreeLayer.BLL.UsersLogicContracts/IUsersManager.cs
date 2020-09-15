using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IUsersManager
    {
        void AddUser(User user);
        void RemoveUserById(int id);
        IEnumerable<User> GetAllUsers();
    }
}
