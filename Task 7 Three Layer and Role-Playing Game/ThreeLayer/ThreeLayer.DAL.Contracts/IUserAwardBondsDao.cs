using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.DAL.Contracts
{
    public interface IUserAwardBondsDao
    {
        bool Bind(int userId, int awardId);
        bool UnBind(int userId, int awardId);

        IEnumerable<User> GetOwnersOfAwardById(int awardId);
        IEnumerable<Award> GetAwardsOfUserById(int userId);
    }
}
