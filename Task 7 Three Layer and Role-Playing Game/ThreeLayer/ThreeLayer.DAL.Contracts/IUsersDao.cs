using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.DAL.Contracts
{
    public interface IUsersDao
    {
        void AddUser(User user);
        void RemoveUserById(int id);
        IEnumerable<User> GetAllUsers();

        void AddAwardToUser(User user, Award award);
        void RemoveAwardFromUser(User user, Award award);
    }
}
