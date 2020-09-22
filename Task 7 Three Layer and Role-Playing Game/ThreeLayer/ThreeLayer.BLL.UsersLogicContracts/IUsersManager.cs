using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IUsersManager
    {
        void AddUser(User user);
        bool RemoveUserById(int id);
        IEnumerable<User> GetAllUsers();
        bool BindToAward(int userId, int awardId);
        bool UnBindFromAward(int userId, int awardId);
        IEnumerable<Award> GetAwards(int userId);
    }
}
