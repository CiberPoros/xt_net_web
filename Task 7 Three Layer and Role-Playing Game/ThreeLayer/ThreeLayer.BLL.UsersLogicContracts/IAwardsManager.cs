using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IAwardsManager
    {
        void AddAward(Award award);
        bool RemoveAwardById(int id);
        IEnumerable<Award> GetAllAwards();
        bool BindToUser(int awardId, int userId);
        bool UnBindFromUser(int awardId, int userId);
        IEnumerable<User> GetUsers(int awardId);
        void Update(Award award);
    }
}
