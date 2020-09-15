using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IUsersManager
    {
        void AddUser(User user);
        void RemoveUserById(int id);
        IEnumerable<User> GetAllUsers();
        void AddAwardToUser(User user, Award award);
    }
}
