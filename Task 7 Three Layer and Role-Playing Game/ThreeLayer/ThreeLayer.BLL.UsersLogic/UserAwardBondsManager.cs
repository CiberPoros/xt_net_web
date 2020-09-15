using System;
using System.Collections.Generic;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class UserAwardBondsManager : IUserAwardBondsDao
    {
        private readonly IUserAwardBondsDao _userAwardBondsDao;

        public UserAwardBondsManager(IUserAwardBondsDao userAwardBondsDao)
        {
            _userAwardBondsDao = userAwardBondsDao ?? throw new ArgumentNullException(nameof(userAwardBondsDao));
        }

        public bool Bind(int userId, int awardId) => _userAwardBondsDao.Bind(userId, awardId);
        public IEnumerable<Award> GetAwardsOfUserById(int userId) => _userAwardBondsDao.GetAwardsOfUserById(userId);
        public IEnumerable<User> GetOwnersOfAwardById(int awardId) => _userAwardBondsDao.GetOwnersOfAwardById(awardId);
        public bool UnBind(int userId, int awardId) => _userAwardBondsDao.UnBind(userId, awardId);
    }
}
